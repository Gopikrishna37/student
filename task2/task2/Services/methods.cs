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
        
        public dynamic GetStaff(staff staff)
        {
            /* using (StudentEntities1 db = new StudentEntities1())
           
                 var obj = db.staffs.ToList();
                 return obj;
             }*/


            using (StudentEntities1 db = new StudentEntities1())
            {

                 
                if (staff != null)
                {
                    var obj = db.staffs.Where(x => x.Isdeleted == false).AsQueryable();
                    /*.Where(s => s.DateOfJoin == staff.DateOfJoin|| s.ID == staff.ID||  s.Name == staff.Name).AsQueryable();*/

                    if (!string.IsNullOrEmpty(staff.Name))
                    {
                        obj = obj.Where(s => s.Name.Contains(staff.Name)).AsQueryable();
                    }
                    if (staff.DateOfJoin!=null)
                    {
                        obj = obj.Where(s => s.DateOfJoin== staff.DateOfJoin).AsQueryable();
                    }
                    if (staff.ID >0)
                    {
                        obj = obj.Where(s => s.ID == staff.ID).AsQueryable();
                    }
                    
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

                var val1 = (from st in db.staffs where st.ID == staff.ID && st.Isdeleted == false select st).FirstOrDefault();
                if (val1 != null)
                {
                    val1.Name =staff.Name==null? val1.Name :staff.Name;
                    val1.Username = staff.Username == null ? val1.Username : staff.Username; 
                    val1.Password = staff.Password == null ? val1.Password : staff.Password; 
                    val1.DateOfJoin = staff.DateOfJoin==null  ? val1.DateOfJoin : staff.DateOfJoin; 
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