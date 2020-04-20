using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.Models
{
    public class ModelNo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("ModelName")]
        public int ModelNameID { get; set; }
        public virtual ModelName ModelName { get; set; }
        public ModelNo()
        {
            this.CreatedDate = DateTime.Now;
        }

    }
}
