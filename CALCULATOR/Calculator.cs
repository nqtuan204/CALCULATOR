using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CALCULATOR
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }

        BieuThucToan BieuThuc = new BieuThucToan();

        private void Calculator_Load(object sender, EventArgs e)
        {
            ActiveControl = txtbBieuThuc;
            MaximumSize = Size;
            MinimumSize = Size;
        }

        public void InputText(Button bt)
        {
            if (txtbKetQua.Text != "")
            {
                txtbKetQua.Clear();
                txtbBieuThuc.Clear();
            }
            txtbBieuThuc.Focus();
            if (Regex.IsMatch(bt.Text, @"sin|cos|tan|ln|lg|abs|sqrt|log|\^"))
            {
                txtbBieuThuc.SelectedText = bt.Text + "()";
                txtbBieuThuc.SelectionStart--;
            }
            else
                txtbBieuThuc.SelectedText = bt.Text;
        }

        private void btGetValue_Click(object sender, EventArgs e)
        {
            if (txtbBieuThuc.Text != "")
            {
                BieuThuc.form = txtbBieuThuc.Text;
                string giatri = BieuThuc.GiaTri();
                giatri = giatri.Replace(@"E\+", "*10^");
                giatri = giatri.Replace(@"E-", "*10^-");
                if (giatri == "Syntax Error" || giatri == "Math Error")
                    MessageBox.Show(giatri);
                else
                    txtbKetQua.Text = giatri;
            }
        }

        private void NhapBieuThuc_Click(object sender, EventArgs e)
        {
            InputText(sender as Button);
        }

        private void btLeft_Click(object sender, EventArgs e)
        {
            if (txtbKetQua.Text == "")
            {
                if (txtbBieuThuc.SelectionStart == 0)
                    txtbBieuThuc.SelectionStart = txtbBieuThuc.TextLength;
                else
                    txtbBieuThuc.SelectionStart--;
            }
            else
            {
                txtbKetQua.Text = "";
                txtbBieuThuc.SelectionStart = txtbBieuThuc.Text.Length;
            }
            txtbBieuThuc.Focus();
        }

        private void btRight_Click(object sender, EventArgs e)
        {
            if (txtbKetQua.Text == "")
            {
                if (txtbBieuThuc.SelectionStart == txtbBieuThuc.TextLength)
                    txtbBieuThuc.SelectionStart = 0;
                else
                    txtbBieuThuc.SelectionStart++;
            }
            else
            {
                txtbKetQua.Text = "";
                txtbBieuThuc.SelectionStart = 0;
            }
            txtbBieuThuc.Focus();
        }

        private void btDEL_Click(object sender, EventArgs e)
        {
            if (txtbKetQua.Text == "")
            {
                txtbBieuThuc.Focus();
                if (txtbBieuThuc.Text != "")
                {
                    if (txtbBieuThuc.SelectionStart == 0)
                        txtbBieuThuc.Text = txtbBieuThuc.Text.Remove(txtbBieuThuc.SelectionStart, 1);
                    else
                    {
                        string Sreplace = txtbBieuThuc.Text.Substring(0, txtbBieuThuc.SelectionStart - 1);
                        txtbBieuThuc.Text = Sreplace + txtbBieuThuc.Text.Substring(txtbBieuThuc.SelectionStart);
                        txtbBieuThuc.SelectionStart = Sreplace.Length;
                    }
                }
            }
        }

        private void btAC_Click(object sender, EventArgs e)
        {
            txtbBieuThuc.Focus();
            if (txtbKetQua.Text != "")
                txtbKetQua.Clear();
            if (txtbBieuThuc.Text != "")
                txtbBieuThuc.Clear();
        }

        private void rbtDEG_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtDEG.Checked)
                MODE.Goc = MODE.ModeGoc.DEG;
            else
                MODE.Goc = MODE.ModeGoc.RAD;
        }

        private void menuHPT2_Click(object sender, EventArgs e)
        {
            HPT2 hpt2 = new HPT2();
            hpt2.ShowDialog();
        }

        private void menuHPT3_Click(object sender, EventArgs e)
        {
            HPT3 hpt3 = new HPT3();
            hpt3.ShowDialog();
        }

        private void menuPTB2_Click(object sender, EventArgs e)
        {
            PTB2 ptb2 = new PTB2();
            ptb2.ShowDialog();
        }

        private void menuPTB3_Click(object sender, EventArgs e)
        {
            PTB3 ptb3 = new PTB3();
            ptb3.ShowDialog();
        }

        private void txtbBieuThuc_Enter(object sender, EventArgs e)
        {
            if (txtbKetQua.Text != "")
                txtbKetQua.Clear();
        }

        private void txtbBieuThuc_TextChanged(object sender, EventArgs e)
        {
            if (txtbKetQua.Text != "")
                txtbKetQua.Clear();
        }
    }
}
