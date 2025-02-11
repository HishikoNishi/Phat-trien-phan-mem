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

namespace _1
{
    public partial class Form1 : Form
    {
        string connectionString = @"Data Source=THANHTAM-KUTEHO;Initial Catalog=SOF102;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
          
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void Form1_Load(object sender, EventArgs e)
        {

            hienThiDuLieuTrenLuoi();
        }

        private void txtTenSanPham_TextChanged(object sender, EventArgs e)
        {

        }
        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Kiểm tra nếu ô được nhấn không phải là tiêu đề của cột
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView3.Rows[e.RowIndex];

                // Hiển thị dữ liệu từ hàng được chọn lên các TextBox
                txtMaSanPham.Text = row.Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = row.Cells["TenSanPham"].Value.ToString();
                txtLoaiSanPham.Text = row.Cells["LoaiSanPham"].Value.ToString();
                txtGia.Text = row.Cells["Gia"].Value.ToString();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string MaSanPham = txtMaSanPham.Text.Trim();
            string TenSanPham = txtTenSanPham.Text;
            string LoaiSanPham = txtLoaiSanPham.Text.Trim();
            decimal Gia;

            if (string.IsNullOrEmpty(LoaiSanPham))
            {
                MessageBox.Show("Vui lòng nhập loại sản phẩm.");
                return;
            }

            if (!decimal.TryParse(txtGia.Text, out Gia))
            {
                MessageBox.Show("Giá bán phải là một số thập phân.");
                return;
            }

            string ChuoiKetNoi = @"Data Source=THANHTAM-KUTEHO;Initial Catalog=SOF102;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(ChuoiKetNoi))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE SanPham SET TenSanPham = @TenSanPham, LoaiSanPham = @LoaiSanPham, Gia = @Gia WHERE MaSanPham = @MaSanPham";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@MaSanPham", MaSanPham);
                    cmd.Parameters.AddWithValue("@TenSanPham", TenSanPham);
                    cmd.Parameters.AddWithValue("@LoaiSanPham", LoaiSanPham);
                    cmd.Parameters.AddWithValue("@Gia", Gia);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Sửa thông tin thành công.");
                        hienThiDuLieuTrenLuoi();
                    }
                    else
                    {
                        MessageBox.Show("Sửa thông tin không thành công.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                }
            }
        }
        private void hienThiDuLieuTrenLuoi()
        {
            string chuoiKetNoi = @"Data Source=THANHTAM-KUTEHO;Initial Catalog=SOF102;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(chuoiKetNoi)) // tạo kết nối
            {
                con.Open(); // mở kết nối
                string strSQL = "SELECT * FROM SanPham";
                using (SqlDataAdapter sda = new SqlDataAdapter(strSQL, con))
                {
                    DataTable dt = new DataTable();
                    sda.Fill(dt); // đọc dữ liệu từ SQL Server về máy trạm
                    dataGridView3.DataSource = dt; // hiển thị
                }
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            string maSPText = txtMaSanPham.Text.Trim();
            string tenSanPham = txtTenSanPham.Text.Trim();
            string loaiSanPham = txtLoaiSanPham.Text.Trim();
            decimal gia;

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrEmpty(maSPText))
            {
                MessageBox.Show("Mã sản phẩm không được để trống.");
                return;
            }

            if (string.IsNullOrEmpty(tenSanPham) || string.IsNullOrEmpty(loaiSanPham))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm.");
                return;
            }

            if (!decimal.TryParse(txtGia.Text.Trim(), out gia) || gia <= 0)
            {
                MessageBox.Show("Giá bán phải là một số dương.");
                return;
            }

            // Chuỗi kết nối SQL Server
            string chuoiKetNoi = @"Data Source=THANHTAM-KUTEHO;Initial Catalog=SOF102;Integrated Security=True";

            try
            {
                using (SqlConnection connection = new SqlConnection(chuoiKetNoi))
                {
                    connection.Open();

                    // Nếu MaSanPham không tự động tăng, thêm MaSanPham vào INSERT
                    string query = "INSERT INTO SanPham (MaSanPham, TenSanPham, LoaiSanPham, Gia) VALUES (@MaSanPham, @TenSanPham, @LoaiSanPham, @Gia)";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@MaSanPham", maSPText);
                        cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                        cmd.Parameters.AddWithValue("@LoaiSanPham", loaiSanPham);
                        cmd.Parameters.AddWithValue("@Gia", gia);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Thêm sản phẩm mới thành công.");
                            hienThiDuLieuTrenLuoi(); // Cập nhật lại danh sách sản phẩm
                            ResetForm(); // Xóa dữ liệu trong ô nhập sau khi thêm thành công
                        }
                        else
                        {
                            MessageBox.Show("Thêm sản phẩm không thành công.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void ResetForm()
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtLoaiSanPham.Clear();
            txtGia.Clear();
            txtMaSanPham.Focus(); // Đưa con trỏ về ô nhập mã sản phẩm
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtMaSanPham.Clear();
            txtTenSanPham.Clear();
            txtLoaiSanPham.Clear();
            txtGia.Clear();
            txtMaSanPham.Focus();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMaSanPham.Text))
            {
                MessageBox.Show("Vui lòng nhập Mã Sản Phẩm để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string maSanPham = txtMaSanPham.Text.Trim();


            DialogResult result = MessageBox.Show($"Bạn có chắc chắn muốn xóa sản phẩm có mã: {maSanPham}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM SanPham WHERE MaSanPham = @MaSanPham";

                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@MaSanPham", maSanPham);

                            int rowsAffected = cmd.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Xóa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtMaSanPham.Clear();
                                txtTenSanPham.Clear();
                                txtGia.Clear();
                                // Cập nhật lại danh sách sản phẩm nếu có DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Không tìm thấy sản phẩm để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }


            hienThiDuLieuTrenLuoi();
        }


private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim(); // Lấy từ khóa tìm kiếm từ TextBox

            if (string.IsNullOrWhiteSpace(keyword))
            {
                hienThiDuLieuTrenLuoi(); // Nếu không nhập gì thì hiển thị toàn bộ danh sách
                return;
            }

            try
            {
                string chuoiKetNoi = @"Data Source=THANHTAM-KUTEHO;Initial Catalog=SOF102;Integrated Security=True";
                using (SqlConnection con = new SqlConnection(chuoiKetNoi))
                {
                    con.Open();
                    string query = "SELECT * FROM SanPham WHERE TenSanPham LIKE @Keyword";

                    using (SqlDataAdapter sda = new SqlDataAdapter(query, con))
                    {
                        sda.SelectCommand.Parameters.AddWithValue("@Keyword", "%" + keyword + "%");

                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        dataGridView3.DataSource = dt; // Hiển thị kết quả tìm kiếm lên DataGridView
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát chương trình này không", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            hienThiDuLieuTrenLuoi();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        
        }
    }
}
