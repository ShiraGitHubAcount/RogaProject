using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.ViewModels
{
    public class IBookCustomData
    {
          public IList<Book> Books { get; set; }
          public  IList<Author> Authors { get; set; }
        public IList<Category> Categories { get; set; }
        public Book Book { get; set; }
        public bool IsEdit { get; set; }
        public IList<AuthorBookViewModel> AuthorBookData { get; set; }
}
    public class AuthorBookViewModel
    {
        public int AuthorID { get; set; }
        public string AuthorName { get; set; }
        public string Phone { get; set; }  
        public string Email { get; set; }  
        public int BookCount { get; set; }
    }

}
