using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IPostMarkRepository
    {
        bool UpsertMark(UserProfile userProfile, Post post, int mark);
        int GetAverageMark(Guid postId);
    }
}
