using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserMessageRepository : IUserMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public UserMessageRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IEnumerable<UserMessage> GetUserMessages(Guid userProfileId, int index)
        {
            return _context.UserMessages.
                Where(x => x.TargetProfileId == userProfileId)
                .OrderByDescending(x => x.SendTime)
                .Skip(10 * index)
                .Take(10);
        }

        public IEnumerable<UserMessage> GetNotOpenUserMessages(Guid userProfileId, int index)
        {
            return _context.UserMessages.
                Where(x => x.TargetProfileId == userProfileId && x.HasBeenOpen == false)
                .OrderByDescending(x => x.SendTime)
                .Skip(10 * index)
                .Take(10);
        }

        public bool SaveMessage(UserMessage userMessage)
        {
            _context.UserMessages.AddAsync(userMessage);
            return SaveChanges();
        }

        public bool MarkAsReaded(Guid messageId)
        {
            _context.UserMessages.FirstOrDefault(x => x.Id == messageId).HasBeenOpen = true;
            return SaveChanges();
        }

        private bool SaveChanges() 
            => _context.SaveChanges() > 0 ? true : false;
    }
}
