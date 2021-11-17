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
using System.IO;

namespace PersonalTracking
{
    public partial class FrmEmployee : Form
    {
        public FrmEmployee()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUserNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }

        private void txtSalary_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtSalary_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = General.isNumber(e);
        }
        EmployeeDTO dto = new EmployeeDTO();
        public EmployeeDetailDTO detail = new EmployeeDetailDTO();
        public bool isUpdate = false;
        string imagePath = "";
        private void Employee_Load(object sender, EventArgs e)
        {
            dto = EmployeeBLL.GetAll();
            cmbDepartment.DataSource = dto.Departments;
            cmbDepartment.DisplayMember = "DepartmentName";
            cmbDepartment.ValueMember = "ID";
            cmbPosition.DataSource = dto.Positions;
            cmbPosition.DisplayMember = "PositionName";
            cmbPosition.ValueMember = "ID";
            cmbDepartment.SelectedItem = -1;
            cmbPosition.SelectedIndex = -1;
            combofull = true;
            if (isUpdate)
            {
                txtName.Text = detail.Name;
                txtSurname.Text = detail.Surname;
                txtUserNo.Text = detail.UserNo.ToString();
                txtPassword.Text = detail.Password;
                chAdmin.Text = detail.isAdmin.ToString();
                txtAddress.Text = detail.Adress;
                dateTimePicker1.Value = Convert.ToDateTime(detail.BhirtDay);
                cmbDepartment.SelectedValue = detail.DepartmentID;
                cmbPosition.SelectedValue = detail.PositionID;
                txtSalary.Text = detail.Salary.ToString();
                imagePath = Application.StartupPath + "\\images\\" + detail.ImagePath;
                txtImagePath.Text = imagePath;
                pictureBox1.ImageLocation = imagePath;
                if (!UserStat.isAdmin)
                {
                    chAdmin.Enabled = false;
                    txtUserNo.Enabled = false;
                    txtSalary.Enabled = false;
                    cmbDepartment.Enabled = false;
                    cmbPosition.Enabled = false;
                }
            }
        }
        bool combofull = false;
        private void cmbDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combofull)
            {
                int departmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                cmbPosition.DataSource = dto.Positions.Where(x => x.DepartmentID == departmentID).ToList();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        string fileName = "";
        private void btnBrowse_Click(object sender, EventArgs e)
        {
             if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
                txtImagePath.Text = openFileDialog1.FileName;
                string Unique = Guid.NewGuid().ToString();
                fileName += Unique + openFileDialog1.SafeFileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("User no is empty");
            }
            
            else if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show("Password is empty");
            }
            else if (txtName.Text.Trim() == "")
            {
                MessageBox.Show("Name is empty");
            }
            else if (txtSurname.Text.Trim() == "")
            {
                MessageBox.Show("Surname is empty");
            }
            else if (txtSalary.Text.Trim() == "")
            {
                MessageBox.Show("Salary is empty");
            }
            else if (cmbDepartment.SelectedIndex == -1)
            {
                MessageBox.Show("Select a department");
            }
            else if (cmbPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Select a position");
            }
            else
            {
                if (!isUpdate)
                {
                    if (!EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text)))
                    {
                        MessageBox.Show("This user no is used by another employee, please change");
                    }
                    else
                    {


                        EMPOLYEE employee = new EMPOLYEE();
                        employee.UserNumber = Convert.ToInt32(txtUserNo.Text);
                        employee.Password = txtPassword.Text;
                        employee.isAdmin = chAdmin.Checked;
                        employee.Name = txtName.Text;
                        employee.Surname = txtSurname.Text;
                        employee.Salary = Convert.ToInt32(txtSalary.Text);
                        employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                        employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                        employee.Adress = txtAddress.Text;
                        employee.BirthDay = dateTimePicker1.Value;
                        employee.ImagePath = fileName;
                        EmployeeBLL.AddEmployee(employee);
                        File.Copy(txtImagePath.Text, @"images\\" + fileName);
                        MessageBox.Show("Employee was added");
                        CleanAllFilters();
                    }
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "title", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        EMPOLYEE employee = new EMPOLYEE();
                        if (txtImagePath.Text != imagePath)
                        {
                            if (File.Exists(@"images\\" + detail.ImagePath))
                            {
                                File.Delete(@"images\\" + detail.ImagePath);
                            }
                            File.Copy(txtImagePath.Text, @"images\\" + fileName);
                            employee.ImagePath = fileName;
                        }
                        else
                        {
                            employee.ImagePath = detail.ImagePath;
                            employee.ID = detail.EmployeeID;
                            employee.UserNumber = Convert.ToInt32(txtUserNo.Text);
                            employee.Name = txtName.Text;
                            employee.Surname = txtSurname.Text;
                            employee.isAdmin = chAdmin.Checked;
                            employee.Password = txtPassword.Text;
                            employee.Adress = txtAddress.Text;
                            employee.BirthDay = dateTimePicker1.Value;
                            employee.DepartmentID = Convert.ToInt32(cmbDepartment.SelectedValue);
                            employee.PositionID = Convert.ToInt32(cmbPosition.SelectedValue);
                            employee.Salary = Convert.ToInt32(txtSalary.Text);
                            EmployeeBLL.UpdateEmployee(employee);
                            MessageBox.Show("Employee was updated");
                            this.Close();
                        }
                    }
                }
            }
        }
        public void CleanAllFilters()
        {
            txtUserNo.Clear();
            txtPassword.Clear();
            chAdmin.Checked = false;
            txtName.Clear();
            txtSurname.Clear();
            txtImagePath.Clear();
            txtSalary.Clear();
            cmbDepartment.SelectedIndex = -1;
            cmbPosition.SelectedValue = -1;
            combofull = false;
            pictureBox1.Image = null;
            txtAddress.Clear();
            cmbPosition.DataSource = dto.Positions;
            dateTimePicker1.Value = DateTime.Today;
        }
        bool isUnique = false;
        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (txtUserNo.Text.Trim() == "")
            {
                MessageBox.Show("UserNo is empty");
            }
            else
            {
                isUnique = EmployeeBLL.isUnique(Convert.ToInt32(txtUserNo.Text));
                if (!isUnique)
                {
                    MessageBox.Show("This UserNo is used by another employee, please change");
                }
                else
                {
                    MessageBox.Show("This UserNo is usable");
                }
            }
        }
    }
}

