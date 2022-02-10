using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Models.Models;
using Models.Models.Entities;
using System;
using System.Linq;

namespace DAL.Repositories
{
    public class PostMarkRepository : IPostMarkRepository
    {
        private readonly ApplicationDbContext _context;
        public PostMarkRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool UpsertMark(UserProfile userProfile, Post post, int mark)
        {
            if (mark < 0 || mark > 3 || userProfile == null || post == null) return false;

            try
            {
                var existingMark = _context.PostMark.SingleOrDefault(x => x.PostId == post.Id && x.UserProfileId == userProfile.Id);
                if (existingMark != null)
                {
                    existingMark.Mark = mark;
                    return _context.SaveChanges() == 1 ? true : false;
                }

                PostMark postMark = new PostMark()
                {
                    Post = post,
                    PostId = post.Id,
                    UserProfile = userProfile,
                    UserProfileId = userProfile.Id,
                    Mark = mark
                };

                _context.PostMark.Add(postMark);

                return _context.SaveChanges() == 1 ? true : false;
            }
            catch(Exception)
            {
                return false;
            }

        }

        public int GetAverageMark(Guid postId)
        {
            try
            {
                var sum = _context.PostMark.Where(x => x.PostId == postId).Count();
                var averageMark = _context.PostMark.Where(x => x.PostId == postId).Sum(x => x.Mark) / sum;
                return averageMark;
            }
            catch(Exception)
            {
                return 0;
            }

        }
    }
}
