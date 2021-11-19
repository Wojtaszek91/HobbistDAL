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

        public UserAccountRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public AuthenticateResponse AuthenticateUser(LoginDetails loginDetails, string key)
        {
            var user = _context.UserAccounts.SingleOrDefault
                (u => u.Email == loginDetails.Email && u.Password == loginDetails.Password);
            if (user == null) return null;

            var expiresIn = DateTime.UtcNow.AddDays(1);
            var tokenHanlder = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(key);
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
        public string GetUserRole(int id) => _context.UserAccounts.FirstOrDefault(u => u.Id == id).Role;

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
                    Role = "User"
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

        public bool BlockOrUnblockUser(string userName, bool isBlocked)
        {
            var userAccount = _context.UserAccounts.FirstOrDefault(a => a.Username == userName);

            if (userAccount == null) return false;

            userAccount.isBlocked = isBlocked;
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0 ? true : false;
        }
    }
}
