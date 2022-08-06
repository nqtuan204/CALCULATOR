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
    public partial class PTB3 : Form
    {
        public PTB3()
        {
            InitializeComponent();
        }

        internal bool NhapChinhXac(PhuongTrinhBac3 ptb3, BieuThucToan bt)
        {
            txtbx1.Clear();
            txtbx2.Clear();
            txtbx3.Clear();
            try
            {
                bt.form = txtba.Text;
                ptb3.a = Double.Parse(bt.GiaTri());
                if (ptb3.a == 0)
                {
                    MessageBox.Show("Hệ số a phải khác 0!");
                    return false;
                }
                bt.form = txtbb.Text;
                ptb3.b = Double.Parse(bt.GiaTri());
                bt.form = txtbc.Text;
                ptb3.c = Double.Parse(bt.GiaTri());
                bt.form = txtbd.Text;
                ptb3.d = Double.Parse(bt.GiaTri());
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
            PhuongTrinhBac3 ptb3 = new PhuongTrinhBac3();
            BieuThucToan bt = new BieuThucToan();
            if (NhapChinhXac(ptb3, bt))
            {
                ptb3.Giai();
                if (ptb3.ketqua == PhuongTrinhBac3.KetQua.NghiemDuyNhat)
                    txtbx1.Text = ptb3.x1.ToString();
                else
                {
                    txtbx1.Text = ptb3.x1.ToString();
                    txtbx2.Text = ptb3.x2.ToString();
                    txtbx3.Text = ptb3.x3.ToString();
                }
            }

        }
    }
}
