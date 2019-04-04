using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApplication.Models
{
    public class NewsModel
    {
        public string NewsId { get; set; }

        public string NewsTitle { get; set; }

        public string NewsDescription { get; set; }

        public DateTime NewsDate { get; set; }

        public string NewsLink { get; set; }

        public string NewsSource { get; set; }

        public string NewsAuthor { get; set; }
    }
}
