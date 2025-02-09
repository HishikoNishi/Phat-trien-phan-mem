using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyTaiKhoan
{
    public partial class ChiTietHoaDonfrm : Form
    {
        public string maHoaDon;
        public SqlConnection conn = new SqlConnection("Data Source=MSI;Initial Catalog=SOF102;Integrated Security=True");
        public ChiTietHoaDonfrm(string maHoaDon)
        {
            InitializeComponent();
            this.maHoaDon = maHoaDon;
            LoadChiTietHoaDon();
        }
        private void LoadChiTietHoaDon()
        {
            try
            {
                conn.Open();
                string query = @"SELECT h.MaHoaDon, h.NgayLap, h.SoBan, h.TrangThai, 
                       c.MaChiTietHD, s.TenSanPham, c.SoLuong, s.GiaBan
                FROM HoaDon h
                LEFT JOIN ChiTietHoaDon c ON h.MaHoaDon = c.MaHoaDon
                LEFT JOIN SanPham s ON c.MaSanPham = s.MaSanPham
                WHERE h.MaHoaDon = @MaHoaDon";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaHoaDon", maHoaDon);
                SqlDataAdapter Da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                Da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    txtMaHoaDon.Text = dt.Rows[0]["MaHoaDon"].ToString();
                    dtpNgaylap.Value = Convert.ToDateTime(dt.Rows[0]["NgayLap"]);
                    txtSoBan.Text = dt.Rows[0]["SoBan"].ToString();
                    cmbTrangThai.SelectedItem = dt.Rows[0]["TrangThai"].ToString();

                    DgvChiTietHoaDon.DataSource = dt;
                    TinhTongTien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message);
            }
            finally 
            {
                conn.Close();
            }

        }
        private void TinhTongTien()
        {
            decimal TongTien = 0;
            foreach (DataGridViewRow row in DgvChiTietHoaDon.Rows)
            {
                if (row.Cells["SoLuong"].Value != null && row.Cells["GiaBan"].Value != null)
                {
                    int SoLuong = Convert.ToInt32(row.Cells["SoLuong"].Value);
                    decimal GiaBan = Convert.ToDecimal(row.Cells["GiaBan"].Value);
                    TongTien += SoLuong * GiaBan;
                }
            }
            lblTongTien.Text = $"Tổng: {TongTien:N0} VND";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void ChiTietHoaDonfrm_Load(object sender, EventArgs e)
        {

        }
    }
}
