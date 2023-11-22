using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyThuVien
{
    public partial class QuanLySach : Form
    {
        //khai bao
        //string dauvao;

        public QuanLySach()
        {
            InitializeComponent();
        }

        private void load_data()
        {
            // goi phuong thuc ket noi toi database , khai bao doi tuong ket noi ( sever + datebase + cho phep truy cap bao mat)
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            // dung de doc du lieu
            SqlDataAdapter da = new SqlDataAdapter("select * from Sach",con);
            DataTable dt = new DataTable();
            // mo ket noi 
            con.Open();
            // fill la phuong thuc dung de hien thi du lieu duoc lay tu cau lenh select
            da.Fill(dt);
            // datasource la thuoc tinh de lien ket voi dtgv
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void load_text()
        {
            txtMaSach.Text = "";
            txtTenSach.Text = "";
            txtDonGia.Text = "";
            txtSoLuong.Text = "";
        }
        // bat loi
        public bool ktNhap()
        {
            bool ok = false;
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            // la phuong thuc dung de thuc hien cac cau lenh truy van 
            SqlCommand cmd = new SqlCommand("select Masach from Sach",con);
          //  SqlDataReader  = cmd.ExecuteReader();

            return ok;
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            /* SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
             SqlCommand cmd = new SqlCommand("insert into Sach values('" + txtMaSach.Text + "','" + txtTenSach.Text + "','" + Convert.ToDouble(txtDonGia.Text) + "','" + Convert.ToInt16(txtSoLuong.Text) + "')", con) ;
             con.Open();
             int ret =  cmd.ExecuteNonQuery();
             if (ret == 1)
             {
                 MessageBox.Show("Thêm thành công");
             }
            */
            

            try
            {
                SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
                SqlCommand cmd = new SqlCommand("insert into Sach values('" + txtMaSach.Text + "','" + txtTenSach.Text + "','" + Convert.ToDouble(txtDonGia.Text) + "','" + Convert.ToInt16(txtSoLuong.Text) + "')", con);
                con.Open();
                // Ex la kieu truy van khong tra ve du lieu , de insert hoac delete
                int ret = cmd.ExecuteNonQuery();
                if (ret == 1)
                {
                    MessageBox.Show("Thêm thành công");
                }
                con.Close();
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(Exception))
                {
                    if(ex.Message.Contains("PRIMARY KEY"))
                    {
                        MessageBox.Show("Mã sách bị trùng mời bạn nhập lại");
                    }
                    else
                    
                        throw ex;
                    
                }
                MessageBox.Show("Mã sách bị trùng mời bạn nhập lại");
            }

            //con.Close();
            load_data();
            load_text();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlCommand cmd = new SqlCommand("update Sach set Tensach='" + txtTenSach.Text + "',Dongia='" + Convert.ToDouble(txtDonGia.Text) + "', Soluong = '" + Convert.ToInt16(txtSoLuong.Text) + "' where Masach = '" + txtMaSach.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
            {
                MessageBox.Show("Cập nhật thành công");
            }
            con.Close();


            load_data();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlCommand cmd = new SqlCommand("delete from Sach where Masach='" + txtMaSach.Text + "'", con);
            con.Open();
            int ret = cmd.ExecuteNonQuery();
            if (ret == 1)
            {
                MessageBox.Show("Xóa thành công");
            }
            con.Close();
            load_data();
        }

        private void QuanLySach_Load(object sender, EventArgs e)
        {
            load_data();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
            SqlDataAdapter da = new SqlDataAdapter("select * from Sach where Tensach LIKE '%"+txtTimKiem.Text+"%'", con);
            DataTable dt = new DataTable();
            con.Open();
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //dataGridView là đối tượng DataGridView mà bạn muốn lấy giá trị của ô.
            //e.RowIndex là chỉ số của hàng mà bạn muốn lấy giá trị của ô.
            //Cells[0] là chỉ số của cột mà bạn muốn lấy giá trị của ô.
            //Value là thuộc tính trả về giá trị của ô.
            //ToString() là phương thức chuyển đổi giá trị của ô thành chuỗi.
            txtMaSach.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSach.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDonGia.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtSoLuong.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        // BAT LOI KHONG NHAP MA SACH
        private void txtMaSach_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaSach.Text))
            {
                e.Cancel = true;
                txtMaSach.Focus();
                errorProvider1.SetError(txtMaSach, "Hãy nhập mã sách");
            } else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtMaSach, null);
            }
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtMaSach_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtDonGia_Validating(object sender, CancelEventArgs e)
        {
           
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar)&&!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chi nhap so");
            }
            
        }

        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsControl(e.KeyChar) && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Chi nhap so");
            }
        }
    }
}
