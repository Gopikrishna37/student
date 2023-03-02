using DBcontext.ServiceModels;
using Newtonsoft.Json;
using ShopView.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ShopView.Service;
using ClosedXML.Excel;

namespace ShopView.Controllers
{
    public class SellerController : Controller
    {

        Uri baseAddress = new Uri("https://localhost:44317/api/SellerApi");
        HttpClient client;
        public SellerController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        // GET: Seller
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult SellerLogin(SellerLogin sellerLogin)
        {
            return View(sellerLogin);
        }

        [HttpPost]
        public ActionResult SellerLogin1(SellerLogin sellerLogin)
        {
            Session["Name"] = sellerLogin.Email;
            string data = JsonConvert.SerializeObject(sellerLogin);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("SellerLogin", sellerLogin);
            }
            return HttpNotFound("SellerLogin");

        }

        [Session]
        public FileResult Download()
        {
            var FileVirtualPath = "~/App_Data/Excel data/Product_Formet.xlsx";
            return File(FileVirtualPath, "application/force-download", Path.GetFileName(FileVirtualPath));
        }

        [HttpPost]
        public ActionResult verify(SellerLogin sellerLogin)
        {
            string data = JsonConvert.SerializeObject(sellerLogin);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/login", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                var seller = Newtonsoft.Json.JsonConvert.DeserializeObject<Seller>(temp);
                Session["Sellerlogin"] = seller.SellerID;
                Session["Sellername"] = seller.Name;
                return RedirectToAction("Existing", "Seller", seller);
            }
            return HttpNotFound();
        }

        [HttpGet]
        public ActionResult SellerSingIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SellerSingIn(SellerSignIn sellerSignIn)
        {

            string data = JsonConvert.SerializeObject(sellerSignIn);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Signin", content).Result;

            if (response.IsSuccessStatusCode)
            {
                var temp = response.Content.ReadAsStringAsync().Result;
                return RedirectToAction("Index", "Home");
            }
            return HttpNotFound("SellerLogin");
        }

        [Session]
        [HttpGet]
        public ActionResult Existing()
        {
            HttpResponseMessage response = client.GetAsync("SellerApi").Result;

            if (response.IsSuccessStatusCode)
            {
                var data = response.Content.ReadAsStringAsync().Result;
                var usermodel = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DBcontext.DBModels.Product>>(data);
                return View(usermodel);
            }
            return View();
        }

      
        
        [HttpGet]
        [Session]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImportFromExcel(HttpPostedFileBase file)
        {

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > (1024 * 1024 * 50))  // 50MB limit  
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

                    filePath = path + Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    file.SaveAs(filePath);

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
                                    connExcel.Dispose();
                                    odaExcel.Dispose();
                                    cmdExcel.Dispose();
                                    ExcelCompare e = new ExcelCompare();

                                    var output = e.Compare(dt);
                                   
                                    if (output is bool)
                                    {
                                        List<ProductItem> db = new List<ProductItem>();
                                        // Create a new Student object.


                                        // Loop through each row of the DataTable.
                                        foreach (DataRow row in dt.Rows)
                                        {
                                            var product = new ProductItem();
                                            // Set the properties of the Student object.
                                            product.Name = row["Name"].ToString();
                                            product.Model = row["Model"].ToString();
                                            product.Price = int.Parse(row["Price"].ToString());
                                            product.Rating = int.Parse(row["Rating"].ToString());
                                            product.ImageLink = row["ImageLink"].ToString();
                                            product.IsDeleted = false;
                                            product.SellerID = Convert.ToInt32(Session["Sellerlogin"]);
                                            product.CreatedOn = DateTime.Now;
                                            db.Add(product);

                                        }


                                        string data = JsonConvert.SerializeObject(db);
                                        StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                                        HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/FileImport", content).Result;

                                        if (response.IsSuccessStatusCode)
                                        {
                                            var temp = response.Content.ReadAsStringAsync().Result;

                                            return RedirectToAction("Existing", "Seller");
                                        }


                                    }
                                    else
                                    {
                                        
                                            string store = "D:/";

                                            string fileName = "Error_"+ DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                                        using (var workbook = new XLWorkbook())
                                        {
                                            var worksheet = workbook.Worksheets.Add("Sheet1");
                                            worksheet.Cell(1, 1).InsertData(output);
                                            workbook.SaveAs(store + fileName);
                                            // return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", output);


                                            using (MemoryStream stream = new MemoryStream())
                                            {
                                                workbook.SaveAs(stream);
                                                return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",fileName);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                    catch (Exception e)
                    {
                        return View(e);
                    }
                    
                }

            }
            return View();

        }
        
    }
}