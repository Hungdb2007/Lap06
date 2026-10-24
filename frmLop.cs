using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CSLab07_ADO
{
    public partial class frmLop : Form
    {
        private string chuoiketnoi = "Server=.; Database=DBHocSinh; Integrated Security=SSPI;";

        public frmLop()
        {
            InitializeComponent();
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            loadDanhSach();
            cboListType.SelectedIndex = 1;
        }

        void loadDanhSach()
        {
            lvLop.Items.Clear();
            SqlConnection con = new SqlConnection(chuoiketnoi);
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Lop", con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                string malop = dr.GetString(0);
                string tenlop = dr.GetString(1);
                int siso = dr.GetInt16(2);

                ListViewItem lvi = new ListViewItem();
                lvi.Tag = malop;
                lvi.Text = malop;
                lvi.SubItems.Add(tenlop);
                lvi.SubItems.Add(siso.ToString());
                lvLop.Items.Add(lvi);
            }
            con.Close();
        }

        private void cboListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvLop.View = (View)cboListType.SelectedIndex;
        }

        private void lvLop_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (e.IsSelected)
            {
                txtMaLop.Text = e.Item.Text;
                txtTenLop.Text = e.Item.SubItems[1].Text;
                txtSiSo.Text = e.Item.SubItems[2].Text;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLop = txtMaLop.Text;
            string tenLop = txtTenLop.Text;
            int siso = int.Parse(txtSiSo.Text);

            SqlConnection con = new SqlConnection(chuoiketnoi);
            con.Open();
            string sql = "INSERT INTO Lop (MaLop, TenLop, SiSo) VALUES (@MaLop, @TenLop, @SiSo)";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.Parameters.Add(new SqlParameter("@MaLop", maLop));
            cmd.Parameters.Add(new SqlParameter("@TenLop", tenLop));
            cmd.Parameters.Add(new SqlParameter("@SiSo", siso));

            cmd.ExecuteNonQuery();
            con.Close();
            loadDanhSach();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvLop.SelectedItems.Count > 0)
            {
                string malop = lvLop.SelectedItems[0].Tag.ToString();
                string tenLop = txtTenLop.Text;
                int siso = int.Parse(txtSiSo.Text);

                SqlConnection con = new SqlConnection(chuoiketnoi);
                con.Open();
                string sql = "UPDATE Lop SET TenLop = @TenLop, SiSo = @SiSo WHERE MaLop = @MaLop";
                SqlCommand cmd = new SqlCommand(sql, con);

                cmd.Parameters.Add(new SqlParameter("@MaLop", malop));
                cmd.Parameters.Add(new SqlParameter("@TenLop", tenLop));
                cmd.Parameters.Add(new SqlParameter("@SiSo", siso));

                cmd.ExecuteNonQuery();
                con.Close();
                loadDanhSach();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvLop.SelectedItems.Count > 0)
            {
                string malop = lvLop.SelectedItems[0].Tag.ToString();
                SqlConnection con = new SqlConnection(chuoiketnoi);
                con.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM Lop WHERE MaLop = @MaLop", con);
                cmd.Parameters.Add(new SqlParameter("@MaLop", malop));
                cmd.ExecuteNonQuery();
                con.Close();
                loadDanhSach();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}