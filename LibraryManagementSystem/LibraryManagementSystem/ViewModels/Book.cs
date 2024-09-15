using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
    public class IBooksCustomData
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public List<Borrower> Borrowers { get; set; }

    }
    public class NewBookCustomData
    {
        public List<Book> Books { get; set; }
        public List<Author> Authors { get; set; }
        public List<Category> Categories { get; set; }
        public string Title { get; set; }
        public DateTime PublishedDate { get; set; }
        public int NumberOfCopies { get; set; }

    }
    public class BookSearchForm
    {
        public int CategoryId { get; set; }
        public bool IsAvailable { get; set; }
        public int AuthorId { get; set; }
        public string BookName { get; set; }
    }
}