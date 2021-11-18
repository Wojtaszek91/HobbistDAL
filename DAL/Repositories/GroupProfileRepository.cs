using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class GroupProfileRepository : IGroupProfileRepository
    {
        private readonly ApplicationDbContext _context;

        public GroupProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public GroupProfile CreateGroupProfile(GroupProfile groupProfile)
        {
            _context.GroupProfiles.Add(groupProfile);
            if (Save())
            {
                return groupProfile;
            }
            else
            {
                return new GroupProfile();
            }
        }

        public bool DeleteGroupProfile(int id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if(groupProfile != null)
            {
                _context.GroupProfiles.Remove(groupProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool DoProfileExist(int id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<int> GetManagersIds(int id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return (IEnumerable<int>)groupProfile.ManagersId;
            }
            else
            {
                return new List<int>();
            }
        }

        public IEnumerable<int> GetMembersIds(int id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile != null)
            {
                return (IEnumerable<int>)groupProfile.MembersId;
            }
            else
            {
                return new List<int>();
            }
        }

        public GroupProfile GetProfileById(int id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id);
        }

        public string GetProfileDescription(int id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).Description;
        }

        public string GetProfilePhoto(int id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfilePhoto;
        }

        public int GetProfileViews(int id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfileViews;
        }

        public string GetVideoLink(int id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).VideoLink;
        }

        public bool IsManager(int id, int userId)
        {
            var managersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId;

            foreach(var manager in managersList)
            {
                if(manager.UserAccountManagerId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsMember(int id, int userId)
        {
            var membersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId;

            foreach (var member in membersList)
            {
                if (member.UserAccountId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SignInFollower(int id, int userId)
        {
            var followerAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);

            if(followerAccount != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Add(followerAccount);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInManager(int id, int userId)
        {
            var managerAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if(managerAccount != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfileManager = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserAccountManager = managerAccount,
                    UserAccountManagerId = managerAccount.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInMember(int id, int userId)
        {
            var memberAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberAccount != null && groupProfile != null)
            {
                GroupProfileUserAccount groupProfileManager = new GroupProfileUserAccount()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserAccount = memberAccount,
                    UserAccountId = memberAccount.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutFollower(int id, int userId)
        {
            var followerAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);

            if (followerAccount != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Remove(followerAccount);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutManager(int id, int userId)
        {
            var managerAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (managerAccount != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfileManager = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserAccountManager = managerAccount,
                    UserAccountManagerId = managerAccount.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Remove(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutMember(int id, int userId)
        {
            var memberAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberAccount != null && groupProfile != null)
            {
                GroupProfileUserAccount groupProfileManager = new GroupProfileUserAccount()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserAccount = memberAccount,
                    UserAccountId = memberAccount.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId.Remove(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public GroupProfile UpdateGroupProfile(GroupProfile groupProfile)
        {
            var groupProfileDb = _context.GroupProfiles.FirstOrDefault(g => g.Id == groupProfile.Id);
            if(groupProfileDb != null)
            {
                groupProfileDb = groupProfile;
                return groupProfile;
            }
            else
            {
                return groupProfileDb;
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
