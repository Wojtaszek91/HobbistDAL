using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IPostRepository
    {
        public Post GetPostById(int id);
        public IEnumerable<Post> GetPostsByUserId(int userId, int index);
        public IEnumerable<Post> GetUserPostsFromDateToDate(DateTime beginDate, DateTime endDate, int userId, int index);
        public IEnumerable<Post> GetGroupPostsFromDateToDate(DateTime beginDate, DateTime endDate, int groupId, int index);
        public IEnumerable<Post> GetHashTagPostsFromDateToDate(DateTime beginDate, DateTime endDate, int hashTagId, int index);
        public IEnumerable<Post> GetPostsByUserIdAndHashTag(int userId, string hashTagName, int index);
        public IEnumerable<Post> GetPostsByHashTag(string hashTag, int index);
        public int GetPostAverageMark(int id);
        public int GetPostViews(int id);
        public bool AddPostView(int id);
        public bool EditPostMessage(int id, string message);
        public bool EditPostHashTag(int id, HashTagDto hashTag);
        public bool AddPost(Post post);
        public bool BlockPost(int postId);
        public bool UnblockPost(int postId);
        public bool DeletePost(int id);
        public bool EditPost(Post post);
        public Post EditBeginDate(DateTime beginDate, int id);
        public Post EditDayLast(int dayLast, int id);
        public bool AddFollower(int postId, int followerId);
        public bool RemoveFollower(int postId, int followerId);
        public bool DoesPostExists(int postId);
        public bool Save();
    }
}
