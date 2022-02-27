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
        private DbContextOptions<ApplicationDbContext> _dbContextOptions;

        public UserMessageRepository(DbContextOptions<ApplicationDbContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public Task<UserMessage> SaveNewMessage(Guid messageBoxId, string content, Guid senderProfileId)
        {
            using (var db = new ApplicationDbContext(_dbContextOptions))
            {
                var sendername = db.UserProfiles.FirstOrDefault(x => x.Id == senderProfileId).Username;
                UserMessage newUserMessage = new UserMessage(content, sendername);

                var message = db.UserMessages.Add(newUserMessage);

                var messageBox = db.MessageBoxes.FirstOrDefault(x => x.Id == messageBoxId);

                if (messageBox == null) return null;

                if (messageBox.MessageHistory == null) messageBox.MessageHistory = new List<UserMessage>();

                messageBox.MessageHistory.Add(message.Entity);

                return Task.FromResult(message.Entity);
            }
        }

        public Task<Guid> CreateNewMessageBox(Guid profileOneId, Guid profileTwoId)
        {
            using (var db = new ApplicationDbContext(_dbContextOptions))
            {
                var profileOne = db.UserProfiles.FirstOrDefault(x => x.Id == profileOneId);
                var profileTwo = db.UserProfiles.FirstOrDefault(x => x.Id == profileTwoId);

                if (profileOne == null || profileTwo == null) return Task.FromResult(Guid.Empty);

                var firstMessageBox = db.MessageBoxes.FirstOrDefault(x => x.ProfileOneId == profileOneId && x.ProfileTwoId == profileTwoId);
                var secondMessageBox = db.MessageBoxes.FirstOrDefault(x => x.ProfileOneId == profileTwoId && x.ProfileTwoId == profileOneId);

                if (firstMessageBox != null) return Task.FromResult(firstMessageBox.Id);
                if (secondMessageBox != null) return Task.FromResult(secondMessageBox.Id);

                MessageBox newMessageBox = new MessageBox(profileOneId, profileOne.Username, profileTwoId, profileTwo.Username);
                
                var savedMessagBox = db.MessageBoxes.Add(newMessageBox);

                db.SaveChanges();

                return Task.FromResult(savedMessagBox.Entity.Id);
            }
        }

        public Task<string> GetUsernameByProfileId(Guid profileId)
        {
            using (var db = new ApplicationDbContext(_dbContextOptions))
            {
                return Task.FromResult(db.UserProfiles.FirstOrDefault(x => x.Id == profileId).Username);
            }
        }

        public Task<MessageBox> GetMessageBoxById(Guid id)
        {
            using (var db = new ApplicationDbContext(_dbContextOptions))
            {
                var messageBox = db.MessageBoxes
                    .Where(x => x.Id == id)
                    .Include(x => x.MessageHistory)
                    .FirstOrDefault();
                messageBox.MessageHistory.OrderByDescending(x => x.SendTime);

                return Task.FromResult(messageBox);
            }
        }

        public Task<List<MessageBox>> GetAllUserMessageBoxes(Guid profileId)
        {
            using (var db = new ApplicationDbContext(_dbContextOptions))
            {
                var messageBoxList = db.MessageBoxes
                    .Where(x => x.ProfileOneId == profileId || x.ProfileTwoId == profileId)
                    .Include(x => x.MessageHistory);

                foreach(var mB in messageBoxList)
                {
                    try
                    {
                        mB.MessageHistory.OrderByDescending(x => x.SendTime);
                    }
                    catch(Exception e)
                    {

                    }
                }
                return Task.FromResult(messageBoxList.ToList());
            }
        }
    }
}
