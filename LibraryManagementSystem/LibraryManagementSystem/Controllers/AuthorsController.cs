using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class AuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
         var query = @"
        SELECT 
            a.AuthorID,
            a.FirstName + a.LastName AS AuthorName,
            a.Email AS Email,  
            a.Phone AS Phone,  
            COUNT(b.BookID) AS BookCount
        FROM 
            Authors a
        LEFT JOIN 
            Books b ON a.AuthorID = b.AuthorID
        GROUP BY 
            a.AuthorID, a.FirstName, a.LastName , a.Email, a.Phone";
            var authorBookData = db.Database.SqlQuery<AuthorBookViewModel>(query).ToList();

            var books = db.Books.ToList();
            var authors = db.Authors.ToList();
            var model = new IBookCustomData
            {
                AuthorBookData= authorBookData,
                Books = books,
                Authors = authors
            };
            return View(model);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New( Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(author);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                var author = db.Authors.Find(id);
                if (author == null)
                {
                    return Json(new { success = false, message = "Author not found" });
                }

                db.Authors.Remove(author);
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }


        [HttpPost]
        public JsonResult Update(Author author)
        {
            try
            {
                var existingAuthor = db.Authors.Find(author.AuthorID);
                if (existingAuthor == null)
                {
                    return Json(new { success = false, message = "Author not found" });
                }

                existingAuthor.FirstName = author.FirstName;
                existingAuthor.Email = author.Email;
                existingAuthor.Phone = author.Phone;

                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

    }


}