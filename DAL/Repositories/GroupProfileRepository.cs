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

            return Save() ? groupProfile : new GroupProfile();
        }

        public bool DeleteGroupProfile(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (groupProfile == null)
                return false;

            _context.GroupProfiles.Remove(groupProfile);
            return Save();
        }

        public bool DoProfileExist(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            return groupProfile is null ?
                false
                :
                true;
        }

        public IEnumerable<Guid> GetManagersIds(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            return groupProfile is null ?
                new List<Guid>()
                :
                groupProfile.ManagersId.Select(x => x.UserProfileId);
        }

        public IEnumerable<Guid> GetMembersIds(Guid id)
        {
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            return groupProfile is null ?
                 new List<Guid>()
                 :
                 groupProfile.MembersId.Select(x => x.ProfileId);
        }

        public GroupProfile GetProfileById(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id);
        }

        public string GetProfileDescription(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).Description;
        }

        public string GetProfilePhoto(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfilePhoto;
        }

        public int GetProfileViews(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ProfileViews;
        }

        public string GetVideoLink(Guid id)
        {
            return _context.GroupProfiles.FirstOrDefault(g => g.Id == id).VideoLink;
        }

        public bool IsManager(Guid id, Guid userId)
        {
            var managersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).ManagersId;

            return managersList.Any(x => x.UserProfileId == userId);
        }

        public bool IsMember(Guid id, Guid userId)
        {
            var membersList = _context.GroupProfiles.FirstOrDefault(g => g.Id == id).MembersId;
            return membersList.Any(x => x.ProfileId == userId);
        }

        public bool SignInFollower(Guid id, Guid profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if (followerProfile is null)
                return false;

            _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Add(followerProfile);
            return Save();
        }

        public bool SignInManager(Guid id, Guid profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);

            if (managerProfile is null || groupProfile is null)
                return false;

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

        public bool SignInMember(Guid id, Guid profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile is null || groupProfile is null)
                return false;

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

        public bool SignOutFollower(Guid id, Guid profileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);

            if (followerProfile == null)
                return false;

            _context.GroupProfiles.FirstOrDefault(g => g.Id == id).FollowersId.Remove(followerProfile);
            return Save();
        }

        public bool SignOutManager(Guid id, Guid profileId)
        {
            var managerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (managerProfile is null && groupProfile is null)
                return false;

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

        public bool SignOutMember(Guid id, Guid profileId)
        {
            var memberProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == id);


            if (memberProfile is null && groupProfile is null)
                return false;

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


        public GroupProfile UpdateGroupProfile(GroupProfile groupProfile)
        {
            var groupProfileDb = _context.GroupProfiles.FirstOrDefault(g => g.Id == groupProfile.Id);
            if (groupProfileDb is null)
                return groupProfileDb;


            groupProfileDb = groupProfile;
            return Save() ? groupProfile : groupProfileDb;
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ?
                true 
                :
                false;
        }
    }
}
