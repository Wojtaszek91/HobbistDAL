﻿using Models.Models;
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
        public static Post MapPostDtoToPost(PostDto postDto, UserProfile userAccount, HashTag hashTag)
        {
            return new Post()
            {
                Id = postDto.Id,
                ChainedTag = hashTag,
                PostMessage = postDto.PostMessage,
                Lat = postDto.Lat,
                Lng = postDto.Lng,
                PostViews = postDto.PostViews,
                AverageMark = postDto.AverageMark,
                BeginDate = postDto.BeginDate,
                DayLast = postDto.DayLast,
                UserId = postDto.AccountId,
                UserAccount = userAccount,
                IsBlocked = postDto.IsBlocked
                
            };
        }

        public static List<PostDto> MapCollectionPostToPostDto(IEnumerable<Post> postCollection, int requestingUserId)
        {
            List<PostDto> postDtoCollection = new List<PostDto>();
            foreach (var post in postCollection)
            {
                PostDto postDto = new PostDto()
                {
                    Id = post.Id,
                    ChainedTagName = post.ChainedTag.HashTagName,
                    PostMessage = post.PostMessage,
                    Lat = post.Lat,
                    Lng = post.Lng,
                    PostViews = post.PostViews,
                    AverageMark = post.AverageMark,
                    DayLast = post.DayLast,
                    BeginDate = post.BeginDate,
                    AccountId = post.UserId,
                    IsFollowed = CheckIfPostIsfollowed(post.FollowersList, requestingUserId),
                    IsBlocked = post.IsBlocked
                };
                postDtoCollection.Add(postDto);
            }
            return postDtoCollection;
        }

        private static bool CheckIfPostIsfollowed(List<int> followers, int userId)
        {
            foreach(var follower in followers) { if (follower == userId) return true; }
            return false;
        }
    }
}