using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Linq;

namespace ExportToExcel
{
    /*
    public static class Extensions
    {
        public static bool Find<T>(this T[] array, T target)
        {
            return array.Contains(target);
        }
    }
    */

    class Program
    {
        static void Main(string[] args)
        {
            bool timeToExport = checkIfTimeForExport(); //should be put in the main of the admin program.  (if checkIfTimeForExport could return a bool - if(checkIfTimeForExport == true) {run export part}
            Console.WriteLine("Time to export?" + timeToExport);
            if(timeToExport == true)
            {
                //Export Excel raheela
            }
        
        }

        /*
        * author: David
        * checkIfTimeForExport checks to see if it's time to export and that the last week's data is exported. 
        */
        public static bool checkIfTimeForExport()
        {
            //Get current year 
            DateTime thisDay = DateTime.Now;
            int currentYear = thisDay.Year;

            // Get current week number : Gets the Calendar instance associated with a CultureInfo and Gets the DTFI properties required by GetWeekOfYear for the Danish week rules.
            CultureInfo myCI = new CultureInfo("da-DK");
            Calendar myCal = myCI.Calendar;
            CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
            DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
            int currentWeek = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW);

            //week 1 the data from week 52 should be exported
            if (currentWeek == 1)
            {
                //export w52.xlsx to currentYear-1
                return true;
            }
            else
            {
                Console.WriteLine("currentweek:" + currentWeek);
                int weekToSearchFor = currentWeek - 1;
                Console.WriteLine("currentweek:" + currentWeek);
                Console.WriteLine("weektosearch:" + weekToSearchFor);

                //creating array to store filenames, getting the files in the directory, and storing their file names in an array
                string[] fileNames = new string[52];

                try
                {
                    fileNames = Directory.GetFiles(@$"C:\WeeklyExportedExcelFiles\{currentYear}\")
                                         .Select(Path.GetFileName)
                                         .ToArray();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return false;
                }
                string stringToFind = $"w{weekToSearchFor}.xlsx"; // If w01, then don't.  AND when creating files x1, x2 etc. must be 01, 02 etc.
                Console.WriteLine("stringtofind:"+ stringToFind);

                foreach (string file in fileNames)
                {
                    Console.WriteLine(file);
                }

                //check if file exists
                bool hasFound = LookInArray(fileNames, stringToFind);

                if (hasFound == false) //no file by last week's name has been found in folder->export last week's data.
                {
                    return true; //true that it's time to export
                }
                else
                {
                    return false;
                }

                static bool LookInArray(Array fileNames, string stringToFind)
                {
                    bool isExist = false;

                    /*
                    //string foundString;
                    isExist = Array.Exists(fileNames, element => element == stringToFind);
                    
                    */
                    return isExist;
                }
            }
        }
    }
}