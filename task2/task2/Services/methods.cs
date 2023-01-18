using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using task2.Models;

namespace task2.Services
{
    interface Imethods
    {
        dynamic GetStaff(staff staff);
        dynamic PostStaff(staff staff);
        dynamic PutStaff(staff staff);
        dynamic DeleteStaff(staff staff);
    }

    public class Methods : Imethods
    {
        /*public dynamic deleted(doj) {
            if (staff.Isdeleted == true)
            {
                return "Data already deleted";
            }
            return false;
           }*/
        public dynamic GetStaff(staff staff)
        {
            /* using (StudentEntities1 db = new StudentEntities1())
           
                 var obj = db.staffs.ToList();
                 return obj;
             }*/


            using (StudentEntities1 db = new StudentEntities1())
            {

                /*var doj = from st in db.staffs where st.DateOfJoin == staff.DateOfJoin ||st.Name.Contains(staff.Name)|| st.ID == staff.ID && st.Isdeleted == false select st;*/
                // var doj1 = db.staffs.AsQueryable().Where(x => x.Isdeleted == false);
                // List<staff> obj=new List<staff>();
                 
                if (staff != null)
                {
                   var  obj= db.staffs.AsQueryable().Where(x => x.Isdeleted == false).Where(s => s.DateOfJoin == staff.DateOfJoin|| s.ID == staff.ID||  s.Name == staff.Name).AsQueryable();
                  
                    if (obj!=null)
                    {
                        return obj.ToList();
                    }
                    else
                    {
                        return "data not found";
                    }
                }
                else
                {
                    return "data not found";
                }

            }
        }

        public dynamic PostStaff(staff staff)
        {
            using (StudentEntities1 db = new StudentEntities1())
            {
                db.staffs.Add(staff);
                db.SaveChanges();
            }
            return staff;
        }
        public dynamic PutStaff(staff staff)
        {
            using (StudentEntities1 db = new StudentEntities1())
            {

                var val1 = (from st in db.staffs where st.ID == staff.ID && st.Isdeleted == false select staff).FirstOrDefault();
                if (val1 != null)
                {

                    val1.ID = 1;
                    val1.Name = staff.Name;
                    val1.Username = staff.Username;
                    val1.Password = staff.Name;
                    val1.DateOfJoin = staff.DateOfJoin;
                    val1.Isdeleted = false;
                    db.Entry(val1).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return val1;
            }
        }

        public dynamic DeleteStaff(staff staff)
        {
            using (var db = new StudentEntities1())
            {
                var st = db.staffs.Where(s => s.Isdeleted == false && s.ID == staff.ID).FirstOrDefault();
                    
                if (st!=null)
                {
                    st.Isdeleted = true;
                    db.Entry(st).State = EntityState.Modified;
                    db.SaveChanges();   
                }

                else
                {
                    return "already deleted";
                }

                return "deleted successfully";
            }
        }
    }
}