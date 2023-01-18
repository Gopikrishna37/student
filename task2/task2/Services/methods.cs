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
             {
                 var obj = db.staffs.ToList();
                 return obj;
             }*/
            using (StudentEntities1 db = new StudentEntities1())
            {

                var name1 = (from st in db.staffs where st.Name.Contains(staff.Name) select st).ToList();
                var id = (from st in db.staffs where st.ID == staff.ID select st).ToList();
                var doj = (from st in db.staffs where st.DateOfJoin == staff.DateOfJoin select st).ToList();

                if (doj.Count() == 1)
                {
                    return doj;
                }
                else
                if (id.Count() == 1)
                {
                    return id;
                }
                else
                if (name1.Count() == 1)
                {
                    return name1;
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

                var val1 = from st in db.staffs where st.ID == staff.ID select staff;
                if (val1 != null)
                {
                    db.Entry(staff).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return staff;
            }
        }

        public dynamic DeleteStaff(staff staff)
        {
            using (var db = new StudentEntities1())
            {
                List<staff>  st = (from x in db.staffs
                    .Where(s => s.ID == staff.ID)
                         select x).ToList();
                if (st != null)
                {
                    if (st[0].Isdeleted == false)
                    {
                        st[0].Isdeleted =true;
                        db.Entry(st[0]).State = EntityState.Modified;
                        db.SaveChanges();

                    }
                    else
                    {
                        return "already deleted";
                    }
                }

                return "deleted successfully";
            }
        }
    }
}