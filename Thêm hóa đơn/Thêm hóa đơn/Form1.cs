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

namespace Thêm_hóa_đơn
{
    public partial class Form1 : Form
    {
        private string connectionString = @"Data Source=DESKTOP-BB7K55A;Initial Catalog=SOF102;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
            LoadData();
            dataGridView1.CellClick += dataGridView1_CellClick;
        }

        public void LoadData()
        {
            string query = "SELECT * FROM HoaDon ORDER BY MaHoaDon";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dataGridView1.DataSource = table;
            }
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            string query = "INSERT INTO HoaDon (NgayLap, SoBan, TongTien, TrangThai) VALUES (@NgayLap, @SoBan, @TongTien, @TrangThai)";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NgayLap", dtpNgaylap.Value);
                        command.Parameters.AddWithValue("@SoBan", textSoban.Text);
                        command.Parameters.AddWithValue("@TongTien", decimal.Parse(textTongTien.Text)); // Lấy giá trị từ textTongTien
                        command.Parameters.AddWithValue("@TrangThai", comboTrangthai.Text);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Hóa đơn mới đã được tạo!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            string query = "UPDATE HoaDon SET NgayLap = @NgayLap, SoBan = @SoBan, TongTien = @TongTien, TrangThai = @TrangThai WHERE MaHoaDon = @MaHoaDon";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MaHoaDon", textMahoadon.Text);
                        command.Parameters.AddWithValue("@NgayLap", dtpNgaylap.Value);
                        command.Parameters.AddWithValue("@SoBan", textSoban.Text);
                        command.Parameters.AddWithValue("@TongTien", decimal.Parse(textTongTien.Text)); // Lấy giá trị từ textTongTien
                        command.Parameters.AddWithValue("@TrangThai", comboTrangthai.Text);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Hóa đơn đã được sửa!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            string deleteDetailsQuery = "DELETE FROM ChiTietHoaDon WHERE MaHoaDon = @MaHoaDon";
            string deleteOrderQuery = "DELETE FROM HoaDon WHERE MaHoaDon = @MaHoaDon";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(deleteDetailsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaHoaDon", textMahoadon.Text);
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand(deleteOrderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@MaHoaDon", textMahoadon.Text);
                        command.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Hóa đơn và các chi tiết liên quan đã bị xóa!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            textMahoadon.Text = "";
            dtpNgaylap.Value = DateTime.Now;
            textSoban.Text = "";
            textTongTien.Text = "";
            comboTrangthai.SelectedIndex = -1;
            LoadData();
        }


        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                textMahoadon.Text = row.Cells["MaHoaDon"].Value?.ToString() ?? string.Empty;
                dtpNgaylap.Value = row.Cells["NgayLap"].Value != DBNull.Value ? Convert.ToDateTime(row.Cells["NgayLap"].Value) : DateTime.Now;
                textSoban.Text = row.Cells["SoBan"].Value?.ToString() ?? string.Empty;
                textTongTien.Text = row.Cells["TongTien"].Value?.ToString() ?? string.Empty;
                comboTrangthai.Text = row.Cells["TrangThai"].Value?.ToString() ?? string.Empty;
            }
        }

    }
}
