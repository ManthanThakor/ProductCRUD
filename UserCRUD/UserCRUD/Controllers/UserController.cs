using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UserCRUD.Models;

namespace UserCRUD.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController()
        {
            _userRepository = new UserRepository();
        }

        public ActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(UserModel user)
        {
            _userRepository.AddUser(user);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
                return HttpNotFound();

            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(UserModel user)
        {
            _userRepository.UpdateUser(user);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            _userRepository.DeleteUser(id);
            return RedirectToAction("Index");
        }


    }
}