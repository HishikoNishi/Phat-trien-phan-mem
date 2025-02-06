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

namespace quanlytaikhoan
{

    public partial class QuanLyTaiKhoan : Form
    {

        private string connectionString = "Data Source=NHATLAM;Initial Catalog=SOF102;Integrated Security=True";
        private SqlConnection connection;
        public QuanLyTaiKhoan()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM TaiKhoanNguoiDung"; // Thay Users bằng tên bảng của bạn
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridView1.DataSource = dt;
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối: " + ex.Message);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO TaiKhoanNguoiDung (MaNguoiDung, TenDangNhap,MatKhau,VaiTro) VALUES (@MaNguoiDung, @TenDangNhap,@MatKhau, @VaiTro)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaNguoiDung", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@MatKhau",txtMatKhau.Text);
                cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Thêm tài khoản thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "DELETE FROM TaiKhoanNguoiDung WHERE MaNguoiDung = @MaNguoiDung";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaNguoiDung", txtMaSP.Text);
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Xóa tài khoản thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtMaSP.Text = row.Cells["MaNguoiDung"].Value.ToString();
                txtTenSP.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtTenSP.Text = row.Cells["MatKhau"].Value.ToString();
                cmbVaiTro.SelectedItem = row.Cells["VaiTro"].Value.ToString();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "UPDATE TaiKhoanNguoiDung SET TenDangNhap = @TenDangNhap, VaiTro = @VaiTro, MatKhau = @MatKhau WHERE MaNguoiDung = @MaNguoiDung";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@MaNguoiDung", txtMaSP.Text);
                cmd.Parameters.AddWithValue("@TenDangNhap", txtTenSP.Text);
                cmd.Parameters.AddWithValue("@MatKhau", txtMatKhau.Text);
                cmd.Parameters.AddWithValue("@VaiTro", cmbVaiTro.SelectedItem.ToString());
                cmd.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Cập nhật tài khoản thành công!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
