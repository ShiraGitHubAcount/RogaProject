using System.Web.Mvc;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.ViewModels;
using System.Linq;

namespace LibraryManagementSystem.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var newBooks = db.Books.OrderByDescending(b => b.PublishedDate).Take(5).ToList();

            return View(new HomePageViewModel
            {
                NewBooks = newBooks
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}