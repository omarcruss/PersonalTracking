using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DAO;
using DAL;

namespace BLL
{
    public class SalaryBLL
    {
        public static SalaryDTO GetAll()
        {
            SalaryDTO salarydto = new SalaryDTO();
            salarydto.Employees = EmployeeDAO.GetEmployees();
            salarydto.Departments = DepartmentDAO.GetDepartments();
            salarydto.Positions = PositionDAO.GetPositions();
            salarydto.Months = SalaryDAO.GetMonths();
            salarydto.Salaries = SalaryDAO.GetSalaries();
            return salarydto;
        }

        public static void AddSalary(SALARY salary, bool control)
        {
            SalaryDAO.AddSalary(salary);
            if (control)
            {
                EmployeeDAO.UpdateEmplyee(salary.EmpolyeeID, salary.Amount);
            }
        }

        public static void UpdateSalary(SALARY salary, bool control)
        {
            SalaryDAO.UpdateSalary(salary);
            if (control)
            {
                EmployeeDAO.UpdateEmplyee(salary.EmpolyeeID, salary.Amount);
            }
        }

        public static void DeleteSalary(int salaryID)
        {
            SalaryDAO.DeleteSalary(salaryID);
        }
    }
}
