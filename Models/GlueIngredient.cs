using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EC_API.Models
{
    public class GlueIngredient
    {
        public GlueIngredient()
        {
            this.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss tt");
        }

        public int ID { get; set; }
        public int ModelName { get; set; } // Ten Giay
        public int ModelNo { get; set; } // Ma giay
        public int GlueID { get; set; } // Keo
        public int LineID { get; set; } // Line
        public int IngredientID { get; set; } //Thanh phan
        public int Percentage { get; set; } // Ty le phan tram
        public string CreatedDate { get; set; } // Ngay pha
        public int Input { get; set; } // So luong thanh phan da pha
    }
}
