using System;

namespace LibraryManagementSystem.Models
{
    public class BorrowedBook
    {
        public int BorrowerId { get; set; }
        public bool IsReturned { get;  set;  }
        public int BorrowedBookID { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int CopyID { get; set; }
        public virtual Copy Copy { get; set; }
        public virtual Borrower Borrower { get; set; }



    }
}