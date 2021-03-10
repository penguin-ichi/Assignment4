using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibraryAs4;

namespace Assignment4
{
    public partial class frmBookReport : Form
    {
        public frmBookReport()
        {
            InitializeComponent();
        }
        List<BookDTO> listBook = null;
        BookDAO dao = new BookDAO();

        public void LoadBooksDesc()
        {
            listBook = dao.GetListBookDesc();
            dgvBookReport.DataSource = listBook;
        }

        private void frmBookReport_Load(object sender, EventArgs e)
        {
            LoadBooksDesc();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmMaintainBooks frmMaintain = new frmMaintainBooks();
            this.Close();
            frmMaintain.Show();
        }

        private void frmBookReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
