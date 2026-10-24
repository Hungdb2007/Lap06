using System;
using System.Windows.Forms;

namespace CSLab07_ADO
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
        }

        private void mnuDangNhap_Click(object sender, EventArgs e)
        {
            frmLogin f = new frmLogin();
            f.MdiParent = this;
            f.Show();
        }

        private void mnuThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void mnuQuanLyLop_Click(object sender, EventArgs e)
        {
            frmLop f = new frmLop();
            f.MdiParent = this;
            f.Show();
        }

        private void mnuHoSoHocSinh_Click(object sender, EventArgs e)
        {
            frmHocSinh f = new frmHocSinh();
            f.MdiParent = this;
            f.Show();
        }

        private void mnuCascade_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.Cascade);
        }

        private void mnuTileVertical_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void mnuTileHorizontal_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }
    }
}