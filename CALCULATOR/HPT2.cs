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
    public partial class HPT2 : Form
    {
        public HPT2()
        {
            InitializeComponent();
        }

        internal bool NhapChinhXac(HePhuongTrinh hpt, BieuThucToan bt)
        {
            lbKL.Text = "";
            txtbx1.Clear();
            txtbx2.Clear();
            try
            {
                bt.form = txtba1.Text;
                hpt.a[1, 1] = Double.Parse(bt.GiaTri());
                bt.form = txtbb1.Text;
                hpt.a[1, 2] = Double.Parse(bt.GiaTri());
                bt.form = txtbc1.Text;
                hpt.a[1, 3] = Double.Parse(bt.GiaTri());
                bt.form = txtba2.Text;
                hpt.a[2, 1] = Double.Parse(bt.GiaTri());
                bt.form = txtbb2.Text;
                hpt.a[2, 2] = Double.Parse(bt.GiaTri());
                bt.form = txtbc2.Text;
                hpt.a[2, 3] = Double.Parse(bt.GiaTri());
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
            HePhuongTrinh hpt = new HePhuongTrinh(2, 2);
            BieuThucToan bt = new BieuThucToan();
            if(NhapChinhXac(hpt,bt))
            {
                hpt.GaussJordan();
                if (hpt.ketqua == HePhuongTrinh.KetQua.NghiemDuyNhat)
                {
                    txtbx1.Text = hpt.a[1, 3].ToString();
                    txtbx2.Text = hpt.a[2, 3].ToString();
                }
                else if (hpt.ketqua == HePhuongTrinh.KetQua.VoNghiem)
                    lbKL.Text = "Vô Nghiệm";
                else
                    lbKL.Text = "Vô Số Nghiệm";
            }
            
            
        }
    }
}
