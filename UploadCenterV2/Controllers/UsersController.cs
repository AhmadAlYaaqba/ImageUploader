using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadCenterV2.Models;

namespace UploadCenterV2.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            User userModel = new User();
            return View(userModel);
        }

        public ActionResult Dashboard()
        {
            using (ImagesEntities db = new ImagesEntities())
            {
                if (Session["userID"] == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                int userID = (int)Session["userID"];
                var data = db.Images.Where(x => x.User_ID == userID);
                return View(data.ToList());
            }
        }

        [HttpPost]
        public ActionResult Create(User userModel)
        {
            using (UsersEntities dbModel = new UsersEntities())
            {
                if (dbModel.Users.Any(x => x.username == userModel.username))
                {
                    ViewBag.DuplicateMessage = "Username Already exist";
                    return View("Create", userModel);
                }
                dbModel.Users.Add(userModel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registration successful";
            return View("Create", new User());
        }

        public ActionResult Authorize(User userModel)
        {
            using (UsersEntities db = new UsersEntities())
            {
                var userDetails = db.Users.Where(x => x.username == userModel.username && x.password == userModel.password).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    Session["userID"] = userDetails.User_ID;
                    Session["fullName"] = userDetails.FullName;
                    return RedirectToAction("Index", "Home");
                }
            }
        }

        public ActionResult Logout()
        {
            //int userID = (int)Session["userID"];
            Session.Abandon();
            return RedirectToAction("Index", "Users");
        }
    }
}