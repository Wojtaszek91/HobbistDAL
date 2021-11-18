using DAL.DataContext;
using DAL.Repositories.IRepositories;
using HobbistApi.Mappings;
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class HashTagRepository : IHashTagRepository
    {
        private readonly ApplicationDbContext _context;
        public HashTagRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<HashTagDto> GetAllHashtags()
        {
            List<HashTagDto> hashtagDtoList = new List<HashTagDto>();
            foreach (var hashTag in _context.HashTags.ToList()) { hashtagDtoList.Add(HashtagMapper.MapHashTagToDto(hashTag)); }

            return hashtagDtoList;
        }

        public bool AddHashTag(string h)
        {
            if (string.IsNullOrEmpty(h)) return false;

            var newTag = new HashTag()
            {
                HashTagName = h,
                Popularity = 0
            };
            _context.HashTags.Add(newTag);

            return Save();
        }

        public bool AddPopularity(int id)
        {
            var dbHash = _context.HashTags.FirstOrDefault(h => h.Id == id);
            dbHash.Popularity++;
            return Save();
        }

        public bool DecreasePopuplarity(int id)
        {
            var dbHash = _context.HashTags.FirstOrDefault(h => h.Id == id);
            dbHash.Popularity--;
            return Save();
        }

        public HashTagDto EditHashTag(HashTagDto h)
        {
            if (h != null)
            {
                var tagFromDb = _context.HashTags.FirstOrDefault(dbH => dbH.HashTagName == h.HashTagName);
                tagFromDb.HashTagName = h.HashTagName;
                _context.HashTags.Update(tagFromDb);
            }
            return Save() ? h : null;
        }

        public bool EditHashTagNoReturnType(HashTagDto h)
        {
            if (EditHashTag(h) != null)
            {
                return Save();
            }
            return false;
        }

        public bool DoesHashTagExists(int id)
        {
            if (_context.HashTags.FirstOrDefault(h => h.Id == id) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteHashTag(int id)
        {
            var hashtag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            if (hashtag != null)
            {
                _context.HashTags.Remove(hashtag);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public HashTagDto GetHashTagById(int id)
        {
            var tagFromDb = _context.HashTags.FirstOrDefault(h => h.Id == id);
            if (tagFromDb != null)
            {
                HashTagDto tagDto = new HashTagDto()
                {
                    HashTagName = tagFromDb.HashTagName,
                    Popularity = tagFromDb.Popularity
                };
                return tagDto;
            }
            else
            {
                return null;
            }
        }

        public HashTag GetHashTagByName(string name)
        {
            return _context.HashTags.FirstOrDefault(h => h.HashTagName == name);
        }

        public int GetHashTagPopularity(int id)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            return dbHashTag.Popularity;
        }

        public int GetHashTagPopularityByName(string name)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.HashTagName == name);
            return dbHashTag.Popularity;
        }

        public bool RemoveHashTag(int id)
        {
            var dbHashTag = _context.HashTags.FirstOrDefault(h => h.Id == id);
            _context.HashTags.Remove(dbHashTag);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
