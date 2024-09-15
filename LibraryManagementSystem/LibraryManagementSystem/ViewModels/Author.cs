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
          public List<Book> Books { get; set; }
          public  List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public Book Book { get; set; }
        public bool IsEdit { get; set; }
        public List<AuthorBookViewModel> AuthorBookData { get; set; }
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
