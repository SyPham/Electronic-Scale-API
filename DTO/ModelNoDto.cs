using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.DTO
{
    public class ModelNoDto
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int ModelNameID { get; set; }
        public string ModelName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
