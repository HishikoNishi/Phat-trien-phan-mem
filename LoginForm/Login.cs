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

namespace LoginForm
{
    public partial class Login : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=SOF102;Integrated Security=True";

        public Login()
        {
            InitializeComponent();
            this.BackColor = System.Drawing.Color.FromArgb(37, 37, 38);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT MatKhau FROM TaiKhoanNguoiDung WHERE TenDangNhap = @username";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@username", username);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string dbPassword = reader["MatKhau"].ToString();
                    if (password == dbPassword) // Cần triển khai băm mật khẩu để bảo mật
                    {
                        MessageBox.Show("Đăng nhập thành công");
                        // Chuyển hướng đến form khác hoặc thực hiện các hành động khác
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không đúng");
                    }
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập không tồn tại");
                }
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Hành động khi bấm nút Hủy
            this.Close(); // Đóng form hiện tại
        }

    }
}
