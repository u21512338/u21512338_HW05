using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace u21512338_HW05.Models
{
    public class Book
    {
        public int bookID { get; set; }
        public string aName { get; set; }
        public int pageCount { get; set; }
        public int point { get; set; }
        public string authorN { get; set; }
        public string typeN { get; set; }
        public DateTime bDate { get; set; }


    }
}