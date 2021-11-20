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
                if(manager.UserProfileId == userId)
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
                if (member.ProfileId == userId)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SignInFollower(int id, int profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if(followerProfile != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Add(followerProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInManager(int id, int profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if(managerProfile != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = managerProfile,
                    UserProfileId = managerProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignInMember(int id, int profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile != null && groupProfile != null)
            {
                GroupProfileUserProfile groupProfileManager = new GroupProfileUserProfile()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = memberProfile,
                    ProfileId = memberProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId.Add(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutFollower(int id, int profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if (followerProfile != null)
            {
                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Remove(followerProfile);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutManager(int id, int profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (managerProfile != null && groupProfile != null)
            {
                GroupProfileManagers groupProfileManager = new GroupProfileManagers()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = managerProfile,
                    UserProfileId = managerProfile.Id
                };

                _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId.Remove(groupProfileManager);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool SignOutMember(int id, int profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile != null && groupProfile != null)
            {
                GroupProfileUserProfile groupProfileManager = new GroupProfileUserProfile()
                {
                    GroupProfile = groupProfile,
                    GroupProfileId = groupProfile.Id,
                    UserProfile = memberProfile,
                    ProfileId = memberProfile.Id
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
