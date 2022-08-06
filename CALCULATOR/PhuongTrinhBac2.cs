using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATOR
{
    class PhuongTrinhBac2
    {
        public KetQua ketqua;
        public double a, b, c;
        public SoPhuc x1, x2;
        public enum KetQua
        {
            NghiemKep, HaiNghiem
        }
        public void GiaiNghiem()
        {
            if (a != 0)
            {
                double Delta = b * b - 4 * a * c;
                if (Delta == 0)
                {
                    ketqua = KetQua.NghiemKep;
                    x1 = new SoPhuc(-b / (2 * a), 0);
                    x2 = new SoPhuc(-b / (2 * a), 0);
                }
                else
                {
                    ketqua = KetQua.HaiNghiem;
                    SoPhuc CanDelta = new SoPhuc();
                    if (Delta < 0)
                        CanDelta = new SoPhuc(0, Math.Sqrt(-Delta));
                    else
                        CanDelta = new SoPhuc(Math.Sqrt(Delta), 0);
                    x1 = new SoPhuc((-b + CanDelta) / (2 * a));
                    x2 = new SoPhuc((-b - CanDelta) / (2 * a));
                }
            }
        }

    }
}
