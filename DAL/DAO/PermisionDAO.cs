using DAL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO;
using DAL.DTO;

namespace DAL.DAO
{
    public class PermisionDAO : EmployeeContext
    {
        public static void AddPermission(PERMISSION permission)
        {
            try
            {
                db.PERMISSIONs.InsertOnSubmit(permission);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static List<PERMISSIONSTATE> GetStates()
        {
            return db.PERMISSIONSTATEs.ToList();
        }

        public static List<PermissionDetailDTO> GetPermissions()
        {
            List<PermissionDetailDTO> permissions = new List<PermissionDetailDTO>();
            var list = (from p in db.PERMISSIONs
                        join s in db.PERMISSIONSTATEs on p.PermissionState equals s.ID
                        join e in db.EMPOLYEEs on p.EmployeeID equals e.ID
                        select new
                        {
                            UserNo = e.UserNumber,
                            Name = e.Name,
                            Surname = e.Surname,
                            StateName = s.StateName,
                            stateID = p.PermissionState,
                            startDate = p.PermissionStartDate,
                            endDate = p.PermissionEndDate,
                            employeeID = p.EmployeeID,
                            PermissionID = p.ID,
                            explanation = p.PermissionExplanation,
                            DayAmount = p.PermissionDay,
                            departmentID = e.DepartmentID,
                            positionID = e.PositionID
                        }).OrderBy(x => x.startDate).ToList();
            foreach (var item in list)
            {
                PermissionDetailDTO dto = new PermissionDetailDTO();
                dto.UserNo = item.UserNo;
                dto.Name = item.Name;
                dto.Surname = item.Surname;
                dto.StateName = item.StateName;
                dto.EmployeeID = item.employeeID;
                dto.State = item.stateID;
                dto.StartDate = item.startDate;
                dto.EndDate = item.endDate;
                dto.EmployeeID = item.employeeID;
                dto.PermissionDayAmount = item.DayAmount;
                dto.Explanation = item.explanation;
                dto.PositionID = item.positionID;
                dto.PermissionID = item.PermissionID;
                dto.DepartmentID = item.departmentID;
                permissions.Add(dto);
            }
            return permissions;
        }

        public static void DeletePermission(int permissionID)
        {
            try
            {
                PERMISSION pr = db.PERMISSIONs.First(x => x.ID == permissionID);
                db.PERMISSIONs.DeleteOnSubmit(pr);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            try
            {
                PERMISSION pr = db.PERMISSIONs.First(x => x.ID == permissionID);
                pr.PermissionState = approved;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            try
            {
                PERMISSION pr = db.PERMISSIONs.First(x => x.ID == permission.ID);
                pr.PermissionStartDate = permission.PermissionStartDate;
                pr.PermissionEndDate = permission.PermissionEndDate;
                pr.PermissionExplanation = permission.PermissionExplanation;
                pr.PermissionDay = permission.PermissionDay;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
