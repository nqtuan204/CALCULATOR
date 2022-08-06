using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATOR
{
    class PhuongTrinhBac3
    {
        public KetQua ketqua;
        public double a, b, c, d;
        public SoPhuc x1, x2, x3;

        public enum KetQua
        {
            BaNghiem, NghiemDuyNhat
        }

        public void Giai()
        {
            if (a != 0)
            {
                if (a != 1)
                {
                    d = d / a;
                    c = c / a;
                    b = b / a;
                    a = 1;
                }

                double p = c - b * b / 3;
                double q = d + (2 * Math.Pow(b, 3) - 9 * b * c) / 27;

                if (p == 0 && q == 0)
                {
                    ketqua = KetQua.NghiemDuyNhat;
                    x1 = new SoPhuc(-b / 3, 0);
                }
                else
                {
                    ketqua = KetQua.BaNghiem;
                    double u = Math.Pow(q / 2 + Math.Sqrt(q * q / 4 + p * p * p / 27), 1.0 / 3);
                    if (u < 0)
                        u = Math.Pow(q / 2 - Math.Sqrt(q * q / 4 + p * p * p / 27), 1.0 / 3);
                    x1 = new SoPhuc(p / (3 * u) - u - b / 3, 0);
                    SoPhuc z = new SoPhuc(-0.5, Math.Sqrt(3) / 2);
                    z = u * z;
                    x2 = p / (3 * z) - z - b / 3;
                    z = new SoPhuc(-0.5, -Math.Sqrt(3) / 2);
                    z = u * z;
                    x3 = p / (3 * z) - z - b / 3;
                }


            }
        }
    }
}
