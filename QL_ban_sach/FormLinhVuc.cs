using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QL_ban_sach
{
    public partial class FormLinhVuc : Form
    {
        DataTable tbllinhvuc;   
        public FormLinhVuc()
        {
            InitializeComponent();
        }

        private void FormLinhVuc_Load(object sender, EventArgs e)
        {
            txtMaLinhVuc.Enabled = false;
            loadDataToGridview();
        }
        private void loadDataToGridview()
        {
            string sql = "Select * From LinhVuc";
            tbllinhvuc = Functions.GetDataToTable(sql);
            dataGridView_LinhVuc.DataSource = tbllinhvuc;
        }
        private void Resetvalue()
        {
            txtMaLinhVuc.Text = "";
            txtTenLinhVuc.Text = "";
        }

        private void dataGridView_LinhVuc_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaLinhVuc.Text = dataGridView_LinhVuc.CurrentRow.Cells["MaLinhVuc"].Value.ToString();
            txtTenLinhVuc.Text = dataGridView_LinhVuc.CurrentRow.Cells["TenLinhVuc"].Value.ToString();
            txtMaLinhVuc.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            Resetvalue();
            txtMaLinhVuc.Enabled = true;
            txtTenLinhVuc.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbllinhvuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu! ");
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã Lĩnh vực");
                txtMaLinhVuc.Focus();
            }
            if (txtTenLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên Lĩnh vực");
                txtTenLinhVuc.Focus();
            }
            sql = "update LinhVuc set TenLinhVuc='" + txtTenLinhVuc.Text + "' where MaLinhVuc ='" + txtMaLinhVuc.Text + "'";
            Functions.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbllinhvuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào");
            }
            else
            {
                if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "delete from LinhVuc where MaLinhVuc= '" + txtMaLinhVuc.Text + "'";
                    Functions.RunSqlDel(sql);
                    loadDataToGridview();
                    Resetvalue();
                }
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (tbllinhvuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }
            if (txtMaLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã Lĩnh vực");
                txtMaLinhVuc.Focus();
            }
            if (txtTenLinhVuc.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên Lĩnh Vực");
                txtMaLinhVuc.Focus();
            }
            sql = "Select MaLinhVuc from LinhVuc where MaLinhVuc ='" + txtMaLinhVuc.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã Lĩnh Vực này đã có bạn phải nhập mã khác");
                txtMaLinhVuc.Focus();
                return;
            }
            sql = "insert into LinhVuc values( '" + txtMaLinhVuc.Text + "' ,'" + txtTenLinhVuc.Text + "')";
            Functions.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Resetvalue();
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMaLinhVuc.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có chắc chắn muốn thoát chương trình không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
}
