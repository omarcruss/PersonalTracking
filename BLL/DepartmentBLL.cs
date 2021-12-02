using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;

namespace BLL
{
    public class DepartmentBLL
    {
        public static void AddDepartment(string departmentName)
        {
            var department = new DEPARTMENT();
						department.DepartmentName = departmentName;

            DepartmentDAO.AddDepartment(department);
        }

        public static List<DEPARTMENT> GetDepartments()
        {
            return DepartmentDAO.GetDepartments();
        }

        public static void UpdateDepartment(int departmentID, string departmentName)
        {
            DepartmentDAO.UpdateDepartment(departmentID, departmentName);
        }

        public static void DeleteDepartment(int iD)
        {
            DepartmentDAO.DeleteDepartment(iD);
        }
    }
}
