using AGCanteen.Models;
using AGCanteen.Services.Business;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//Author: Andrej
namespace AGCanteen.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Index");
        }


        public ActionResult Canteen(UserModel user)
        {
            return View();
        }



        public ActionResult logInCheck(UserModel userModel)
        {
            SecurityService securityService = new SecurityService();
            Boolean success = securityService.Authenticate(userModel);

            if (success)
            {
                Session["userID"] = userModel.Id;
                Session["orderID"] = securityService.GenerateSessionID();
                //all of the functionality added to a list and call canteen view

                //Creating Lists of Model objects
                List<UserModel> user = securityService.Getuser(userModel);
                List<BreakfastModel> BreakfastItems = securityService.CanteenItems();


                //Session["sessionID"] = securityService.getCartInstance(securityService.GenerateSessionID());

                //Creating a customer Data model in order to pass on all of the data to view
                CanteenModel customerData = new CanteenModel();

                customerData.Items = BreakfastItems;
                customerData.User = user;
                Session["userdata"] = customerData;

                return View("Canteen", customerData);
            }

            else
            {
                return View("LogInFailure");
            }
        }


        public ActionResult Add(BreakfastModel BreakfastItem)
        {

            if (Session["cart"] == null)
            {
                List<BreakfastModel> list = new List<BreakfastModel>();

                list.Add(BreakfastItem);
                Session["cart"] = list;
                ViewBag.cart = list.Count();

                Session["count"] = 1;


            }
            else
            {
                List<BreakfastModel> li = (List<BreakfastModel>)Session["cart"];
               
                int index = li.IndexOf(li.Where(p => p.ItemID == BreakfastItem.ItemID).FirstOrDefault());
                if (index > -1)
                {
                    li[index].quantity = li[index].quantity + 1;
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }
                else {
                    li.Add(BreakfastItem);
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                }

            }

            return View("Canteen", (CanteenModel)Session["userdata"]);

        }

        public ActionResult Goback(){

            return View("Canteen", (CanteenModel) Session["userdata"]);

    }


        public ActionResult Myorder()
        {

            return View((List<BreakfastModel>)Session["cart"]);

        }

        public ActionResult Remove(BreakfastModel BreakfastItem)
        {
            List<BreakfastModel> li = (List<BreakfastModel>)Session["cart"];
            int itemsToRemove;
            var match = li.FirstOrDefault(x => x.ItemID == BreakfastItem.ItemID);
           
            if (match != null) {
                int index = li.IndexOf(li.Where(p => p.ItemID == BreakfastItem.ItemID).FirstOrDefault());
                itemsToRemove = li[index].quantity;
                Session["cart"] = li;
                Session["count"] = Convert.ToInt32(Session["count"]) - itemsToRemove;
                li.Remove(match); 
            }
                
           // li.RemoveAll(x => x.ItemID == BreakfastItem.ItemID);
           
            // return RedirectToAction("Myorder", "Home");
            return View("Myorder", (List<BreakfastModel>)Session["cart"]);
        }



        public ActionResult CompleteOrder()
        {
            SecurityService securityService = new SecurityService();

            List<BreakfastModel> li = (List<BreakfastModel>)Session["cart"];
            int userID = Convert.ToInt32(Session["userID"]);
            string orderID = (string)Session["orderID"];

            Boolean success = securityService.CompleteOrder(li,userID,orderID);

            if (success)
            {
                Session.Clear();
                return View("CompleteOrder");

            }

            else
            {
                return View("Myorder", (List<BreakfastModel>)Session["cart"]);
            }

          
        }



    }
}