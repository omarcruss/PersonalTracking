using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTO;
using DAL;
namespace DAL.DAO
{
    public class TaskDAO : EmployeeContext
    {
        public static List<TASKSTATE> GetTaskStates()
        {
            return db.TASKSTATEs.ToList();
        }

        public static void AddTask(TASK task)
        {
            try
            {
                db.TASKs.InsertOnSubmit(task);
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<TaskDetailDTO> GetTasks()
        {
            List<TaskDetailDTO> tasklist = new List<TaskDetailDTO>();
            var list = (from t in db.TASKs
                        join s in db.TASKSTATEs on t.TaskState equals s.ID
                        join e in db.EMPOLYEEs on t.EmployeeID equals e.ID
                        join d in db.DEPARTMENTs on e.DepartmentID equals d.ID
                        join p in db.POSITIONs on e.PositionID equals p.ID
                        select new
                        {
                            taskID = t.ID,
                            title = t.TaskTitle,
                            content = t.TaskContent,
                            startDate = t.TaskStartDate,
                            deliveryDate = t.TaskDeliveryDate,
                            taskStateName = s.StateName,
                            taskStateID = t.TaskState,
                            userNo = e.UserNumber,
                            name = e.Name,
                            emplyeeID = t.EmployeeID,
                            surname = e.Surname,
                            positionName = p.PositionName,
                            departmentName = d.DepartmentName,
                            positionID = e.PositionID,
                            departmentID = e.DepartmentID
                        }).OrderBy(x => x.startDate).ToList();
            foreach (var item in list)
            {
                TaskDetailDTO dto = new TaskDetailDTO();
                dto.TaskID = item.taskID;
                dto.Title = item.title;
                dto.Content = item.content;
                dto.TaskStartDate = item.startDate;
                dto.TaskDeliveryDate = item.deliveryDate;
                dto.TaskStateName = item.taskStateName;
                dto.taskStateID = item.taskStateID;
                dto.UserNo = item.userNo;
                dto.Name = item.name;
                dto.EmployeeID = item.emplyeeID;
                dto.Surname = item.surname;
                dto.PositionName = item.positionName;
                dto.DepartmentName = item.departmentName;
                dto.PositionID = item.positionID;
                dto.DepartmentID = item.departmentID;
                tasklist.Add(dto);
            }
            return tasklist;
        }

        public static void ApproveTask(int taskID, bool isAdmin)
        {
            try
            {
                TASK tsk = db.TASKs.First(x => x.ID == taskID);
                if (isAdmin)
                {
                    tsk.TaskState = TaskStates.Approved;
                }
                else
                {
                    tsk.TaskState = TaskStates.Delivered;
                    tsk.TaskDeliveryDate = DateTime.Today;
                    db.SubmitChanges();
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DeleteTask(int taskID)
        {
            try
            {
                TASK tsk = db.TASKs.First(x => x.ID == taskID);
                db.TASKs.DeleteOnSubmit(tsk);
                db.SubmitChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void UpdateTask(TASK update)
        {
            try
            {
                TASK task = db.TASKs.First(x => x.ID == update.ID);
                task.TaskTitle = update.TaskTitle;
                task.TaskContent = update.TaskContent;
                task.TaskState = update.TaskState;
                task.EmployeeID = update.EmployeeID;
                db.SubmitChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
