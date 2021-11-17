using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmTaskList : Form
    {
        public FrmTaskList()
        {
            InitializeComponent();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TaskDTO dto = new TaskDTO();
        private bool combofull = false;
        void FillAllData()
        {
            dto = TaskBLL.GetAll();
            if (!UserStat.isAdmin)
            {
                dto.Tasks = dto.Tasks.Where(x => x.EmployeeID == UserStat.EmployeeID).ToList();
            }
            dataGridView1.DataSource = dto.Tasks;

            combofull = false;
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            cmbTaskState.DataSource = dto.TaskStates;
            cmbTaskState.DisplayMember = "StateName";
            cmbTaskState.ValueMember = "ID";
            cmbTaskState.SelectedIndex = -1;
        }
        private void FrmTaskList_Load(object sender, EventArgs e)
        {
            FillAllData();
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = "User No";
            dataGridView1.Columns[2].HeaderText = "Name";
            dataGridView1.Columns[3].HeaderText = "Surname";
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].HeaderText = "Task State";
            dataGridView1.Columns[12].HeaderText = "Start Date";
            dataGridView1.Columns[13].HeaderText = "Delivery Date";
            dataGridView1.Columns[9].HeaderText = "Task Title";
            dataGridView1.Columns[14].Visible = false;

            if (!UserStat.isAdmin)
            {
                btnNew.Visible = false;
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnClose.Location = new Point(423, 16);
                btnApprove.Location = new Point(254, 16);
                pnlForAdmin.Hide();
                btnApprove.Text = "Delivery";
            }

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            FrmTask frm = new FrmTask();
            frm.Hide();
            frm.ShowDialog();
            this.Visible = true;
            FillAllData();
            CleanFilters();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (detail.TaskID == 0)
            {
                MessageBox.Show("Please select a task on table");
            }
            else
            {
                FrmTask frm = new FrmTask();
                frm.isUpdate = true;
                frm.detail = detail;
                frm.Hide();
                frm.ShowDialog();
                this.Visible = true;
                FillAllData();
                CleanFilters();
            }
        }

        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID ==
                Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            List<TaskDetailDTO> list = dto.Tasks;
            if (txtUserNo.Text.Trim() != "")
            {
                list = list.Where(x => x.UserNo == Convert.ToInt32(txtUserNo.Text)).ToList();
            }
            if (txtName.Text.Trim() != "")
            {
                list = list.Where(x => x.Name.Contains(txtName.Text)).ToList();
            }
            if (txtSurname.Text.Trim() != "")
            {
                list = list.Where(x => x.Surname.Contains(txtSurname.Text)).ToList();
            }
            if (cmbDepartment.SelectedIndex != -1)
            {
                list = list.Where(x => x.DepartmentID == Convert.ToInt32(cmbDepartment.SelectedValue)).ToList();
            }
            if (cmbPosition.SelectedIndex != -1)
            {
                list = list.Where(x => x.PositionID == Convert.ToInt32(cmbPosition.SelectedValue)).ToList();
            }
            if (rbStartDate.Checked)
            {
                list = list.Where(x => x.TaskStartDate > Convert.ToDateTime(dbStart.Value) && 
                x.TaskStartDate < Convert.ToDateTime(dbEnd.Value)).ToList();
            }
            if (rbDeliveryDate.Checked)
            {
                list = list.Where(x => x.TaskDeliveryDate > Convert.ToDateTime(dbStart.Value) && 
                x.TaskDeliveryDate< Convert.ToDateTime(dbEnd.Value)).ToList();
            }
            if (cmbTaskState.SelectedIndex != -1)
            {
                list = list.Where(x => x.taskStateID == Convert.ToInt32(cmbTaskState.SelectedValue)).ToList();
            }
            dataGridView1.DataSource = list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            CleanFilters();
        }

        private void CleanFilters()
        {
            txtUserNo.Clear();
            txtName.Clear();
            txtSurname.Clear();
            combofull = false;
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            cmbPosition.DataSource = dto.Positions;
            combofull = true;
            rbDeliveryDate.Checked = false;
            rbStartDate.Checked = false;
            cmbTaskState.SelectedIndex = -1;
            dataGridView1.DataSource = dto.Tasks;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        TaskDetailDTO detail = new TaskDetailDTO();
        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            detail.Title = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
            detail.Name = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            detail.Surname = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            detail.TaskStartDate= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[12].Value);
            detail.TaskDeliveryDate= Convert.ToDateTime(dataGridView1.Rows[e.RowIndex].Cells[13].Value);
            detail.Content = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
            detail.UserNo = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[1].Value);
            detail.taskStateID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[14].Value);
            detail.TaskID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[8].Value);
            detail.EmployeeID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
            detail.TaskStateName = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete this task?", "Warning!", MessageBoxButtons.YesNo);
            if (DialogResult.Yes == result)
            {
                TaskBLL.DeleteTask(detail.TaskID);
                MessageBox.Show("Task was deleted");
                FillAllData();
                CleanFilters();
            }
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (UserStat.isAdmin && detail.taskStateID == TaskStates.OnEmployee && detail.EmployeeID != UserStat.EmployeeID)
            {
                MessageBox.Show("Before approve a task employee have to delivery task");
            }
            else if (UserStat.isAdmin && detail.taskStateID == TaskStates.Approved)
            {
                MessageBox.Show("This task is already approved");
            }
            else if (!UserStat.isAdmin && detail.taskStateID == TaskStates.Delivered)
            {
                MessageBox.Show("This task is already delivered");
            }
            else if (!UserStat.isAdmin && detail.taskStateID == TaskStates.Approved)
            {
                MessageBox.Show("This task is already approved");
            }
            else
            {
                TaskBLL.ApproveTask(detail.TaskID, UserStat.isAdmin);
                MessageBox.Show("Task was updated");
                FillAllData();
                CleanFilters();
            }
        }

        private void txtExcel_Click(object sender, EventArgs e)
        {
            ExportToExcel.ExcelExport(dataGridView1);
        }
    }
}
