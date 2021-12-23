using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Author: Andrej
namespace AGCanteen.Models
{
    public class CartItemModel
    {
        public string OrderId { get; set; }
        public int Amount { get; set; }
        public int ItemID { get; set; }

        public CartItemModel(string sessionID)
        {
            OrderId = sessionID;
        }







    }
}