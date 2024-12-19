using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UserCRUD;
using UserCRUD.Models;

namespace UserCRUD.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }
}



namespace UserRegistration.Repositories
{
    public class UserRepository : IUserRepository
    {
        private UserDataContext _dataContext;

        public UserRepository()
        {
            string connectionString = "Data Source=TGS147\\SQLEXPRESS;Initial Catalog=UserRegistration;Integrated Security=True;";
            _dataContext = new UserDataContext(connectionString);
        }

        public void AddUser(UserModel user)
        {
            var newUser = new User
            {
                Name = user.Name,
                Address = user.Address
            };

            _dataContext.Users.InsertOnSubmit(newUser);
            Save();
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = _dataContext.Users
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
            var user = _dataContext.Users.SingleOrDefault(u => u.Id == id);
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
            var existingUser = _dataContext.Users.SingleOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Address = user.Address;
                Save();
            }
        }

        public void DeleteUser(int id)
        {
            var user = _dataContext.Users.SingleOrDefault(u => u.Id == id);
            if (user != null)
            {
                _dataContext.Users.DeleteOnSubmit(user);
                Save();
            }
        }

        public void Save()
        {
            _dataContext.SubmitChanges();
        }

        public void Dispose()
        {
            if (_dataContext != null)
            {
                _dataContext.Dispose();
            }
        }
    }
}