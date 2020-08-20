using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login_Form.Models
{
    public class StoreItem
    {

        public String Name { get; set; }
        public int Quantity { get; set; }
        public double Cost { get; set; }
        public String Description { get; set; }
        public String Image { get; set; }
    }
}