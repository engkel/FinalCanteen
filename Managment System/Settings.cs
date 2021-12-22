using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Managment_System
{
    /*
     * Author: David
    */
    static class Settings
    {
        /*Here, enter your SERVER NAME (find it in sql management studio 
         * when you login.) This value is then used throughout the management app wherever it's needed. =$@"{DB}" */
        public static string ServerName
        {
            get
            {
                return "DESKTOP-MONOLIT"; //SERVER NAME
            }
        }
    }
}

