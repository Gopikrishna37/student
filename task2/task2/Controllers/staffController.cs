using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using task2.Models;
using task2.Services;

namespace task2.Controllers
{
    public class StaffController : ApiController
    {
        [HttpPost]
        public dynamic Post(staff staff)
        {
            try
            {
                /* using (StudentEntities1 db = new StudentEntities1())
                 {
                     db.staffs.Add(staff);
                     db.SaveChanges();
                     return Json(staff);
                 }*/

                Imethods m = new Methods();
                return Json(m.PostStaff(staff));

            }
            catch (Exception e)
            {
                return Json("Failed to success");
            }
        }


/*
        [HttpGet]
        public dynamic Getall()

        {

            try
            {
                Imethods m = new Methods();
                return Json(m.GetStaff());

            }
            catch (Exception e)
            {
                return e;
            }
        }*/

        [HttpGet]
        public dynamic Get(staff staff)
        {

            try
            {
                Imethods m = new Methods();
                return Json(m.GetStaff(staff));

            }
            catch (Exception e)
            {
                return e;
            }
        }


        [HttpPut]
        public dynamic Put(staff staff)
        {
            try
            {
                using(StudentEntities1 db=new StudentEntities1())
                {
                    var st=db.staffs.Find(staff.ID);
                   
                    if (staff != null)
                    {
                        db.Entry(staff).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    return Json(staff);
                    /* Imethods m = new Methods();
                     return Json(m.PutStaff(staff));*/
                }

            }
            catch(Exception ex)
            {
                return ex;
            }
        }

        /*[HttpDelete]
        public dynamic Delete(staff staff)
        {
            try
            {
                using(StudentEntities1 db=new StudentEntities1())
                {
                    
                    if (staff != null)
                    {
                        db.staffs.Remove(db.staffs.FirstOrDefault(e => e.ID == staff.ID));
                        db.SaveChanges();
                    }
                    return Json(staff);
                }
            }
            catch(Exception e)
            {
            }
        }*/

        [HttpDelete]
        public dynamic Delete(staff staff)
        {
            try
            {
                if (staff.ID <= 0)
                    return BadRequest("Not a valid student id");
                Imethods m = new Methods();
                return Json(m.DeleteStaff(staff));
                
            }
            catch(Exception Ex)
            {
                return Ex;
            }

        }
    }
}
