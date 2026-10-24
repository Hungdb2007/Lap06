using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CSLab07_ADO
{
    public partial class frmHocSinh : Form
    {
        private string chuoiketnoi = "Server=.; Database=DBHocSinh; Integrated Security=SSPI;";

        public frmHocSinh()
        {
            InitializeComponent();
        }

        private void frmHocSinh_Load(object sender, EventArgs e)
        {
            loadDanhSachHocSinh();
        }

        void loadDanhSachHocSinh()
        {
            lvHocSinh.Items.Clear();
            SqlConnection con = new SqlConnection(chuoiketnoi);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM HocSinh", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string mahs = dr.GetString(0);
                string tenhs = dr.GetString(1);
                DateTime ngaysinh = dr.GetDateTime(2);
                string diachi = dr.GetString(3);
                float dtb = dr.GetFloat(4);
                string malop = dr.GetString(5);

                ListViewItem lvi = new ListViewItem();
                lvi.Tag = mahs;
                lvi.Text = mahs;
                lvi.SubItems.Add(tenhs);
                lvi.SubItems.Add(ngaysinh.ToString("dd/MM/yyyy"));
                lvi.SubItems.Add(diachi);
                lvi.SubItems.Add(dtb.ToString());
                lvi.SubItems.Add(malop);
                lvHocSinh.Items.Add(lvi);
            }
            con.Close();
        }

        private void lvHocSinh_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                txtMaHS.Text = e.Item.Text;
                txtTenHS.Text = e.Item.SubItems[1].Text;
                dtpNgaySinh.Value = DateTime.Parse(e.Item.SubItems[2].Text);
                txtDiaChi.Text = e.Item.SubItems[3].Text;
                txtDTB.Text = e.Item.SubItems[4].Text;
                txtMaLopHS.Text = e.Item.SubItems[5].Text;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(chuoiketnoi);
            con.Open();
            string sql = "INSERT INTO HocSinh (MaHS, TenHS, NgaySinh, DiaChi, DTB, MaLop) VALUES (@MaHS, @TenHS, @NgaySinh, @DiaChi, @DTB, @MaLop)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add(new SqlParameter("@MaHS", txtMaHS.Text));
            cmd.Parameters.Add(new SqlParameter("@TenHS", txtTenHS.Text));
            cmd.Parameters.Add(new SqlParameter("@NgaySinh", dtpNgaySinh.Value));
            cmd.Parameters.Add(new SqlParameter("@DiaChi", txtDiaChi.Text));
            cmd.Parameters.Add(new SqlParameter("@DTB", float.Parse(txtDTB.Text)));
            cmd.Parameters.Add(new SqlParameter("@MaLop", txtMaLopHS.Text));

            cmd.ExecuteNonQuery();
            con.Close();
            loadDanhSachHocSinh();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvHocSinh.SelectedItems.Count > 0)
            {
                string mahs = lvHocSinh.SelectedItems[0].Tag.ToString();
                SqlConnection con = new SqlConnection(chuoiketnoi);
                con.Open();
                string sql = "UPDATE HocSinh SET TenHS = @TenHS, NgaySinh = @NgaySinh, DiaChi = @DiaChi, DTB = @DTB, MaLop = @MaLop WHERE MaHS = @MaHS";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.Add(new SqlParameter("@MaHS", mahs));
                cmd.Parameters.Add(new SqlParameter("@TenHS", txtTenHS.Text));
                cmd.Parameters.Add(new SqlParameter("@NgaySinh", dtpNgaySinh.Value));
                cmd.Parameters.Add(new SqlParameter("@DiaChi", txtDiaChi.Text));
                cmd.Parameters.Add(new SqlParameter("@DTB", float.Parse(txtDTB.Text)));
                cmd.Parameters.Add(new SqlParameter("@MaLop", txtMaLopHS.Text));

                cmd.ExecuteNonQuery();
                con.Close();
                loadDanhSachHocSinh();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvHocSinh.SelectedItems.Count > 0)
            {
                string mahs = lvHocSinh.SelectedItems[0].Tag.ToString();
                SqlConnection con = new SqlConnection(chuoiketnoi);
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM HocSinh WHERE MaHS = @MaHS", con);
                cmd.Parameters.Add(new SqlParameter("@MaHS", mahs));
                cmd.ExecuteNonQuery();
                con.Close();
                loadDanhSachHocSinh();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}