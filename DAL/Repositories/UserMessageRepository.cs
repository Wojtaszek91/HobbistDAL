using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.Entities;
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

        public Task<bool> SaveNewMessage(Guid messageBoxId, string content, Guid senderProfileId)
        {
            var sendername = _context.UserProfiles.FirstOrDefault(x => x.Id == senderProfileId).Username;
            UserMessage newUserMessage = new UserMessage(content, sendername);

            var message = _context.UserMessages.Add(newUserMessage);

            var messageBox = _context.MessageBoxes.FirstOrDefault(x => x.Id == messageBoxId);

            if (messageBox == null) return Task.FromResult(false);

            if (messageBox.MessageHistory == null) messageBox.MessageHistory = new List<UserMessage>();

            messageBox.MessageHistory.Add(message.Entity);

            return Task.FromResult(_context.SaveChanges() == 1 ? true : false);
        }

        public Task<Guid> CreateNewMessageBox(Guid profileOneId, Guid profileTwoId)
        {
            bool profileOneExists = _context.UserProfiles.FirstOrDefault(x => x.Id == profileOneId) != null ? true : false;
            bool profileTwoExists = _context.UserProfiles.FirstOrDefault(x => x.Id == profileTwoId) != null ? true : false;

            if (!profileOneExists || !profileTwoExists) return Task.FromResult(Guid.Empty);

            var firstMessageBox = _context.MessageBoxes.FirstOrDefault(x => x.ProfileOneId == profileOneId && x.ProfileTwoId == profileTwoId);
            var secondMessageBox = _context.MessageBoxes.FirstOrDefault(x => x.ProfileOneId == profileTwoId && x.ProfileTwoId == profileOneId);

            if (firstMessageBox != null) return Task.FromResult(firstMessageBox.Id);
            if (secondMessageBox != null) return Task.FromResult(secondMessageBox.Id);

            MessageBox newMessageBox = new MessageBox(profileOneId, profileTwoId);

            var savedMessagBox = _context.MessageBoxes.Add(newMessageBox);

            _context.SaveChanges();

            return Task.FromResult(savedMessagBox.Entity.Id);
        }

        public Task<MessageBox> GetMessageBoxById(Guid id)
        {
            var messageBox = _context.MessageBoxes
                .Where(x => x.Id == id)
                .Include(x => x.MessageHistory)
                .FirstOrDefault();
            messageBox.MessageHistory.OrderByDescending(x => x.SendTime);

            return Task.FromResult(messageBox);
        }

    }
}
