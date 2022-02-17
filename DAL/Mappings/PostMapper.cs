using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace HobbistApi.Mappings
{
    public static class PostMapper
    {
        public static Post MapPostDtoToPost(PostDto postDto, UserProfile userProfile, HashTag hashTag)
        {
            return new Post()
            {
                Id = postDto.Id,
                ChainedTag = hashTag,
                PostMessage = postDto.PostMessage,
                Lat = postDto.Lat,
                Lng = postDto.Lng,
                PostViews = postDto.PostViews,
                BeginDate = postDto.BeginDate,
                DayLast = postDto.DayLast,
                UserProfileId = postDto.ProfileId,
                UserProfile = userProfile,
                IsBlocked = postDto.IsBlocked
                
            };
        }

        public static PostDto MapPostToPostDto(Post post, int averageMark, Guid requestingUserId)
        {
            return new PostDto()
            {
                Id = post.Id,
                ChainedTagName = post.ChainedTag.HashTagName,
                PostMessage = post.PostMessage,
                PostViews = post.PostViews,
                AverageMark = averageMark,
                DayLast = post.DayLast,
                BeginDate = post.BeginDate,
                ProfileId = post.UserProfileId,
                IsBlocked = post.IsBlocked,

                Lat = post.Lat,
                Lng = post.Lng,
                IsFollowed = CheckIfPostIsfollowed(post.FollowersList, requestingUserId)
            };
        }

        private static bool CheckIfPostIsfollowed(List<Guid> followers, Guid userId)
        {
            foreach(var follower in followers) { if (follower == userId) return true; }
            return false;
        }
    }
}
