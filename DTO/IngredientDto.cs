using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.DTO
{
    public class IngredientDto
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public int Percentage { get; set; }
        public bool Status { get; set; }

    }
}
