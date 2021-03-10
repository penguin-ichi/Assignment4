using System;
using System.Windows.Forms;
using LibraryAs4;

namespace Assignment4
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            EmployeeDAO dao = new EmployeeDAO();
            string empID = txtEmpID.Text;
            string pass = txtEmpPassword.Text;

            if(empID.Equals("") || pass.Equals(""))
            {
                lbError.Text = "Id or Pass can not empty";
            }
            else
            {
                EmployeeDTO dto = dao.CheckLogin(empID, pass);
                if(dto == null)
                {
                    Console.WriteLine(pass);
                    lbError.Text = "Id or Pass is wrong";
                }
                else
                {
                    if(dto.EmpRole)
                    {
                        frmMaintainBooks frmMaintain = new frmMaintainBooks();
                        this.Hide();
                        frmMaintain.Show();
                        
                    }
                    else
                    {
                        frmChangeAccount frmChange = new frmChangeAccount(dto.EmpID, dto.EmpPassword, dto.EmpRole);
                        this.Hide();
                        frmChange.Show();
                    }
                }
            }
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
