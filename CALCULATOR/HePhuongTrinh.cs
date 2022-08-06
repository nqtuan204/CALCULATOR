using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CALCULATOR
{
    class HePhuongTrinh
    {
        public double[,] a;
        // m là số phương trình n là số ẩn
        public int m, n;
        public KetQua ketqua;

        public enum KetQua
        {
            NghiemDuyNhat, VoNghiem, VoSoNghiem
        }

        public HePhuongTrinh(int m, int n)
        {
            this.m = m;
            this.n = n;
            a = new double[m + 1, n + 2];
        }

        public void doihang(int i1, int i2)     // đổi vị trí 2 hàng
        {
            for (int j = 1; j <= n + 1; j++)
            {
                a[i1, j] += a[i2, j];
                a[i2, j] = a[i1, j] - a[i2, j];
                a[i1, j] = a[i1, j] - a[i2, j];
            }
        }

        public void tru2hang(int row, int i, int j)   // trừ hàng row cho hàng i nhân với a[i,j]
        {
            double thuong = a[row, j] / a[i, j];
            while (j <= n + 1)
            {
                a[row, j] = a[row, j] - thuong * a[i, j];
                j++;
            }
        }

        public void chonamax(int i, int j)  // chọn a[i,j] có trị tuyệt đối lớn nhất
        {
            double max = Math.Abs(a[i, j]); int IndexOfMax = i;
            for (int row = i + 1; row <= m; row++)
                if (Math.Abs(a[row, j]) > max)
                {
                    max = Math.Abs(a[row, j]);
                    IndexOfMax = row;
                }
            if (IndexOfMax != i)
                doihang(i, IndexOfMax);
        }

        public void chiahang(int i, int j)   // chia hàng i cho a[i,j]
        {
            for (int k = n + 1; k >= j; k--)
                a[i, k] = a[i, k] / a[i, j];
        }

        public void khux(int i, int j)   // khử xj của các hàng dưới i
        {
            if (a[i, j] != 1)
                chiahang(i, j);
            for (int row = i + 1; row <= m; row++)
                if (a[row, j] != 0)
                    tru2hang(row, i, j);
        }

        public void KetLuanNghiem()
        {
            if (rank(n) < rank(n + 1))
                ketqua = KetQua.VoNghiem;
            else if (rank(n) == n)
            {
                ketqua = KetQua.NghiemDuyNhat;
                if (a[n, n] != 1)
                    chiahang(n, n);
                nghiemduynhat(n, n);
            }

            else
                ketqua = KetQua.VoSoNghiem;
        }

        public void nghiemduynhat(int i, int j)
        {
            while (i > 1)
            {
                for (int row = i - 1; row >= 1; row--)
                    tru2hang(row, i, i);
                i--;
            }
        }

        public int rank(int col)   //tính hạng ma trận
        {
            int rank = 0;
            for (int i = 1; i <= m; i++)
            {
                int j = 0;
                do
                {
                    j++;
                    if (a[i, j] != 0)
                        rank++;
                } while (a[i, j] == 0 && j < col);
            }
            return rank;
        }

        public void GaussJordan()       //khử các phần tử nằm dưới aij
        {
            int i = 1, j = 1;
            while (i < m && j <= n + 1)
            {
                chonamax(i, j);
                if (a[i, j] != 0)
                    khux(i, j);
                else
                {
                    j++;
                    continue;
                }
                i++; j++;
            }
            KetLuanNghiem();
        }
    }
}

