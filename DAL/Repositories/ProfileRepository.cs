using DAL.DataContext;
using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProfileRepository : IUserProfileRepository
    {
        private readonly ApplicationDbContext _context;
        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddProfileView(int id)
        {
            _context.UserProfiles.FirstOrDefault(p => p.Id == id).ProfileViews++;
            return Save();
        }

        public bool CreateProfile(UserProfile userProfile)
        {
            _context.UserProfiles.Add(userProfile);
            return Save();
        }

        public bool AddProfilePhotoBase64(string photoBase64, int userId)
        {
            var user = _context.UserProfiles.FirstOrDefault(x => x.UserAccountId == userId);
            if (user == null) return false;
            user.ProfilePhoto = photoBase64;
            return Save();
        }

        public bool DeleteProfile(int id)
        {
            var userProfile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (userProfile != null)
            {
                var userProfileWithUserAccount = _context.UserProfiles
                    .Include(ua => ua.UserAccount)
                  .Where(p => p.Id == id);

                _context.RemoveRange(userProfileWithUserAccount);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public ProfileDto GetProfileById(int id)
        {
            var profileFromDb = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
            if (profileFromDb != null)
            {
                return ProfileMapper.MapProfileToProfileDto(profileFromDb);
            }
            else
            {
                return null;
            }
        }

        public ProfileDto GetProfileByUserId(int userId)
        {
            var profileFromDb = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userId);
            if (profileFromDb != null)
            {
                return ProfileMapper.MapProfileToProfileDto(profileFromDb);
            }
            else
            {
                return null;
            }
        }

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

        public bool DoesProfileExist(int userId)
        {
            if (_context.UserProfiles.FirstOrDefault(p => p.UserAccountId == userId) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SignInFollower(int id, int userId)
        {
            var followerAccount = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            if (followerAccount != null)
            {
                var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
                if (profile != null)
                {
                    profile.FollowersId.Add(followerAccount);
                    return Save();
                }
                else
                {
                    return false;
                }
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
                var profile = _context.UserProfiles.FirstOrDefault(p => p.Id == id);
                if (profile != null)
                {
                    profile.FollowersId.Remove(followerAccount);
                    return Save();
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateProfile(ProfileDto profileDto)
        {
            var profile = _context.UserProfiles.FirstOrDefault(p => p.UserAccountId == profileDto.UserAccountId);
            if (profile != null)
            {
                profile.Description = profileDto.Description;
                profile.ProfilePhoto = profileDto.ProfilePhoto;
                profile.VideoLink = profileDto.VideoLink;

                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
