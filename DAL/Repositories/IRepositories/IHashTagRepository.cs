
using Models.Models;
using Models.Models.EntityFrameworkJoinEntities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IHashTagRepository
    {
        public List<HashTagDto> GetAllHashtags();
        public HashTagDto GetHashTagById(int id);
        public int GetHashTagPopularity(int id);
        public HashTag GetHashTagByName(string name);
        public int GetHashTagPopularityByName(string name);
        public bool AddPopularity(int id);
        public bool DecreasePopuplarity(int id);
        public bool DoesHashTagExists(int id);
        public bool DeleteHashTag(int id);
        public bool AddHashTag(string h);
        public bool RemoveHashTag(int id);
        public HashTagDto EditHashTag(HashTagDto h);
        public bool EditHashTagNoReturnType(HashTagDto h);
        public bool Save();
    }
}
