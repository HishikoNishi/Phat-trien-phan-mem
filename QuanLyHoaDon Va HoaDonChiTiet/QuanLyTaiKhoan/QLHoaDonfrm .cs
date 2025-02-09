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

namespace QuanLyTaiKhoan
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=MSI;Initial Catalog=SOF102;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
            LoadHoaDon();
        }
        private void LoadHoaDon()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "select * from HoaDon";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                DgvHoaDon.DataSource = dt;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO HoaDon (NgayLap, SoBan, TongTien, TrangThai) VALUES (@NgayLap, @SoBan, 0, 'Chờ thanh toán')";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@NgayLap", DateTime.Now);
                cmd.Parameters.AddWithValue("@SoBan", 1); // Giá trị mặc định
                cmd.ExecuteNonQuery();
                LoadHoaDon();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (DgvHoaDon.SelectedRows.Count > 0)
            {
                int maHoaDon = Convert.ToInt32(DgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                string trangThaiMoi = "Đã thanh toán";
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE HoaDon SET TrangThai = @TrangThai WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThaiMoi);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.ExecuteNonQuery();
                    LoadHoaDon();
                }
            }
        }

        private void btnXemChiTiet_Click(object sender, EventArgs e)
        {
            if (DgvHoaDon.SelectedRows.Count > 0)
            {
                string maHoaDon = DgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value.ToString();
                ChiTietHoaDonfrm chiTietform = new ChiTietHoaDonfrm(maHoaDon);
                chiTietform.ShowDialog();
            }
            else 
            {
                MessageBox.Show("Vui lòng chọn một hóa đơn để xem chi tiết!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaHoaDon_Click(object sender, EventArgs e)
        {
            if (DgvHoaDon.SelectedRows.Count > 0)
            {
                int maHoaDon = Convert.ToInt32(DgvHoaDon.SelectedRows[0].Cells["MaHoaDon"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                    cmd.ExecuteNonQuery();
                    LoadHoaDon();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void DgvHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DgvHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
