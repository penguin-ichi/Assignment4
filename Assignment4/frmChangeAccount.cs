using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Assignment4
{
    public partial class frmChangeAccount : Form
    {
        public frmChangeAccount(string EmpID, string EmpPass, bool EmpRole)
        {
            InitializeComponent();
            this.EmpID = EmpID;
            this.EmpPassword = EmpPass;
            this.EmpRole = EmpRole;
        }

        public string EmpID { get; set; }
        public string EmpPassword { get; set; }
        public bool EmpRole { get; set; }

        string ConnectionString = @"server=DESKTOP-DLMULPB;database=BookStore;uid=sa;pwd=tam";

        private void frmChangeAccount_Load(object sender, EventArgs e)
        {
            txtEmpID.Text = EmpID;
            txtEmpPassword.Text = EmpPassword;
            txtEmpRole.Text = EmpRole.ToString();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            this.Close();
            frmLogin.Show();
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            string SQL = @"UPDATE Employee SET EmpPassword = @pass WHERE EmpID = @id";
            SqlCommand command = new SqlCommand(SQL, connection);

            command.Parameters.AddWithValue("id", txtEmpID.Text);
            command.Parameters.AddWithValue("pass", txtEmpPassword.Text);
            connection.Open();;
            command.ExecuteNonQuery();
            MessageBox.Show("Change password successful.", "Announcement");
            connection.Close();
        }

        private void frmChangeAccount_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
