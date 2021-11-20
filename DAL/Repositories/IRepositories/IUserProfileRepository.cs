using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserProfileRepository
    {
        bool CreateProfile(UserProfile userProfile);
        bool UpdateProfile(ProfileDto profileDto);
        bool AddProfilePhotoBase64(string photoBase64, int userId);
        bool DoesProfileExist(int userId);
        bool DeleteProfile(int id);
        ProfileDto GetProfileById(int id);
        ProfileDto GetProfileByUserId(int userId);
        bool AddProfileView(int id);
        string GetProfileDescription(int id);
        string GetProfilePhoto(int id);
        int GetProfileViews(int id);
        int GetUserIdByProfileId(int id);
        string GetVideoLink(int id);
        bool SignInFollower(int id, int followerProfileId);
        bool SignOutFollower(int id, int followerProfileId);
        IEnumerable<HashTag> GetUserHashTags(int profileId);
        IEnumerable<string> GetProfileHashTagsNames(int profileId);
        bool AddHashTagToProfileAccount(int hashTagId, int profileId);
        bool AddHashTagByNameToUserProfile(string hashTagName, int profileId);
        bool AddProfileHashtagById(int profileId, int HashTagid);
        bool RemoveHashTagByNameFromUserProfile(string hashTagName, int userProfileId);
        bool AddUserGroupByIdToProfile(int profileId, int groupId);
        IEnumerable<int> GetUserGroupsIdListByProfileId(int profileId);

        public bool Save();
    }
}
