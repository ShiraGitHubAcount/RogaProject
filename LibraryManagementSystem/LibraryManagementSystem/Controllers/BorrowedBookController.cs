using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowedBookController: Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(BorrowedBookSearchForm searchForm)
        {
            var borrowedBooksQuery = db.BorrowedBooks.AsQueryable();
            var booksQuery = db.Books.AsQueryable();
            var borrowersQuery = db.Borrowers.AsQueryable();
            if (searchForm.BorrowedDate != null && searchForm.BorrowedDate != DateTime.MinValue)
            {      
                borrowedBooksQuery = borrowedBooksQuery.Where(bb => bb.BorrowedDate == searchForm.BorrowedDate);
            }
            if (searchForm.IsReturned)
            {
                borrowedBooksQuery = borrowedBooksQuery.Where(bb => bb.IsReturned == searchForm.IsReturned);
            }

            if (searchForm.BorrowerId>0)
            {
                borrowedBooksQuery = borrowedBooksQuery.Where(bb => bb.BorrowerId == searchForm.BorrowerId);
            }

            if (!string.IsNullOrEmpty(searchForm.BookName))
            {
                borrowedBooksQuery = borrowedBooksQuery.Where(bb => db.Books.Any(b => b.BookID == bb.Copy.BookID && b.Title.Contains(searchForm.BookName)));
            }

            var borrowedBooks = borrowedBooksQuery.ToList();
            var books = booksQuery.ToList();
            var borrowers = borrowersQuery.ToList();

            var model = new BorrowedBooksViewModel
            {
                BorrowedBooks =borrowedBooks,
                Books = books,
                Borrowers = borrowers
            };

            return View(model);
        }
        public ActionResult NewBorrow(int bookId, int userId)
        {
            var availableCopies = db.Copies
                .Where(c => c.BookID == bookId && c.IsAvailable)
                .OrderBy(c => c.CopyID)
                .ToList();

            var copyToBorrow = availableCopies.First();
            copyToBorrow.IsAvailable = false;
            var book = db.Books.Find(bookId);
            if (book != null)
            {
                book.AvailableCopies = db.Copies.Count(c => c.BookID == bookId && c.IsAvailable);
            }
            var borrowedBook = new Models.BorrowedBook
            {
                BorrowerId = userId,
                IsReturned = false,
                BorrowedDate = DateTime.Now,
                CopyID = copyToBorrow.CopyID
            };

            db.BorrowedBooks.Add(borrowedBook);
            db.SaveChanges();
            return Json(new { success = true, message = "Book borrowed successfully" });
        }

        public ActionResult ReturnBook(int borrowedBookId)
        {
            var borrowedBook = db.BorrowedBooks.Find(borrowedBookId);
            if (borrowedBook == null)
            {
                return Json(new { success = false, message = "Borrowed book record not found" });
            }
            var copy = db.Copies.Find(borrowedBook.CopyID);

            if (copy == null)
            {
                return Json(new { success = false, message = "Copy not found" });
            }
            copy.IsAvailable = true;
            borrowedBook.IsReturned = true;
            borrowedBook.ReturnDate = DateTime.Now;
            var book = db.Books.Find(copy.BookID);
            if (book != null)
            {
                book.AvailableCopies = db.Copies.Count(c => c.BookID == copy.BookID && c.IsAvailable);
            }
            db.SaveChanges();
            return Json(new { success = true, message = "Book returned successfully" });
        }


    }
}