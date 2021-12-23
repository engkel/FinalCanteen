using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGCanteen.Models;
using AGCanteen.Services.Business;

namespace AGCanteen.Models
{
    public class BreakfastModel
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Category { get; set; }
  
        public int ItemID { get; set; }

        public int quantity { get; set; }
        public BreakfastModel()
        {
            Name = "None";
            Price = -1;
            Category = -1;
            ItemID = -1;

        }



        public BreakfastModel(string name, int price, int category, int itemID)
        {
            Name = name;
            Price = price;
            Category = category;
            ItemID = itemID;
        }   
    }
}