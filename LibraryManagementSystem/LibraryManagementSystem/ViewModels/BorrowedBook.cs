using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
    public class BorrowedBooksViewModel
    {
        public IList<Book> Books { get; set; }
        public IList<Borrower> Borrowers { get; set; }
        public IList<Models.BorrowedBook> BorrowedBooks { get; set; }
    }
    public class BorrowedBookSearchForm
    {
        public DateTime BorrowedDate { get; set; }
        public bool IsReturned { get; set; }
        public int BorrowerId { get; set; }
        public string BookName { get; set; }

    }
}