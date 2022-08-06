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
    public partial class PTB2 : Form
    {
        public PTB2()
        {
            InitializeComponent();
        }

        internal bool NhapChinhXac(PhuongTrinhBac2 ptb2, BieuThucToan bt)
        {
            lbKL.Text = "";
            txtbx1.Clear();
            txtbx2.Clear();
            try
            {
                bt.form = txtba.Text;
                ptb2.a = Double.Parse(bt.GiaTri());
                if (ptb2.a == 0)
                {
                    MessageBox.Show("Hệ số a phải khác 0!");
                    return false;
                }
                bt.form = txtbb.Text;
                ptb2.b = Double.Parse(bt.GiaTri());
                bt.form = txtbc.Text;
                ptb2.c = Double.Parse(bt.GiaTri());
            }
            catch
            {
                MessageBox.Show("Lỗi nhập dữ liệu, kiểm tra lại!");
                return false;
            }
            return true;
        }

        private void btKetQua_Click(object sender, EventArgs e)
        {
            PhuongTrinhBac2 ptb2 = new PhuongTrinhBac2();
            BieuThucToan bt = new BieuThucToan();
            if (NhapChinhXac(ptb2, bt))
            {
                ptb2.GiaiNghiem();
                switch (ptb2.ketqua)
                {
                    case PhuongTrinhBac2.KetQua.HaiNghiem:
                        {
                            txtbx1.Text = ptb2.x1.ToString();
                            txtbx2.Text = ptb2.x2.ToString();
                        }
                        break;
                    case PhuongTrinhBac2.KetQua.NghiemKep:
                        {
                            txtbx1.Text = ptb2.x1.ToString();
                            txtbx2.Text = ptb2.x2.ToString();
                        }
                        break;
                }
            }
        }
    }
}
