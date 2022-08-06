using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CALCULATOR
{
    public partial class HPT3 : Form
    {
        public HPT3()
        {
            InitializeComponent();
        }

        internal bool NhapChinhXac(HePhuongTrinh hpt, BieuThucToan bt)
        {
            lbKL.Text = "";
            txtbx1.Clear();
            txtbx2.Clear();
            txtbx3.Clear();
            try
            {
                bt.form = txtba1.Text;
                hpt.a[1, 1] = Double.Parse(bt.GiaTri());
                bt.form = txtbb1.Text;
                hpt.a[1, 2] = Double.Parse(bt.GiaTri());
                bt.form = txtbc1.Text;
                hpt.a[1, 3] = Double.Parse(bt.GiaTri());
                bt.form = txtbd1.Text;
                hpt.a[1, 4] = Double.Parse(bt.GiaTri());
                bt.form = txtba2.Text;
                hpt.a[2, 1] = Double.Parse(bt.GiaTri());
                bt.form = txtbb2.Text;
                hpt.a[2, 2] = Double.Parse(bt.GiaTri());
                bt.form = txtbc2.Text;
                hpt.a[2, 3] = Double.Parse(bt.GiaTri());
                bt.form = txtbd2.Text;
                hpt.a[2, 4] = Double.Parse(bt.GiaTri());
                bt.form = txtba3.Text;
                hpt.a[3, 1] = Double.Parse(bt.GiaTri());
                bt.form = txtbb3.Text;
                hpt.a[3, 2] = Double.Parse(bt.GiaTri());
                bt.form = txtbc3.Text;
                hpt.a[3, 3] = Double.Parse(bt.GiaTri());
                bt.form = txtbd3.Text;
                hpt.a[3, 4] = Double.Parse(bt.GiaTri());
                return true;
            }
            catch
            {
                MessageBox.Show("Lỗi nhập dữ liệu, kiểm tra lại!");
                return false;
            }
        }

        private void btKetQua_Click(object sender, EventArgs e)
        {
            HePhuongTrinh hpt = new HePhuongTrinh(3, 3);
            BieuThucToan bt = new BieuThucToan();
            
            hpt.GaussJordan();
            if (hpt.ketqua == HePhuongTrinh.KetQua.NghiemDuyNhat)
            {
                txtbx1.Text = hpt.a[1, 4].ToString();
                txtbx2.Text = hpt.a[2, 4].ToString();
                txtbx3.Text = hpt.a[3, 4].ToString();
            }
            else if (hpt.ketqua == HePhuongTrinh.KetQua.VoNghiem)
                lbKL.Text = "Vô Nghiệm";
            else
                lbKL.Text = "Vô Số Nghiệm";
        }
    }
}
