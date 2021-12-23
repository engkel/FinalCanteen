using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGCanteen.Models;
using AGCanteen.Services.Business;
//Author: Andrej
namespace AGCanteen.Models
{
    public class CanteenModel
    {
        // A model containing Lists of other Model
        public List<BreakfastModel> Items { get; set; }

        public List<UserModel> User { get; set; }
       
    }
}