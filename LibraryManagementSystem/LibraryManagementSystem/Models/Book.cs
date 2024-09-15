using System;

namespace LibraryManagementSystem.Models
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }
        public int AuthorID { get; set; }
        public int NumberOfCopies { get; set; }
        public int CategoryId { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime PublishedDate { get; set; }
        public virtual Category Category { get; set; }
        public virtual Author Author { get; set; }
    }
}