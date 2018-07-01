using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadCenterV2.Models;

namespace UploadCenterV2.Controllers
{
    public class ImagesController : Controller
    {
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult View(int id)
        {
            Image imageModel = new Image();
            using (ImagesEntities db = new ImagesEntities())
            {
                imageModel = db.Images.Where(x => x.Image_ID == id).FirstOrDefault();
            }
            return View(imageModel);
        }

        [HttpPost]
        public ActionResult Add(Image imageModel)
        {
            string fileName = Path.GetFileNameWithoutExtension(imageModel.ImageFile.FileName);
            string extension = Path.GetExtension(imageModel.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            imageModel.ImagePath = "~/Image/" + fileName;
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            imageModel.ImageFile.SaveAs(fileName);
            int userID = (int)Session["userID"];
            imageModel.User_ID = userID;
            using (ImagesEntities db = new ImagesEntities())
            {
                db.Images.Add(imageModel);
                db.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Uploaded successful";
            return View("Add", new Image());
        }

        public ActionResult AddC(Comment commentModel)
        {
            using (CommentsEntities comment = new CommentsEntities())
            {
                commentModel.Username = Session["fullName"].ToString();
                comment.Comments.Add(commentModel);
                comment.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Added successful";
            return RedirectToAction("View", "Images", new { id = commentModel.Image_ID});
            //return View();
        }
    }
}