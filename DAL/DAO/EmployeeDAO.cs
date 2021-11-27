using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DAO
{
    public class EmployeeDAO : EmployeeContext
    {
        public static void AddEmployee(EMPOLYEE employee)
        {
            try
            {
                db.EMPOLYEEs.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<EmployeeDetailDTO> GetEmployees()
        {
            List<EmployeeDetailDTO> employeeList = new List<EmployeeDetailDTO>();
            var list = (from e in db.EMPOLYEEs
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        {
                            UserNo = e.UserNumber,
                            Password = e.Password,
                            Name = e.Name,
                            Surname = e.Surname,
                            ImagePath = e.ImagePath,
                            Salary = e.Salary,
                            DepartmentName = d.DepartmentName,
                            PositionName = p.PositionName,
                            DepartmentID = d.ID,
                            PositionID = p.ID,
                            BirthDay = e.BirthDay,
                            Adress = e.Adress,
                            EmplyeeID = e.ID,
                            isAdmin = e.isAdmin
                        }).OrderBy(x => x.UserNo).ToList();

            foreach (var item in list)
            {
                EmployeeDetailDTO dto = new EmployeeDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.EmployeeID = item.EmplyeeID;
                dto.Password = item.Password;
                dto.DepartmentID = item.DepartmentID;
                dto.DepartmentName = item.DepartmentName;
                dto.PositionID = item.PositionID;
                dto.PositionName = item.PositionName;
                dto.isAdmin = (bool)item.isAdmin;
                dto.Salary = item.Salary;
                dto.BhirtDay = item.BirthDay;
                dto.Adress = item.Adress;
                dto.ImagePath = item.ImagePath;
                employeeList.Add(dto);

            }
            return employeeList;
        }

        public static void DeleteEmployee(int employeeID)
        {
            try
            {
                EMPOLYEE emp = db.EMPOLYEEs.First(x => x.ID == employeeID);
                db.EMPOLYEEs.Remove(emp);
                db.SaveChanges();
                //List<TASK> tasks = db.TASKs.Where(x => x.EmployeeID == employeeID).ToList();
                //db.TASKs.DeleteAllOnSubmit(tasks);
                //db.SubmitChanges();

                //List<SALARY> salaries = db.SALARies.Where(x => x.EmpolyeeID == employeeID).ToList();
                //db.SALARies.DeleteAllOnSubmit(salaries);
                //db.SubmitChanges();

                //List<PERMISSION> permissions = db.PERMISSIONs.Where(x => x.EmployeeID == employeeID).ToList();
                //db.PERMISSIONs.DeleteAllOnSubmit(permissions);
                //db.SubmitChanges();




            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateEmployee(POSITION position)
        {
            List<EMPOLYEE> list = db.EMPOLYEEs.Where(x => x.PositionID == position.ID).ToList();
            foreach (var item in list)
            {
                item.DepartmentID = position.DepartmentID;
            }
            db.SaveChanges();
        }

        public static void UpdateEmployee(EMPOLYEE employee)
        {
            try
            {
                EMPOLYEE emp = db.EMPOLYEEs.First(x => x.ID == employee.ID);
                emp.UserNumber = employee.UserNumber;
                emp.Name = employee.Name;
                emp.Surname = employee.Surname;
                emp.Password = employee.Password;
                emp.isAdmin = employee.isAdmin;
                emp.BirthDay = employee.BirthDay;
                emp.Adress = employee.Adress;
                emp.DepartmentID = employee.DepartmentID;
                emp.PositionID = employee.PositionID;
                emp.Salary = employee.Salary;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdateEmplyee(int empolyeeID, int amount)
        {
            try
            {
                EMPOLYEE employee = db.EMPOLYEEs.First(x => x.ID == empolyeeID);
                employee.Salary = amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static List<EMPOLYEE> GetEmployees(int v, string text)
        {
            try
            {
                List<EMPOLYEE> list = db.EMPOLYEEs.Where(x => x.UserNumber == v && x.Password == text).ToList();
                return list;
						}
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<EMPOLYEE> GetUsers(int v)
        {
            return db.EMPOLYEEs.Where(x => x.UserNumber == v).ToList();
        }
    }
}
