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
        public int GlueID { get; set; }
        public int IngredientID { get; set; }
        public int Percentage { get; set; }
        public string CreatedDate { get; set; }
        //public int Amount { get; set; }
       
    }
}
