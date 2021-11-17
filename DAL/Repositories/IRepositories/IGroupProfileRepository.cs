using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IGroupProfileRepository
    {
        public GroupProfile GetProfileById(int id);
        public string GetProfileDescription(int id);
        public string GetVideoLink(int id);
        public string GetProfilePhoto(int id);
        public int GetProfileViews(int id);
        public GroupProfile CreateGroupProfile(GroupProfile groupProfile);
        public bool DeleteGroupProfile(int id);
        public GroupProfile UpdateGroupProfile(GroupProfile groupProfile);
        public bool DoProfileExist(int id);
        public IEnumerable<int> GetManagersIds(int id);
        public bool IsManager(int id, int userId);
        public IEnumerable<int> GetMembersIds(int id);
        public bool IsMember(int id, int userId);
        public bool SignInMember(int id, int userId);
        public bool SignOutMember(int id, int userId);
        public bool SignInManager(int id, int userId);
        public bool SignOutManager(int id, int userId);
        public bool SignInFollower(int id, int userId);
        public bool SignOutFollower(int id, int userId);
        public bool Save();
    }
}
