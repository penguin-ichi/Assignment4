using LibraryAs4;
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
    public partial class frmMaintainBooks : Form
    {
        public frmMaintainBooks()
        {
            InitializeComponent();
        }

        List<BookDTO> listBook = null;
        BookDAO dao = new BookDAO();

        public void LoadBooks(string bookName)
        {
            listBook = dao.GetListBook(bookName);
            dgvBookList.DataSource = listBook;
        }

        private void frmMaintainBooks_Load(object sender, EventArgs e)
        {
            LoadBooks(txtSearch.Text);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            this.Close();
            frmLogin.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadBooks(txtSearch.Text);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            dgvBookList.CurrentCell = dgvBookList.Rows[0].Cells[0];
            dgvBookList.Rows[0].Selected = true;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            dgvBookList.CurrentCell = dgvBookList.Rows[listBook.Count - 1].Cells[0];
            dgvBookList.Rows[listBook.Count - 1].Selected = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int index = dgvBookList.CurrentRow.Index + 1;
            if (index >= listBook.Count)
            {
                MessageBox.Show("End Book.", "Annou");
            }
            else
            {
                dgvBookList.CurrentCell = dgvBookList.Rows[index].Cells[0];
                dgvBookList.Rows[index].Selected = true;
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            int index = dgvBookList.CurrentRow.Index - 1;
            if (index < 0)
            {
                MessageBox.Show("First Book.", "Annou");
            }
            else
            {
                dgvBookList.CurrentCell = dgvBookList.Rows[index].Cells[0];
                dgvBookList.Rows[index].Selected = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string BookID = txtBookID.Text;
            string BookName = txtBookName.Text;
            string BookPrice = txtBookPrice.Text;

            if(BookID.Equals("") || BookName.Equals("") || BookPrice.Equals(""))
            {
                MessageBox.Show("Fill all information!");
            } else
            {
                BookDTO dto = new BookDTO(int.Parse(BookID), BookName, float.Parse(BookPrice));
                dao.InsertBook(dto);
                LoadBooks(txtSearch.Text);
            }
        }

        private void txtBookID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtBookPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string id = txtBookID.Text;
            if ("".Equals(id))
            {
                MessageBox.Show("Input id to remove!");
            }
            else
            {
                bool check = dao.DeleteBook(int.Parse(id));
                if(check)
                {
                    MessageBox.Show("Delete Success!");
                    LoadBooks(txtSearch.Text);
                }
                else
                {
                    MessageBox.Show("Delete fail!");
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string BookID = txtBookID.Text;
            string BookName = txtBookName.Text;
            string BookPrice = txtBookPrice.Text;

            if (BookID.Equals("") || BookName.Equals("") || BookPrice.Equals(""))
            {
                MessageBox.Show("Fill all information!");
            }
            else
            {
                BookDTO dto = new BookDTO(int.Parse(BookID), BookName, float.Parse(BookPrice));
                bool check = dao.UpdateBook(dto);
                if (check)
                {
                    MessageBox.Show("Update success!");
                    LoadBooks(txtSearch.Text);
                }
                else
                {
                    MessageBox.Show("Id is not exist!");
                }
            }
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmBookReport frmBook = new frmBookReport();
            this.Close();
            frmBook.Show();
        }

        private void frmMaintainBooks_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
