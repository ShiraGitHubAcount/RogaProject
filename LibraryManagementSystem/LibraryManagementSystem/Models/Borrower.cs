using System;

namespace LibraryManagementSystem.Models
{
    public class Borrower
    {
        public int BorrowerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string IDNumber { get; set; }
        public int Age { get; set; }
        public bool IsActive { get; set; }

    }
}