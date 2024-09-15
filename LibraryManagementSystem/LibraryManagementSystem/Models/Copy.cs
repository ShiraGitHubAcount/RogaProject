using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagementSystem.Models
{
    public class Copy
    {
        public int CopyID { get; set; }
        public int BookID { get; set; }  
        public bool IsAvailable { get; set; }
        public virtual Book Book { get; set; }  
    }

}