using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UploadCenterV2.Models;

namespace UploadCenterV2.Controllers
{
    public class CommentsController : Controller
    {
        // GET: Comments
        
        public ActionResult View(int id)
        {
            Comment commentModel = new Comment();
            using (CommentsEntities db = new CommentsEntities())
            {
                //int imgID = (int)Session["image_ID"];
                var data = db.Comments.Where(x => x.Image_ID == id);
                return View(data.ToList());
            }
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CommentsEntities db = new CommentsEntities())
            {
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    {
                        return HttpNotFound();
                    }
                }
                return View(comment);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Comment_ID,Text,Username,Image_ID")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                using (CommentsEntities db = new CommentsEntities())
                {
                    db.Entry(comment).State = EntityState.Modified;
                    db.SaveChanges();
                }
                //return View(comment);
                return RedirectToAction("View", new { id = comment.Image_ID});
            }
            return View(comment);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            using (CommentsEntities db = new CommentsEntities())
            {
                Comment comment = db.Comments.Find(id);
                if (comment == null)
                {
                    {
                        return HttpNotFound();
                    }
                }
                return View(comment);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (CommentsEntities db = new CommentsEntities())
            {
                Comment comment = db.Comments.Find(id);
                db.Comments.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("View", new { id = comment.Image_ID });
            }
        }

        public ActionResult Add()
        {
            Comment commentModel = new Comment();
            return View(commentModel);
        }

        [HttpPost]
        public ActionResult Add(Comment commentModel)
        {
            using (CommentsEntities comment = new CommentsEntities())
            {
                commentModel.Image_ID = (int)Session["image_ID"];
                commentModel.Username = Session["fullName"].ToString();
                comment.Comments.Add(commentModel);
                comment.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Added successful";
            return RedirectToAction("View", "Comments", new { id = (int)Session["image_ID"] });
            //return View();
        }
    }
}