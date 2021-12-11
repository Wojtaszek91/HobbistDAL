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
        public IEnumerable<UserMessage> GetUserMessages(int userProfileId, int index)
        {
            throw new NotImplementedException();
        }

        public bool SaveMessage(UserMessage userMessage)
        {
            throw new NotImplementedException();
        }
    }
}
