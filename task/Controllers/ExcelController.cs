using ClosedXML.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using task.Models;

namespace task.Controllers
{
    public class ExcelController : Controller
    {
        DBstudent db = new DBstudent();
        // GET: InsuranceCertificate  
        public ActionResult Excel()
        {
            var students = db.student.ToList();
            return View(students);
        }

        [HttpPost]
        public ActionResult ExportToExcel()
        {
            DataTable dt = new DataTable("Grid");
            dt.Columns.AddRange(new DataColumn[6] { 
                                                     new DataColumn("StudentId"),
                                                     new DataColumn("StudentName"),
                                                     new DataColumn("Address"),
                                                     new DataColumn("Username"),
                                                     new DataColumn("Password"),
                                                     new DataColumn("Email")});

            var customers = from stu in db.student select stu;
          /*  var customers = from customer in entities.Customers.Take(10)
                            select customer;*/

            foreach (var customer in customers)
            {
                dt.Rows.Add(customer.StudentId, customer.StudentName, customer.Username, customer.Password,customer.Address,customer.Email);
            }

            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Grid.xlsx");
                }
            }
        }
     
    

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                if (postedFile != null && postedFile.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
                {
                    ModelState.AddModelError("postedFile", "Your file is to large. Maximum size allowed is 50MB !");
                }

                else
                {
                    string filePath = string.Empty;
                    string path = Server.MapPath("~/Uploads/");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    filePath = path + Path.GetFileName(postedFile.FileName);
                    string extension = Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(filePath);

                    string conString = string.Empty;
                    switch (extension)
                    {
                        case ".xls": //For Excel 97-03.  
                            conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                            break;
                        case ".xlsx": //For Excel 07 and above.  
                            conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                            break;
                    }

                    try
                    {
                        DataTable dt = new DataTable();
                        conString = string.Format(conString, filePath);

                        using (OleDbConnection connExcel = new OleDbConnection(conString))
                        {
                            using (OleDbCommand cmdExcel = new OleDbCommand())
                            {
                                using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                                {
                                    cmdExcel.Connection = connExcel;

                                    //Get the name of First Sheet.  
                                    connExcel.Open();
                                    DataTable dtExcelSchema;
                                    dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                                    string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                                    connExcel.Close();

                                    //Read Data from First Sheet.  
                                    connExcel.Open();
                                    cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                                    odaExcel.SelectCommand = cmdExcel;
                                    odaExcel.Fill(dt);
                                    connExcel.Close();
                                }
                            }
                        }

                        using (DBstudent db = new DBstudent())
                        {
                            // Create a new Student object.
                            var student = new task.Models.Student();

                            // Loop through each row of the DataTable.
                            foreach (DataRow row in dt.Rows)
                            {
                                // Set the properties of the Student object.
                                student.StudentId = int.Parse(row["StudentId"].ToString());
                                student.StudentName = row["Name"].ToString();
                                student.Address = row["Address"].ToString();
                                student.Email = row["Email"].ToString();
                                student.Password = row["Password"].ToString();
                                student.Username = row["Username"].ToString();
                                db.student.Add(student);
                                db.SaveChanges();
                            }

                            return Json("File uploaded successfully");
                        }
                    }


                    catch (Exception e)
                    {
                        return Json("error" + e.Message);
                    }
                }
            }
            return Json("no files were selected !");
        }
    }
}