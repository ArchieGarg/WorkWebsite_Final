using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_Form.Models
{
    public class NewStoreItem
    {

        public String name { get; set; }
        public int quantity { get; set; }
        public int showQuantity { get; set; }
        public double cost { get; set; }
        public String description { get; set; }
        public String image { get; set; }
        public String detailedDescription { get; set;}
        public String Fact1 { get; set; }
        public String Fact2 { get; set; }
        public String Fact3 { get; set; }

    }
}