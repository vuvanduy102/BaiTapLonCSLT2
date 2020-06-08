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
    public partial class FormNhaCungCap : Form
    {
        DataTable tblNhacungcap;
        public FormNhaCungCap()
        {
            InitializeComponent();
        }

        private void FormNhaCungCap_Load(object sender, EventArgs e)
        {
            txtMaNCC.Enabled = false;
            loadDataToGridview();
        }
        private void loadDataToGridview()
        {
            string sql = "Select * From NhaCungCap";
            tblNhacungcap = Functions.GetDataToTable(sql);
            dataGridView_NuocSX.DataSource = tblNhacungcap;
        }
        private void Resetvalue()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
        }

        private void dataGridView_NuocSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNCC.Text = dataGridView_NuocSX.CurrentRow.Cells["MaNhaCC"].Value.ToString();
            txtTenNCC.Text = dataGridView_NuocSX.CurrentRow.Cells["TenNhaCC"].Value.ToString();
            txtDiaChi.Text = dataGridView_NuocSX.CurrentRow.Cells["DiaChi"].Value.ToString();
            txtSoDienThoai.Text = dataGridView_NuocSX.CurrentRow.Cells["DienThoai"].Value.ToString();
            txtMaNCC.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            Resetvalue();
            txtMaNCC.Enabled = true;
            txtMaNCC.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNhacungcap.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu! ");
            }
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã NCC");
                txtMaNCC.Focus();
            }
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NCC");
                txtTenNCC.Focus();
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập địa chỉ");
                txtTenNCC.Focus();
            }
            if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập số điên thoại");
                txtTenNCC.Focus();
            }
            sql = "UPDATE NhaCungCap SET  TenNhaCC=N'" + txtTenNCC.Text.Trim().ToString() +
                    "',DiaChi=N'" + txtDiaChi.Text.Trim().ToString() +
                    "',DienThoai='" + txtSoDienThoai.Text.ToString() +
                    "' WHERE MaNhaCC=N'" + txtMaNCC.Text + "'";
            Functions.RunSqlDel(sql);
            loadDataToGridview();
            Resetvalue();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
                string sql;
                if (tblNhacungcap.Rows.Count == 0)
                {
                    MessageBox.Show("khong co du lieu");
                }
                if (txtMaNCC.Text == "")
                {
                MessageBox.Show("ban chua chon Nhà cung cấp");
                }
                else
                {
                if (MessageBox.Show("Bạn muốn xóa không", "Thông báo",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sql = "delete from NhaCungCap where MaNhaCC ='" + txtMaNCC.Text + "'";
                    Functions.RunSQL(sql);
                    loadDataToGridview();
                    Resetvalue();
                }
                }
            }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblNhacungcap.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!");
                return;
            }
            if (txtMaNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập mã NSX");
                txtMaNCC.Focus();
            }
            if (txtTenNCC.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NSX");
                txtTenNCC.Focus();
            }
            if (txtDiaChi.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NSX");
                txtDiaChi.Focus();
            }
            if (txtSoDienThoai.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên NSX");
                txtSoDienThoai.Focus();
            }
            sql = "Select MaNhaCC from NhaCungCap where MaNhaCC ='" + txtMaNCC.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã NSX này đã có bạn phải nhập mã khác");
                txtMaNCC.Focus();
                return;
            }
            sql = "insert into NhaCungCap values( '" + txtMaNCC.Text + "' ,'" + txtTenNCC.Text + "','" + txtDiaChi.Text + "' ,'" + txtSoDienThoai.Text + "')";
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
            txtMaNCC.Enabled = false;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("bạn có chắc chắn muốn thoát chương trình không", "Hỏi Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                this.Close();
        }
    }
        
}
