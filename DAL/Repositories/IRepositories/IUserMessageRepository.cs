using Models.Models;
using Models.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserMessageRepository
    {
        Task<UserMessage> SaveNewMessage(Guid messageBoxId, string content, Guid senderProfileId);
        Task<Guid> CreateNewMessageBox(Guid profileOneId, Guid profileTwoId);
        Task<MessageBox> GetMessageBoxById(Guid id);
        Task<List<MessageBox>> GetAllUserMessageBoxes(Guid profileId);
        Task<string> GetUsernameByProfileId(Guid profileId);
    }
}
