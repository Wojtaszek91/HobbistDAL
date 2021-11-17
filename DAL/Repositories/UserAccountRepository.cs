using DAL.DataContext;
using DAL.Repositories.IRepositories;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.Models;
using Models.Models.DTOs;
using Models.Models.EntityFrameworkJoinEntities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace DAL.Repositories
{
    public class UserAccountRepository : IUserAccountRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly AppSettings _appSettings;

        public UserAccountRepository(ApplicationDbContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse AuthenticateUser(LoginDetails loginDetails)
        {
            var user = _context.UserAccounts.SingleOrDefault
                (u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);
            if (user == null) return null;

            var expiresIn = DateTime.UtcNow.AddDays(1);
            var tokenHanlder = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptior = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                        new Claim(ClaimTypes.Name, user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                Expires = expiresIn,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHanlder.CreateToken(tokenDescriptior);
            user.Token = tokenHanlder.WriteToken(token);
            user.Password = "";

            return new AuthenticateResponse()
            {
                Emial = user.Email,
                AccountId = user.Id,
                Role = user.Role,
                Token = user.Token,
                TokenExpirationDate = expiresIn
            };

        }

        public bool ChangeEmail(LoginDetails loginDetails, string newEmail)
        {
            var userAccount = _context.UserAccounts.FirstOrDefault(a =>
                a.Email == loginDetails.Email
                && a.Password == loginDetails.Password);

            if (userAccount != null) { userAccount.Email = newEmail; return Save(); }
            else { return false; }
        }

        public string GetUserEmial(int id) => _context.UserAccounts.FirstOrDefault(u => u.Id == id).Email;

        public string GetUserUsernameById(int id) => _context.UserAccounts.FirstOrDefault(u => u.Id == id).Username;

        public bool IsUserEmailAvailable(string email)
        {
            try
            {
                var users = _context.UserAccounts.OrderBy(u => u.Email).ToList();
                foreach (var user in users)
                {
                    if (user.Email == email)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                // log exce
                return false;
            }
        }

        public string GetUserRole(int id) => _context.UserAccounts.FirstOrDefault(u => u.Id == id).Role;

        public bool IsUserNameAvailable(string username)
        {
            try
            {
                var users = _context.UserAccounts.OrderBy(u => u.Username).ToList();
                foreach (var user in users)
                {
                    if (user.Username == username)
                    {
                        return false;
                    }
                }
                return true;
            }
            catch(Exception e)
            {
                //log exception
                return false;
            }
        }

        /// <summary>
        /// Method can be called only if username and email has been check to do not duplicate.
        /// </summary>
        /// <param name="userAccount">Details of new account</param>
        /// <returns>Created userAccount or empty if fail.</returns>    
        public UserAccount RegisterUser(UserAccount userAccount)
        {
            try
            {
                UserAccount newUser = new UserAccount()
                {
                    Username = "",
                    Password = userAccount.Password,
                    Email = userAccount.Email,
                    DateOfBirth = userAccount.DateOfBirth,
                    isBlocked = false,// block in future to make email authentication.
                    Role = "User",
                    UserAccountHashTags = new List<UserAccountHashTag>(),
                    GroupManagers = new List<GroupProfileManagers>(),
                    GroupProfiles = new List<GroupProfileUserAccount>()
                };

                _context.UserAccounts.Add(newUser);

                return Save() ? newUser : new UserAccount();            
            }
            catch (Exception e)
            {
                // log in future.
                return new UserAccount();
            }

        }

        public bool RemoveUser(LoginDetails loginDetails)
        {
            var userToRemove = _context.UserAccounts.FirstOrDefault
                (u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);

            if (userToRemove == null && userToRemove.isBlocked) return false;// avoid delete account of blocked user.
            _context.UserAccounts.Remove(userToRemove);
            return Save();
        }

        public bool UpdateUserDateOfBirth(LoginDetails loginDetails, DateTime dateOfBirth)
        {
            var userFromDb = _context.UserAccounts.FirstOrDefault
                (u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);

            if (userFromDb == null) return false;
            userFromDb.DateOfBirth = dateOfBirth;
            return Save();
        }

        public bool AddProfileToAccount(UserProfile userProfile, int userAccountId)
        {
            var user = _context.UserAccounts.FirstOrDefault(u => u.Id == userAccountId);
            if (user != null)
            {
                user.UserProfile = userProfile;
                user.UserProfileId = userProfile.Id;
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










        public bool AddUserGroupId(int userId, int groupId)
        {
            var userFromDb = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            if (userFromDb != null)
            {
                var groupProfile = _context.GroupProfiles.FirstOrDefault(g => g.Id == groupId);
                if (groupProfile != null)
                {
                    GroupProfileUserAccount groupUser = new GroupProfileUserAccount()
                    {
                        UserAccount = userFromDb,
                        UserAccountId = userFromDb.Id,
                        GroupProfile = groupProfile,
                        GroupProfileId = groupProfile.Id
                    };
                    userFromDb.GroupProfiles.Add(groupUser);
                    groupProfile.MembersId.Add(groupUser);
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

        public bool AddUserHashTag(int userId, int HashTagid)
        {
            var userFromDb = _context.UserAccounts.FirstOrDefault(u => u.Id == userId);
            if (userFromDb != null)
            {
                var hashTag = _context.HashTags.FirstOrDefault(h => h.Id == HashTagid);
                if (hashTag != null)
                {
                    UserAccountHashTag userHashTag = new UserAccountHashTag()
                    {
                        UserAccount = userFromDb,
                        UserAccountId = userFromDb.Id,
                        HashTag = hashTag,
                        HashTagId = hashTag.Id
                    };
                    userFromDb.UserAccountHashTags.Add(userHashTag);
                    hashTag.UserAccountHashTags.Add(userHashTag);
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

        public bool BlockOrUnblockUser(string userName, bool isBlocked)
        {
            var userAccount = _context.UserAccounts.FirstOrDefault(a => a.Username == userName);

            if (userAccount != null)
            {
                userAccount.isBlocked = isBlocked;
                return Save();
            }
            else
            {
                return false;
            }
        }

        public UserAccount GetUserById(int id)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Id == id);
        }

        public UserAccount GetUserByUserName(string username)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Username == username);
        }

        public DateTime GetUserDateOfBirth(int id)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Id == id).DateOfBirth;
        }

        public IEnumerable<int> GetUserGroupsIdList(int id)
        {
            var groupProfiles = _context.UserAccounts.FirstOrDefault(u => u.Id == id).GroupProfiles;
            List<int> groupIdsList = new List<int>();
            foreach (var group in groupProfiles)
            {
                groupIdsList.Add(group.GroupProfileId);
            }
            return groupIdsList;
        }

        public IEnumerable<HashTag> GetUserHashTags(int id)
        {
            var hashTags = _context.UserAccounts.FirstOrDefault(u => u.Id == id).UserAccountHashTags;
            List<HashTag> hashTagsList = new List<HashTag>();
            foreach (var hashTag in hashTags)
            {
                hashTagsList.Add(hashTag.HashTag);
            }
            return hashTagsList;
        }

        public IEnumerable<string> GetUserHashTagsNames(int userId)
        {
            List<string> hashTagsList = new List<string>();
            foreach (var hashTag in _context.UserAccountHashTags)
            {
                if (hashTag.UserAccountId == userId) hashTagsList.Add(_context.HashTags.FirstOrDefault(x => x.Id == hashTag.HashTagId).HashTagName);
            }
            return hashTagsList;
        }

        public bool IsUserBlocked(int id)
        {
            return _context.UserAccounts.FirstOrDefault(u => u.Id == id).isBlocked;
        }

        public bool UpdateUsername(int accId, string newUsername)
        {
            var userAcc = _context.UserAccounts.FirstOrDefault(x => x.Id == accId);
            if (userAcc == null) return false;
            userAcc.Username = newUsername;
            return Save();
        }

        public bool AddHashTagToUserAccount(int hashTagId, int userAccId)
        {
            var userAccount = _context.UserAccounts.FirstOrDefault(x => x.Id == userAccId);
            var hashTag = _context.HashTags.FirstOrDefault(x => x.Id == hashTagId);
            if (userAccount == null || hashTag == null) return false;

            var relation = new UserAccountHashTag() { UserAccount = userAccount, UserAccountId = userAccId, HashTag = hashTag, HashTagId = hashTagId };

            if (userAccount.UserAccountHashTags == null) userAccount.UserAccountHashTags = new List<UserAccountHashTag>() { relation };
            else userAccount.UserAccountHashTags.Add(relation);
            _context.UserAccounts.Update(userAccount);

            if (hashTag.UserAccountHashTags == null) hashTag.UserAccountHashTags = new List<UserAccountHashTag>() { relation };
            else hashTag.UserAccountHashTags.Add(relation);
            _context.HashTags.Update(hashTag);

            _context.UserAccountHashTags.Add(relation);
            return Save();
        }

        public bool AddHashTagToUserAccByName(string hashTagName, int userAccId)
        {
            // add check if already isnt added !
            var hashTag = _context.HashTags.FirstOrDefault(x => x.HashTagName.ToUpper() == hashTagName.ToUpper());
            if (hashTag == null) return false;
            return AddHashTagToUserAccount(hashTag.Id, userAccId);
        }

        public bool RemoveHashTagFromAccByName(string hashTagName, int userAccId)
        {
            var userAcc = _context.UserAccounts.FirstOrDefault(x => x.Id == userAccId);
            var accHashtag = _context.UserAccountHashTags.FirstOrDefault(x => x.HashTag.HashTagName == hashTagName && x.UserAccountId == userAccId);
            var hashtag = _context.HashTags.FirstOrDefault(x => x.HashTagName == hashTagName);
            if (userAcc == null || accHashtag == null || hashtag == null) return false;

            try
            {
                userAcc.UserAccountHashTags.Remove(accHashtag);
                hashtag.UserAccountHashTags.Remove(accHashtag);
                _context.UserAccountHashTags.Remove(accHashtag);

                return Save();
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
