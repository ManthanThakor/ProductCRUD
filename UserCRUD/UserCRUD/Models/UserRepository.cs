using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;

namespace UserCRUD.Models
{
    public class UserRepository : IUserRepository
    {
        private DataClasses1DataContext _dataClasses1DataContext;

        public UserRepository()
        {
            string connectionString = "Data Source=TGS147\\SQLEXPRESS;Initial Catalog=UserCrud;Integrated Security=True;";
            _dataClasses1DataContext = new DataClasses1DataContext(connectionString);
        }

        public void AddUser(UserModel user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Address = user.Address,
            };
            _dataClasses1DataContext.Users.InsertOnSubmit(newUser);
            Save();
        }

        public IEnumerable<UserModel> GetAll()
        {
            var users = _dataClasses1DataContext.Users
                                   .Select(u => new UserModel
                                   {
                                       Id = u.Id,
                                       Name = u.Name,
                                       Address = u.Address
                                   })
                                   .ToList();
            return users;
        }

        public UserModel GetUserById(int id)
        {
            var user = _dataClasses1DataContext.Users.SingleOrDefault(u => u.Id == id);
            if (user == null)
            {
                return null;
            }

            var userModel = new UserModel
            {
                Id = user.Id,
                Name = user.Name,
                Address = user.Address
            };
            return userModel;
        }

        public void UpdateUser(UserModel user)
        {
            var exisitingUser = _dataClasses1DataContext.Users.SingleOrDefault(u => u.Id == user.Id);
            if (exisitingUser != null)
            {
                exisitingUser.Name = user.Name;
                exisitingUser.Address = user.Address;
                Save();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _dataClasses1DataContext.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dataClasses1DataContext.Users.DeleteOnSubmit(user);
                Save();
            }
        }

        public void Save()
        {
            _dataClasses1DataContext.SubmitChanges();
        }

        public void Dispose()
        {
            if (_dataClasses1DataContext != null)
            {
                _dataClasses1DataContext.Dispose();
            }
        }

    }
}