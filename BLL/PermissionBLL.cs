using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DAL.DAO;
using DAL.DTO;

namespace BLL
{
    public class PermissionBLL
    {
        public static void AddPermission(PERMISSION permission)
        {
            PermisionDAO.AddPermission(permission);
        }

        public static PermissionDTO GetAll()
        {
            PermissionDTO dto = new PermissionDTO();
            dto.Departments = DepartmentDAO.GetDepartments();
            dto.Positions = PositionDAO.GetPositions();
            dto.States = PermisionDAO.GetStates();
            dto.Permissions = PermisionDAO.GetPermissions();
            return dto;
        }

        public static void UpdatePermission(PERMISSION permission)
        {
            PermisionDAO.UpdatePermission(permission);
        }

        public static void UpdatePermission(int permissionID, int approved)
        {
            PermisionDAO.UpdatePermission(permissionID, approved);
        }

        public static void DeletePermission(int permissionID)
        {
            PermisionDAO.DeletePermission(permissionID);
        }
    }
}
