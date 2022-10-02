using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21512338_HW05.Models
{
    public class Borrow
    {
        public int borrowID { get; set; }
        public string studentName { get; set; }
        public DateTime takenDate { get; set; }
        public DateTime broughtDate { get; set; }
        public string bookName { get; set; }
    }
}