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

namespace Thống_kê_báo_cáo
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=NHATLAM;Initial Catalog=SOF102;Integrated Security=True";
        private SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connection = new SqlConnection(connectionString);
     
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            string query = "SELECT MaBaoCao, NgayBaoCao, DoanhThu, LoaiBaoCao FROM BaoCao WHERE NgayBaoCao >= DATEADD(MONTH, -1, GETDATE())";
            LoadDataToGridView(query);
        }
        private void LoadDataToGridView(string query)
        {
            try
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                connection.Close();

                dgvThongKe.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        private void btnFIller_Click(object sender, EventArgs e)
        {
            string filter = cmbTimeFilter.SelectedItem.ToString();
            string query = "SELECT MaBaoCao, NgayBaoCao, DoanhThu, LoaiBaoCao FROM BaoCao WHERE 1=1";


            if (filter == "Hôm nay")
                query += " AND CAST(NgayBaoCao AS DATE) = CAST(GETDATE() AS DATE)";
            else if (filter == "Tuần này")
                query += " AND NgayBaoCao >= DATEADD(DAY, -7, GETDATE())";
            else if (filter == "Tháng này")
                query += " AND NgayBaoCao >= DATEADD(MONTH, -1, GETDATE())";

            LoadDataToGridView(query);
        }
    }
}
