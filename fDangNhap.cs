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
    public partial class fDangNhap : Form
    {
        public fDangNhap()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {

            Form1 f1 = new Form1();

            try
            {
                SqlConnection con = new SqlConnection("server = DESKTOP-N38OH8L\\SQLEXPRESS; database = SachDB;integrated security = true ");
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "dbo.spDangNhap";
                cmd.Parameters.AddWithValue("@tendangnhap",txtTendangnhap.Text);
                cmd.Parameters.AddWithValue("@matkhau", txtMatkhau.Text);
                cmd.Connection = con;
                object kq = cmd.ExecuteScalar();
                int code = Convert.ToInt32(kq);
                if(code == 0)
                {
                    
                    f1.ShowDialog(); 

                } else if (code == 1)
                {
                    MessageBox.Show("Ten tai khoan hoac mat khau khong chinh xac");
                    txtTendangnhap.Text = "";
                    txtMatkhau.Text = "";

                } else if (code == 2)
                {
                    MessageBox.Show("Dang nhap khong thanh cong");
                    txtTendangnhap.Text = "";
                    txtMatkhau.Text = "";
                }

                con.Close();
            } catch (Exception ex )
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
