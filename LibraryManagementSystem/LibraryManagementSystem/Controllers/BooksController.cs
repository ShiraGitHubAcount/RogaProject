using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(BookSearchForm searchForm)
        {
            var booksQuery = db.Books.AsQueryable();
            var categories = db.Categories.ToList();
            var authors = db.Authors.ToList();
            var borrowers = db.Borrowers.ToList();
            if (searchForm.CategoryId > 0)
            {
                booksQuery = booksQuery.Where(b => b.CategoryId == searchForm.CategoryId);
            }

            if (searchForm.IsAvailable==true)
            {
                booksQuery = booksQuery.Where(b => b.AvailableCopies > 0);
            }
            else  if (searchForm.IsAvailable == null)
                {
                    booksQuery = booksQuery.Where(b => b.AvailableCopies == 0);
                }

            if (searchForm.AuthorId > 0)
            {
                booksQuery = booksQuery.Where(b => b.AuthorID == searchForm.AuthorId);
            }

            if (!string.IsNullOrEmpty(searchForm.BookName))
            {
                booksQuery = booksQuery.Where(b => b.Title.Contains(searchForm.BookName));
            }

            var books = booksQuery.ToList();

            var model = new IBooksCustomData
            {
                Authors = authors,
                Books = books,
                Categories = categories,
                Borrowers = borrowers,
            };

            return View(model);
        }
        [HttpGet]
        public ActionResult NewBook(bool fromHomePage = true)
        {
            var books = db.Books.ToList();
            var categories = db.Categories.ToList();
            var authors = db.Authors.ToList();
            var model = new IBookCustomData
            {
                Authors = authors,
                Books = books,
                Categories = categories,
                IsEdit = false,
                Book = new Book()
            };
            if (fromHomePage)
                return View("NewBook", model);
            else
            {
                return PartialView("NewBook", model);
            }
        }

        [HttpPost]
        public ActionResult Create(Book book)
        {
            try
            {
                book.AvailableCopies = book.NumberOfCopies;
                db.Books.Add(book);
                db.SaveChanges();
                for (int i = 0; i < book.NumberOfCopies; i++)
                {
                    var copy = new Copy
                    {
                        BookID = book.BookID,
                        IsAvailable = true,
                    };
                    db.Copies.Add(copy);
                }

                db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            try
            {
                var book = db.Books.Find(id);
                if (book == null)
                {
                    return Json(new { success = false, message = "Book not found" });
                }

                var copies = db.Copies.Where(c => c.BookID == id).ToList();
                var copyIds = db.Copies.Where(c => c.BookID == id).Select(c => c.CopyID).ToList();
                var borrowedBooks = db.BorrowedBooks.Where(bb => copyIds.Contains(bb.CopyID)).ToList();

                db.BorrowedBooks.RemoveRange(borrowedBooks);
                db.Copies.RemoveRange(db.Copies.Where(c => copyIds.Contains(c.CopyID)));
                db.Books.Remove(book);

                db.SaveChanges();


                return Json(new { success = true, message = "Book and its copies deleted successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(book).State = EntityState.Modified;
                    db.SaveChanges();
                    return Json(new { success = true });
                }
  
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }

            return Json(new { success = false, message = "Model state is not valid." });
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var categories = db.Categories.ToList();
            var authors = db.Authors.ToList();
              var model = new IBookCustomData
                {
    Authors = authors,
    Categories = categories,
    Book = book,
                  IsEdit=true,

              };

            return PartialView("EditBook", model); 
        }
    }
}