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
                ProfileId = profile.UserAccountId,
                HashtagNames = GetHashtagStringList(profile.HashTags.ToList())
            };
        }

        public static List<string> GetHashtagStringList(List<HashTag> hashtagList)
        {
            List<string> hashNamesList = new List<string>();
            foreach(var hashTag in hashtagList)
            {
                hashNamesList.Add(hashTag.HashTagName);
            }

            return hashNamesList;
        }

        public static UserProfile MapProfileDtoToProfile(UpsertProfileDto profileDto)
        {
        return new UserProfile()
        {
            Username = profileDto.Username,
                Description = profileDto.Description,
                VideoLink = profileDto.VideoLink,
                ProfilePhoto = profileDto.ProfilePhoto,
                UserAccountId = profileDto.ProfileId
            };
        }
    }
}
