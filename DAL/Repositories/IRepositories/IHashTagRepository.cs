
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
        List<HashTagDto> GetAllHashtags();
        IEnumerable<string> GetAllHashTagNames();
        HashTagDto GetHashTagById(int id);
        int GetHashTagPopularity(int id);
        HashTag GetHashTagByName(string name);
        int GetHashTagPopularityByName(string name);
        bool AddPopularity(int id);
        bool DecreasePopuplarity(int id);
        bool DoesHashTagExists(int id);
        bool DeleteHashTag(int id);
        bool AddHashTag(string h);
        bool RemoveHashTag(int id);
        HashTagDto EditHashTag(HashTagDto h);
        bool EditHashTagNoReturnType(HashTagDto h);
        bool Save();
    }
}
