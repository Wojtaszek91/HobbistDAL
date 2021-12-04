using DAL.DataContext;
using DAL.Repositories.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;

namespace DAL.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Add
        public bool AddFollower(int postId, int followerId)
        {
            var lol = _context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList;
            if (_context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList.Count() == 0) { _context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList = new List<int>() { followerId }; }
            else if (!_context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList.Contains(followerId)) _context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList.Add(followerId);
            return Save();
        }
        public bool AddPost(Post post)
        {
            post.FollowersList = new List<int>();
            post.IsBlocked = false;
            _context.Posts.Add(post);
            return Save();
        }
        public bool AddPostView(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return false;
            post.PostViews++;
            return Save();
        }
        #endregion Add

        #region Remove
        public bool RemoveFollower(int postId, int followerId)
        {
            _context.Posts.FirstOrDefault(p => p.Id == postId).FollowersList.Remove(followerId);
            return Save();
        }
        public bool DeletePost(int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return false;
            _context.Posts.Remove(post);
            return Save();
        }
        #endregion Remove

        #region Edit
        public Post EditBeginDate(DateTime beginDate, int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post != null) return new Post();
            post.BeginDate = beginDate;
            return Save() ? post : new Post();
        }
        public Post EditDayLast(int dayLast, int id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return new Post();
            post.DayLast = dayLast;
            return Save() ? post : new Post();
        }
        public bool EditPost(Post post)
        {
            var postFromDb = _context.Posts.FirstOrDefault(p => p.Id == post.Id);
            if (postFromDb == null) return false;

            postFromDb.ChainedTag = post.ChainedTag;
            postFromDb.PostMessage = post.PostMessage;
            postFromDb.BeginDate = post.BeginDate;
            postFromDb.DayLast = post.DayLast;
            postFromDb.Lat = post.Lat;
            postFromDb.Lng = post.Lng;

            return Save();
        }
        public bool EditPostHashTag(int id, HashTagDto hashTag)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            var hashTagFromDb = _context.HashTags.FirstOrDefault(h => h.HashTagName == hashTag.HashTagName);
            if (post == null || hashTagFromDb == null) return false;

            post.ChainedTag = hashTagFromDb;
            return Save();


        }
        public bool EditPostMessage(int id, string message)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null) return false;
            post.PostMessage = message;
            return Save();
        }
        #endregion Edit

        #region Get
        public IEnumerable<Post> GetGroupPostsFromDateToDate(DateTime beginDate, DateTime endDate, int groupId, int index)
        {
            var date = endDate - beginDate;
            return _context.Posts.Where(p => p.BeginDate >= beginDate && p.DayLast <= date.Days)
                .Skip(10 * index).Take(10);
        }

        public IEnumerable<Post> GetHashTagPostsFromDateToDate(DateTime beginDate, DateTime endDate, int hashTagId, int index)
        {
            var date = endDate - beginDate;
            return _context.Posts.Where(p => p.BeginDate >= beginDate && p.DayLast <= date.Days && p.ChainedTag.Id == hashTagId)
                .Skip(10 * index).Take(10); ;
        }

        public int GetPostAverageMark(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id).AverageMark;
        }

        public Post GetPostById(int id)
        {
            return _context.Posts.Include(h => h.ChainedTag).FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Post> GetPostsByProfileId(int userProfile, int index)
        {
            return _context.Posts.Include(h => h.ChainedTag)
                .Where(p => p.UserProfileId == userProfile)
                .OrderBy(p => p.BeginDate)
                .Skip(10 * index).Take(10);
        }

        public IEnumerable<Post> GetPostsByHashTag(string hashTag, int index)
        {
            return _context.Posts.Include(h => h.ChainedTag)
                .Where(p => p.ChainedTag.HashTagName == hashTag)
                .OrderBy(p => p.BeginDate)
                .Skip(10 * index).Take(10);
        }

        public IEnumerable<Post> GetPostsByProfileIdAndHashTag(int profileId, string hashTagName, int index)
        {
            return _context.Posts.Include(h => h.ChainedTag)
                .Where(p => p.UserProfileId == profileId && p.ChainedTag.HashTagName == hashTagName)
                .Skip(10 * index).Take(10); ;
        }

        public int GetPostViews(int id)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == id).PostViews;
        }

        public IEnumerable<Post> GetUserPostsFromDateToDate(DateTime beginDate, DateTime endDate, int profileId, int index)
        {
            var date = endDate - beginDate;
            return _context.Posts.Where(p => p.BeginDate >= beginDate && p.DayLast <= date.Days && p.UserProfileId == profileId)
                .Skip(10 * index).Take(10); ;
        }
        #endregion GET

        public bool DoesPostExists(int postId)
        {
            return _context.Posts.FirstOrDefault(p => p.Id == postId) == null ? false : true;
        }

        public bool BlockPost(int postId)
        {
            if (!DoesPostExists(postId)) return false;
            _context.Posts.FirstOrDefault(p => p.Id == postId).IsBlocked = true;
            return Save();
        }

        public bool UnblockPost(int postId)
        {
            if (!DoesPostExists(postId)) return false;
            _context.Posts.FirstOrDefault(p => p.Id == postId).IsBlocked = false;
            return Save();
        }

        public bool Save() => _context.SaveChanges() >= 0 ? true : false;
    }
}
