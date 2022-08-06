using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CALCULATOR
{
    class SoPhuc
    {
        public double thuc, ao;

        public SoPhuc()
        {

        }

        public SoPhuc(double thuc, double ao)
        {
            this.thuc = thuc;
            this.ao = ao;
        }

        public SoPhuc(SoPhuc z)
        {
            thuc = z.thuc;
            ao = z.ao;
        }

        public static SoPhuc Parse(string s)
        {
            SoPhuc z = new SoPhuc();
            if (s.IndexOf('i') == -1)
            {
                z.ao = 0;
                z.thuc = Double.Parse(s);
            }
            else
            {
                string pattern = @"\-?(\d+(\.\d+)?)?i|\-?\d+(\.\d+)?";
                MatchCollection MC = Regex.Matches(s, pattern);
                foreach (Match m in MC)
                {
                    if (m.ToString().IndexOf('i') == -1)
                        z.thuc = Double.Parse(m.ToString());
                    else
                    {
                        if (m.ToString() == "i")
                            z.ao = 1;
                        else if (m.ToString() == "-i")
                            z.ao = -1;
                        else
                            z.ao = Double.Parse(m.ToString().Substring(0, m.ToString().Length - 1));
                    }
                }
            }
            return z;

        }

        public override string ToString()
        {
            if (thuc == 0)
            {
                switch (ao)
                {
                    case 0: return "0";
                    case 1: return "i";
                    case -1: return "-i";
                    default: return ao.ToString() + "i";
                }
            }
            else
            {
                switch (ao)
                {
                    case 0: return thuc.ToString();
                    case 1: return thuc.ToString() + "+i";
                    case -1: return thuc.ToString() + "-i";
                    default:
                        {
                            if (ao > 0)
                                return thuc.ToString() + "+" + ao.ToString() + "i";
                            else
                                return thuc.ToString() + ao.ToString() + "i";
                        }
                }
            }
        }

        public SoPhuc LienHop()
        {
            SoPhuc w = new SoPhuc();
            w.thuc = this.thuc;
            w.ao = -this.ao;
            return w;
        }

        public double Modun()
        {
            return Math.Sqrt(Math.Pow(thuc, 2) + Math.Pow(ao, 2));
        }

        public double Argumen()
        {
            return Math.Atan2(ao, thuc);
        }

        public static SoPhuc operator ^(SoPhuc z1, double n)
        {
            SoPhuc z = new SoPhuc(0, 0);
            if (n >= 0 && n - (int)n == 0)
            {
                z.thuc = 1;
                for (int i = 1; i <= n; i++)
                    z *= z1;
                return z;
            }

            double phi = z1.Argumen();
            z.thuc = Math.Round(Math.Cos(n * phi),13);
            z.ao = Math.Round(Math.Sin(n * phi),13);
            z = z * Math.Pow(z1.Modun(), n);
            return z;
        }
        public static SoPhuc operator +(SoPhuc z1, SoPhuc z2)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z1.thuc + z2.thuc;
            w.ao = z1.ao + z2.ao;
            return w;
        }

        public static SoPhuc operator -(SoPhuc z)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = -z.thuc;
            w.ao = -z.ao;
            return w;
        }
        public static SoPhuc operator -(SoPhuc z1, SoPhuc z2)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z1.thuc - z2.thuc;
            w.ao = z1.ao - z2.ao;
            return w;
        }

        public static SoPhuc operator *(SoPhuc z1, SoPhuc z2)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z1.thuc * z2.thuc - z1.ao * z2.ao;
            w.ao = z1.thuc * z2.ao + z1.ao * z2.thuc;
            return w;
        }

        public static SoPhuc operator /(SoPhuc z1, SoPhuc z2)
        {
            SoPhuc w = new SoPhuc();
            if (z2.ao == 0 && z1.ao == 0)
            {
                w.ao = 0;
                w.thuc = z1.thuc / z2.thuc;
            }
            else
            {
                double Mau = z2.thuc * z2.thuc + z2.ao * z2.ao;
                w = z1 * z2.LienHop() / Mau;
            }
            return w;
        }

        public static SoPhuc operator +(double x, SoPhuc z)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc + x;
            w.ao = z.ao;
            return w;
        }

        public static SoPhuc operator +(SoPhuc z, double x)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc + x;
            w.ao = z.ao;
            return w;
        }

        public static SoPhuc operator -(double x, SoPhuc z)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = x - z.thuc;
            w.ao = -z.ao;
            return w;
        }

        public static SoPhuc operator -(SoPhuc z, double x)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc - x;
            w.ao = z.ao;
            return w;
        }

        public static SoPhuc operator *(SoPhuc z, double x)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc * x;
            w.ao = z.ao * x;
            return w;
        }

        public static SoPhuc operator *(double x, SoPhuc z)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc * x;
            w.ao = z.ao * x;
            return w;
        }

        public static SoPhuc operator /(SoPhuc z, double x)
        {
            SoPhuc w = new SoPhuc();
            w.thuc = z.thuc / x;
            w.ao = z.ao / x;
            return w;
        }

        public static SoPhuc operator /(double x, SoPhuc z)
        {
            SoPhuc w = new SoPhuc();
            w = x * z.LienHop() / (z * z.LienHop());
            return w;
        }
    }
}
