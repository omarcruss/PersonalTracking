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

namespace PersonalTracking
{
    public partial class FrmDepartment : Form
    {
        private int _departmentId;
        private string _departmentName;
        private bool _isUpdate;

        private FrmDepartment()
        {
            InitializeComponent();
        }

        public FrmDepartment(string departmentName = null, int departmentId = 0) : this()
        {
            _departmentName = departmentName;
            _departmentId = departmentId;

            _isUpdate = string.IsNullOrEmpty(_departmentName) || _departmentId != 0;
        }

        private void txtDepartment_Click(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDepartment.Text.Trim()=="")
            {
                MessageBox.Show("Please fill the name");
            }
            else
            {
                if (!_isUpdate)
                {
                    string departmentName = txtDepartment.Text;
                    DepartmentBLL.AddDepartment(departmentName);
                    MessageBox.Show("Department was added");
                    txtDepartment.Clear();
                }
                else
                {
                    DialogResult result = MessageBox.Show("Are you sure?", "Warning!", MessageBoxButtons.YesNo);
                    if (DialogResult.Yes == result)
                    {
                        string departmentName = txtDepartment.Text;
                        DepartmentBLL.UpdateDepartment(_departmentId, departmentName);
                        MessageBox.Show("Department was updated");
                        this.Close();
                    }
                }
            }
        }

        private void FrmDepartment_Load(object sender, EventArgs e)
        {
            if (_isUpdate)
            {
                txtDepartment.Text = _departmentName;
            }
        }
    }
}
