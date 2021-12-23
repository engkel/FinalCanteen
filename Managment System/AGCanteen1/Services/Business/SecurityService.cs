using AGCanteen.Models;
using AGCanteen.Services.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//Author: Andrej
namespace AGCanteen.Services.Business
{
    public class SecurityService
    {

        SecurityDAO daoService = new SecurityDAO();


        public bool Authenticate(UserModel userModel)
        {
            return daoService.FindUserId(userModel);


        }



        public List<BreakfastModel> CanteenItems()
        {
            return daoService.GetCanteenItems();
        }

        public List<UserModel> Getuser(UserModel user)
        {
            return daoService.GetUser(user);

        }

        public string GenerateSessionID()
        {
            char[] characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            Random r = new Random();
            System.Threading.Thread.Sleep(10);
            string randomstring = "";
            for (int i = 0; i < 20; i++)
            {
                randomstring += characters[r.Next(0, 60)].ToString();
            }

            return randomstring;
        }


        public List<CartItemModel> getCartInstance(string id)
        {
            List<CartItemModel> returnList = new List<CartItemModel>();
            CartItemModel Cm = new CartItemModel(id);
            returnList.Add(Cm);
            return returnList;

        }

        public bool CompleteOrder(List<BreakfastModel> cart, int userID, string orderID)
        {

            return daoService.StoreOrder(cart,userID,orderID);

        }




    }
}