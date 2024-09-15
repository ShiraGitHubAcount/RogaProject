using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;

using System;
using System.Linq;
using System.Web.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BorrowersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index(BorrowerSearchForm searchForm)
        {
            var borrowersQuery = db.Borrowers.AsQueryable();

            if (!string.IsNullOrEmpty(searchForm.FirstName))
            {
                borrowersQuery = borrowersQuery.Where(b => b.FirstName.Contains(searchForm.FirstName));
            }

            if (!string.IsNullOrEmpty(searchForm.LastName))
            {
                borrowersQuery = borrowersQuery.Where(b => b.LastName.Contains(searchForm.LastName));
            }

            if (!string.IsNullOrEmpty(searchForm.Phone))
            {
                borrowersQuery = borrowersQuery.Where(b => b.Phone.Contains(searchForm.Phone));
            }

            if (!string.IsNullOrEmpty(searchForm.Email))
            {
                borrowersQuery = borrowersQuery.Where(b => b.Email.Contains(searchForm.Email));
            }

            if (!string.IsNullOrEmpty(searchForm.IDNumber))
            {
                borrowersQuery = borrowersQuery.Where(b => b.IDNumber.Contains(searchForm.IDNumber));
            }

            if (searchForm.IsActive)
            {
                borrowersQuery = borrowersQuery.Where(b => b.IsActive);
            }

            var borrowers = borrowersQuery.ToList();

            return View(borrowers);
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Borrower borrower)
        {
      
                db.Borrowers.Add(borrower);
                db.SaveChanges();
                return RedirectToAction("Index");
            
        }

        public ActionResult Edit(int id)
        {
            var borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return HttpNotFound();
            }
            return View(borrower);
        }

        [HttpPost]
        public ActionResult Edit(Borrower borrower)
        {
            if (ModelState.IsValid)
            {
                db.Entry(borrower).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(borrower);
        }
        public ActionResult Delete(int id)
        {
            var borrower = db.Borrowers.Find(id);
            if (borrower == null)
            {
                return Json(new { success = false, message = "Borrower not found." });
            }

            var activeLoans = db.BorrowedBooks
                                .Where(bb => bb.BorrowerId == id && !bb.IsReturned)
                                .ToList();

            if (activeLoans.Any())
            {
                return Json(new { success = false, message = "Cannot delete borrower with active loans." });
            }

            var loansToDelete = db.BorrowedBooks
                                 .Where(bb => bb.BorrowerId == id)
                                 .ToList();

            foreach (var loan in loansToDelete)
            {
                db.BorrowedBooks.Remove(loan);
            }

            db.Borrowers.Remove(borrower);
            db.SaveChanges();

            return Json(new { success = true, message = "Borrower deleted successfully." });
        }


    }
}
