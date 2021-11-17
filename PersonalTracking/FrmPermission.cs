using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
using DAL.DTO;

namespace PersonalTracking
{
    public partial class FrmPermission : Form
    {
        public FrmPermission()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        TimeSpan permissionDay;
        public bool isUpdate = false;
        public PermissionDetailDTO detail = new PermissionDetailDTO();
        private void FrmPermission_Load(object sender, EventArgs e)
        {
            txtUserNo.Text = UserStat.UserNo.ToString();
            if (isUpdate)
            {
                dbStart.Value = detail.StartDate;
                dbEnd.Value = detail.EndDate;
                txtDayAmount.Text = detail.PermissionDayAmount.ToString();
                txtUserNo.Text = detail.UserNo.ToString();
            }
        }

        private void dbStart_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dbEnd.Value.Date - dbStart.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString();
        }

        private void dbEnd_ValueChanged(object sender, EventArgs e)
        {
            permissionDay = dbEnd.Value.Date - dbStart.Value.Date;
            txtDayAmount.Text = permissionDay.TotalDays.ToString(); 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDayAmount.Text.Trim() == "")
            {
                MessageBox.Show("Please change end or start date");
            }
            else if (Convert.ToInt32(txtDayAmount.Text) <= 0)
            {
                MessageBox.Show("Permission day must be bigger than 0");
            }
            else if (txtExplanation.Text.Trim() == "")
            {
                MessageBox.Show("Explanation is empty");
            }
            else
            {
                PERMISSION permission = new PERMISSION();
                if (!isUpdate)
                {
                    permission.EmployeeID = UserStat.EmployeeID;
                    permission.PermissionStartDate = dbStart.Value.Date;
                    permission.PermissionEndDate = dbEnd.Value.Date;
                    permission.PermissionState = 1;
                    permission.PermissionDay = Convert.ToInt32(permission.PermissionDay);
                    permission.PermissionExplanation = txtExplanation.Text;
                    PermissionBLL.AddPermission(permission);
                    MessageBox.Show("Permission was added");
                    permission = new PERMISSION();
                    dbStart.Value = DateTime.Today;
                    dbEnd.Value = DateTime.Today;
                    txtExplanation.Clear();
                    txtDayAmount.Clear();
                }
                else if (isUpdate)
                {
                    DialogResult result = MessageBox.Show("Are you sure", "Warning", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        permission.ID = detail.PermissionID;
                        permission.PermissionExplanation = txtExplanation.Text;
                        permission.PermissionStartDate = dbStart.Value;
                        permission.PermissionEndDate = dbEnd.Value;
                        permission.PermissionDay = Convert.ToInt32(txtDayAmount.Text);
                        PermissionBLL.UpdatePermission(permission);
                        MessageBox.Show("Permission was updated");
                        this.Close();
                    }
                }
            }
        }
    }
}
