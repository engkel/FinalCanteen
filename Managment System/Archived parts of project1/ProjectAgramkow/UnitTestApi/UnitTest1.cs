
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using ProjectAgramkow.Controllers;

namespace UnitTestApi
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //public void TestMethod1()
        
        public void Index()
        {
            //Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;
            //Assert
            Assert.IsNotNull(result);
        }
    }
}
