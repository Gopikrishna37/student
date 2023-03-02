using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using OfficeOpenXml;
using ClosedXML.Excel;
using System.Web.Mvc;
using System.IO;

namespace ShopView.Service
{
    public class ExcelCompare
    {
        public dynamic Compare(DataTable dt1)
        {
            try
            {
                bool flag = true;
                string path2 = "C:/Users/BE HAPPY/source/repos/task/ShopView/App_Data/Excel data/Product_Formet.xlsx";

                // Connection strings for both the sheets 
                string connString2 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path2 +
                    ";Extended Properties=Excel 12.0;";

                // Create two separate connection objects 
                OleDbConnection conn2 = new OleDbConnection(connString2);

                // Open both the connections 
                conn2.Open();

                // Create two separate DataTables to store data from two sheets 
                DataTable dt2 = new DataTable();

                // Get Sheets name in both Excel Files 
                DataTable dtSheet2 = conn2.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Get the name of first sheet from workbook 
                string sheet2 = dtSheet2.Rows[0]["TABLE_NAME"].ToString();

                // Execute OleDb command and fetch data from excel and store in DataTable 

                OleDbCommand cmd2 = new OleDbCommand("select * from [" + sheet2 + "]", conn2);
                OleDbDataAdapter da2 = new OleDbDataAdapter(cmd2);
                da2.Fill(dt2);
                int rowCount = dt1.Rows.Count;

                // Add a new row 

                // Check number of columns in both the sheets 
                if (dt1.Columns.Count == dt2.Columns.Count)
                {
                    
                    int colcount=dt1.Columns.Count;

                    // If both sheets have same number of columns 
                    // Compare each column one by one 
                    for (int i = 0; i < colcount; i++)
                    {
                        // Check DataType of target column 

                        if (dt1.Columns[i].ColumnName != dt2.Columns[i].ColumnName)
                        {
                            dt1.Columns.Add("Error", typeof(String));
                            dt1.Rows.Add();
                            dt1.Rows[rowCount]["Error"] = "Name of column " + dt1.Columns[i].ColumnName + " is not matched";
                            flag = false;
                            colcount--;
                            rowCount--;
                            
                        }
                    }
                        for (int row = 0; row < rowCount; row++)
                        {
                            for (int col=0; col< colcount-3; col++)
                            if (dt1.Rows[row][col] == null)
                            {
                                dt1.Rows[row][col] = "cell should not be null";
                                flag = false;
                            }
                        }

                    
                    conn2.Close();
               
                }
                else
                {

                    dt1.Columns.Add("Error", typeof(String));
                    dt1.Rows.Add();
                    dt1.Rows[rowCount]["Error"] = "column count is not matched";
                    flag = false;
                }

                // Close both the connections 
                if (flag == false)
                {
                    return dt1;
                }

                return true;

            }
            catch (Exception e)
            {
                return e;
            }

        }
    }
}