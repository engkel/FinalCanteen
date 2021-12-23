using System;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using System.Data.Sql;
using System.Drawing;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using MessageBox = System.Windows.Forms.MessageBox;

namespace dataToExcel
{
    class ExcelUILanguageHelper : IDisposable
        {
            private CultureInfo m_CurrentCulture;

            public ExcelUILanguageHelper()
            {
                // save current culture and set culture to da-DK 
                m_CurrentCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("da-DK");
            }

            // return to normal culture
            public void Dispose() => Thread.CurrentThread.CurrentCulture = m_CurrentCulture;
        }
        class Program
    {
        private const string sqlselect = "SELECT * FROM [CanteenDB].[dbo].[Employee]";
        //"SELECT [EmpId], [EmpName], FROM [CanteenDB].[dbo].[Employee]";
        //" SELECT [CategoryNumber],[CategoryName] FROM [CanteenDB].[dbo].[FoodCategory]";
        //  "SELECT [ItemId], [ItemName],[ItemPrice], [CategoryNumber] FROM [CanteenDB].[dbo].[FoodItems]";
        //"SELECT [ShoppingCartId],[Amount],[ReceiptNumber],[ItemId] FROM[CanteenDB].[dbo].[ShoppingCart]";
        //SELECT [ReceiptNumber],[ReceiptCreated],[EmpId] FROM [CanteenDB].[dbo].[Receipt]";

        private const string connectionString = "" + "Data Source=DESKTOP-MONOLIT;Initial Catalog=CanteenDB;Integrated Security=True";
        

        static void Main(string[] args)
        {
            using (new ExcelUILanguageHelper())
            {
                string fileName = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + "ExcelReport.xlsx";

                Excel.Application xlsApp;
                Excel.Workbook xlsWorkbook;
                Excel.Worksheet xlsWorksheet;
                object misValue = System.Reflection.Missing.Value;

                // Remove the old excel report file
                try
                {
                    FileInfo oldFile = new FileInfo(fileName);
                    if (oldFile.Exists)
                    {
                        File.SetAttributes(oldFile.FullName, FileAttributes.Normal);
                        oldFile.Delete();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error removing old dataToExcel: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                try
                {
                    xlsApp = new Excel.Application();
                    xlsWorkbook = xlsApp.Workbooks.Add(misValue);
                    xlsWorksheet = (Excel.Worksheet)xlsWorkbook.Sheets[1];

                    // Create the header for Excel file
                    xlsWorksheet.Cells[1, 1] = "Example of datatoExcel . Get data from CanteenDB database, table Employee";
                    Excel.Range range = xlsWorksheet.get_Range("A1", "E1");
                    range.Merge(1);
                    range.Borders.Color = Color.Black.ToArgb();
                    range.Interior.Color = Color.Yellow.ToArgb();

                    int i = 3;

                    SqlConnection conn = new SqlConnection(connectionString);
                    conn.Open();
                     SqlCommand cmd = new SqlCommand(sqlselect, conn);
                     SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        for (int j = 0; j < dr.FieldCount; ++j)
                        {
                            xlsWorksheet.Cells[i, j + 1] = dr.GetName(j);
                        }
                        ++i;
                    }

                    while (dr.Read())
                    {
                        for (int j = 1; j <= dr.FieldCount; ++j)
                            xlsWorksheet.Cells[i, j] = dr.GetValue(j - 1);
                        ++i;
                    }

                    range = xlsWorksheet.get_Range("A2", "I" + (i + 2).ToString());
                    range.Columns.AutoFit();

                    xlsWorkbook.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookDefault, misValue, misValue, misValue, misValue,
                        Excel.XlSaveAsAccessMode.xlExclusive, Excel.XlSaveConflictResolution.xlLocalSessionChanges, misValue, misValue, misValue, misValue);
                    xlsWorkbook.Close(true, misValue, misValue);
                    xlsApp.Quit();

                    ReleaseObject(xlsWorksheet);
                    ReleaseObject(xlsWorkbook);
                    ReleaseObject(xlsApp);

                    if (MessageBox.Show("Excel report has been created on your desktop\nWould you like to open it?", "Created Excel report",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        Process.Start(fileName);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error creating Excel report: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        static private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                MessageBox.Show("Exception occured while releasing object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}

