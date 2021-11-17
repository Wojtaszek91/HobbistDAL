using Models.Models;
using Models.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories.IRepositories
{
    public interface IUserAccountRepository
    {
        public UserAccount GetUserById(int id);
        public UserAccount GetUserByUserName(string username);
        public string GetUserRole(int id);
        public string GetUserEmial(int id);
        public string GetUserUsernameById(int id);
        public DateTime GetUserDateOfBirth(int id);
        public IEnumerable<HashTag> GetUserHashTags(int id);
        public IEnumerable<string> GetUserHashTagsNames(int id);
        public IEnumerable<int> GetUserGroupsIdList(int id);
        public bool AddProfileToAccount(UserProfile userProfile, int userAccountId);
        public bool IsUserBlocked(int id);
        public bool BlockOrUnblockUser(string userName, bool isBlocked);
        public bool IsUserNameAvailable(string username);
        public bool IsUserEmailAvailable(string email);
        public AuthenticateResponse AuthenticateUser(LoginDetails loginDetails);
        public UserAccount RegisterUser(UserAccount userAccount);
        public bool RemoveUser(LoginDetails loginDetails);
        public bool ChangeEmail(LoginDetails loginDetails, string newEmail);
        public bool AddUserGroupId(int userId, int groupId);
        public bool AddUserHashTag(int userId, int HashTagid);
        public bool UpdateUsername(int accId, string newUsername);
        public bool UpdateUserDateOfBirth(LoginDetails loginDetails, DateTime dateOfBirth);
        public bool AddHashTagToUserAccount(int hashTagId, int userAccId);
        public bool AddHashTagToUserAccByName(string hashTagName, int userAccId);
        public bool RemoveHashTagFromAccByName(string hashTagName, int userAccId);
        public bool Save();

    }
}
