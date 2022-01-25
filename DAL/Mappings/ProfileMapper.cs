using Models.Models;
using Models.Models.DTOs.Profile;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HobbistApi.Mappings
{
    public static class ProfileMapper
    {
        public static UpsertProfileDto MapProfileToProfileDto(UserProfile profile)
        {
            return new UpsertProfileDto()
            { 
                Username = profile.Username,
                Description = profile.Description,
                VideoLink = profile.VideoLink,
                ProfilePhoto = profile.ProfilePhoto,
                UserAccountId = profile.UserAccountId
            };
        }

        public static UserProfile MapProfileDtoToProfile(UpsertProfileDto profileDto)
        {
        return new UserProfile()
        {
            Username = profileDto.Username,
                Description = profileDto.Description,
                VideoLink = profileDto.VideoLink,
                ProfilePhoto = profileDto.ProfilePhoto,
                UserAccountId = profileDto.UserAccountId
            };
        }
    }
}
