using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserMessageRepository
    {
        bool SaveMessage(UserMessage userMessage);
        IEnumerable<UserMessage> GetUserMessages(Guid userProfileId, int index);
    }
}
