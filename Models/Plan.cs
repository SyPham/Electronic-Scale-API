using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.Models
{
    public class Plan
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LineID { get; set; }
        public int GlueID { get; set; }
        public string Quantity { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
