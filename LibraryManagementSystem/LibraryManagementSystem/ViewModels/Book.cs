using LibraryManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.ViewModels
{
    public class IBooksCustomData
    {
        public IList<Book> Books { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Category> Categories { get; set; }
        public IList<Borrower> Borrowers { get; set; }

    }
    public class NewBookCustomData
    {
        public IList<Book> Books { get; set; }
        public IList<Author> Authors { get; set; }
        public IList<Category> Categories { get; set; }
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