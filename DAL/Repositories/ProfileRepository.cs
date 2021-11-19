using DAL.DataContext;
using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProfileRepository : IUserProfileRepository // update interface after refactor
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region BasicCRUD
        public bool CreateProfile(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            return Save();
        }

        public bool UpdateProfile(ProfileDto profileDto)
        {
            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == profileDto.UserAccountId);
            if (profile == null) return false;

            profile.Description = profileDto.Description;
            profile.ProfilePhoto = profileDto.ProfilePhoto;
            profile.VideoLink = profileDto.VideoLink;

            return Save();
        }

        public bool AddProfilePhotoBase64(string photoBase64, int userId)
        {
            var user = _context.UserProfiles.FirstOrDefault(x => x.UserAccountId == userId);
            if (user == null) return false;

            user.ProfilePhoto = photoBase64;

            return Save();
        }

        public bool DoesProfileExist(int userId) => _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userId) == null ? false : true;

        public bool DeleteProfile(int id)
        {
            var userProfile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (userProfile == null) return false;

            var userProfileWithUserAccount = _context.UserProfiles.Include(ua => ua.UserAccount).Where(p => p.Id == id);
            _context.RemoveRange(userProfileWithUserAccount);

            return Save();
        }

        public ProfileDto GetProfileById(int id)
        {
            var profileFromDb = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (profileFromDb == null) return null;

            return ProfileMapper.MapProfileToProfileDto(profileFromDb);
        }

        public ProfileDto GetProfileByUserId(int userId)
        {
            var profileFromDb = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userId);
            if (profileFromDb == null) return null;

            return ProfileMapper.MapProfileToProfileDto(profileFromDb);
        }

        public bool AddProfileView(int id)
        {
            _context.UserProfiles.FirstOrDefault(p => p.Id == id).ProfileViews++;
            return Save();
        }

        #endregion BasicCRUD

        #region GetProfileParts
        public string GetProfileDescription(int id)
        {
            return _context.UserProfiles.FirstOrDefault(p => p.Id == id).Description;
        }

        public string GetProfilePhoto(int id)
        {
            return _context.UserProfiles.FirstOrDefault(p => p.Id == id).ProfilePhoto;
        }

        public int GetProfileViews(int id)
        {
            return _context.UserProfiles.FirstOrDefault(p => p.Id == id).ProfileViews;
        }

        public int GetUserIdByProfileId(int id)
        {
            return _context.UserProfiles.FirstOrDefault(u => u.Id == id).UserAccountId;
        }

        public string GetVideoLink(int id)
        {
            return _context.UserProfiles.FirstOrDefault(u => u.Id == id).VideoLink;
        }
        #endregion GetProfileParts

        #region Followers
        public bool SignInFollower(int id, int followerProfileId)
        {
            var followerProfile = _context.UserProfiles.FirstOrDefault(u => u.Id == followerProfileId);
            if (followerProfile == null) return false;

            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) return false;

            profile.FollowersId.Add(followerProfile);
            return Save();
        }

        public bool SignOutFollower(int id, int followerProfileId)
        {
            var followerAccount = _context.UserProfiles.FirstOrDefault(u => u.Id == followerProfileId);
            if (followerAccount == null) return false;

            var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (profile == null) return false;

            profile.FollowersId.Remove(followerAccount);
            return Save();
        }
        #endregion Followers

        #region HashTag
        public IEnumerable<HashTag> GetUserHashTags(int profileId)
        {
            var hashTags = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId).UserProfileHashTags;
            List<HashTag> hashTagsList = new List<HashTag>();
            foreach (var hashTag in hashTags)
            {
                hashTagsList.Add(hashTag.HashTag);
            }
            return hashTagsList;
        }

        public IEnumerable<string> GetProfileHashTagsNames(int profileId)
        {
            List<string> hashTagsList = new List<string>();
            foreach (var hashTag in _context.UserProfileHashTags)
            {
                if (hashTag.UserProfileId == profileId) hashTagsList.Add(_context.HashTags.FirstOrDefault(x => x.Id == hashTag.HashTagId).HashTagName);
            }
            return hashTagsList;
        }

        public bool AddHashTagToProfileAccount(int hashTagId, int profileId)
        {
            var hashTag = _context.HashTags.FirstOrDefault(x => x.Id == hashTagId);
            var profile = _context.UserProfiles.FirstOrDefault(x => x.Id == profileId);

            if (profile == null || hashTag == null) return false;

            var relation = new UserProfileHashTag() { UserProfile = profile, UserProfileId = profileId, HashTag = hashTag, HashTagId = hashTagId };

            if (profile.UserProfileHashTags == null) profile.UserProfileHashTags = new List<UserProfileHashTag>() { relation };
            else profile.UserProfileHashTags.Add(relation);

            _context.UserProfiles.Update(profile);

            if (hashTag.UserAccountHashTags == null) hashTag.UserAccountHashTags = new List<UserProfileHashTag>() { relation };
            else hashTag.UserAccountHashTags.Add(relation);

            _context.HashTags.Update(hashTag);

            _context.UserProfileHashTags.Add(relation);

            return Save();
        }

        public bool AddHashTagByNameToUserProfile(string hashTagName, int profileId)
        {
            // add check if already isnt added !
            var hashTag = _context.HashTags.FirstOrDefault(x => x.HashTagName.ToUpper() == hashTagName.ToUpper());
            if (hashTag == null) return false;
            return AddHashTagToProfileAccount(hashTag.Id, profileId);
        }

        public bool AddProfileHashtagById(int profileId, int HashTagid)
        {
            var profile = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId);
            if (profile == null) return false;

            var hashTag = _context.HashTags.FirstOrDefault(h => h.Id == HashTagid);
            if (hashTag == null) return false;
            {
                UserProfileHashTag userHashTag = new UserProfileHashTag()
                {
                    UserProfile = profile,
                    UserProfileId = profile.Id,
                    HashTag = hashTag,
                    HashTagId = hashTag.Id
                };
                profile.UserProfileHashTags.Add(userHashTag);
                hashTag.UserAccountHashTags.Add(userHashTag);
                return Save();
            }
        }

        public bool RemoveHashTagByNameFromUserProfile(string hashTagName, int userProfileId)
        {
            var profile = _context.UserProfiles.FirstOrDefault(x => x.Id == userProfileId);
            var accHashtag = _context.UserProfileHashTags.FirstOrDefault(x => x.HashTag.HashTagName == hashTagName && x.UserProfileId == userProfileId);
            var hashtag = _context.HashTags.FirstOrDefault(x => x.HashTagName == hashTagName);

            if (profile == null || accHashtag == null || hashtag == null) return false;

            try
            {
                profile.UserProfileHashTags.Remove(accHashtag);
                hashtag.UserAccountHashTags.Remove(accHashtag);
                _context.UserProfileHashTags.Remove(accHashtag);

                return Save();
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion HashTag

        #region Group
        public bool AddUserGroupByIdToProfile(int profileId, int groupId)
        {
            var profileFromDb = _context.UserProfiles.FirstOrDefault(p => p.Id == profileId);
            if (profileFromDb == null) return false;

            var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == groupId);
            if (groupProfile == null) return false;

            GroupProfileUserProfile groupUser = new GroupProfileUserProfile()
            {
                UserProfile = profileFromDb,
                ProfileId = profileFromDb.Id,
                GroupProfile = groupProfile,
                GroupProfileId = groupProfile.Id
            };

            profileFromDb.GroupProfiles.Add(groupUser);
            groupProfile.MembersId.Add(groupUser);

            return Save();
        }

        public IEnumerable<int> GetUserGroupsIdListByProfileId(int profileId)
        {
            var groupProfiles = _context.UserProfiles.FirstOrDefault(u => u.Id == profileId).GroupProfiles;
            List<int> groupIdsList = new List<int>();
            foreach (var group in groupProfiles)
            {
                groupIdsList.Add(group.GroupProfileId);
            }
            return groupIdsList;
        }

        #endregion Group

        public bool Save() => _context.SaveChanges() >= 0 ? true : false;
    }
}
