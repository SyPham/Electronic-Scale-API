using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.Models
{
    public class ModelName
    {
        public ModelName()
        {
            this.CreatedDate = DateTime.Now;
        }
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual ICollection<ModelNo> ModelNos { get; set; }
    }
}
