using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserCRUD.Models;

namespace UserCRUD.Models
{
    public interface IUserRepository
    {
        void AddUser(UserModel user);
        IEnumerable<UserModel> GetAll();
        UserModel GetUserById(int id);
        void UpdateUser(UserModel user);
        void DeleteUser(int id);
        void Save();
    }

}

