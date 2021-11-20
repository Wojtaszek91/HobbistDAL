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
        AuthenticateResponse AuthenticateUser(LoginDetails loginDetails);
        bool ChangeEmail(LoginDetails loginDetails, string newEmail);
        string GetUserEmial(int id);
        string GetUserRole(int id);
        bool IsUserEmailAvailable(string email);
        UserAccount RegisterUser(UserAccount userAccount);
        bool RemoveUser(LoginDetails loginDetails);
        bool UpdateUserDateOfBirth(LoginDetails loginDetails, DateTime dateOfBirth);
        bool AddProfileToAccount(UserProfile userProfile, int userAccountId);
        bool IsUserBlocked(int id);
        UserAccount GetUserById(int id);
        DateTime GetUserDateOfBirth(int id);
        bool BlockOrUnblockUser(string userName, bool isBlocked);
        public bool Save();

    }
}
