using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class DepartmentDAO : EmployeeContext
    {
        public static void AddDepartment(DEPARTMENT department)
        {
            try
            {
                db.DEPARTMENTs.Add(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<DEPARTMENT> GetDepartments()
        {
            return db.DEPARTMENTs.ToList();
        }

        public static void UpdateDepartment(int departmentID, string departmentName)
        {
            try
            {
                DEPARTMENT dep = db.DEPARTMENTs.First(x => x.ID == departmentID);
                dep.DepartmentName = departmentName;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void DeleteDepartment(int iD)
        {
            try
            {
                DEPARTMENT dep = db.DEPARTMENTs.First(x => x.ID == iD);
                db.DEPARTMENTs.Remove(dep);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
