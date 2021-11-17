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
        public ProfileDto GetProfileById(int id);
        public ProfileDto GetProfileByUserId(int userId);
        public string GetProfileDescription(int id);
        public string GetVideoLink(int id);
        public string GetProfilePhoto(int id);
        public int GetProfileViews(int id);
        public int GetUserIdByProfileId(int id);
        public bool CreateProfile(UserProfile userProfile);
        public bool AddProfilePhotoBase64(string photoBase64, int userId);
        public bool DeleteProfile(int id);
        public bool UpdateProfile(ProfileDto userProfile);
        public bool DoesProfileExist(int id);
        public bool AddProfileView(int id);
        public bool SignInFollower(int id, int userId);
        public bool SignOutFollower(int id, int userId);
        public bool Save();
    }
}
