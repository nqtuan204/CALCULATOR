using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace CALCULATOR
{
    // lớp các phần tử trong biểu thức (gồm số, phép toán +-*/, hàm số,dấu ngoặc,...)
    class PhanTuBieuThuc
    {
        public string form;
        public double MucUuTien = 0;
        public int Ngoi;

        public PhanTuBieuThuc()
        {

        }

        public PhanTuBieuThuc(string Form)
        {
            form = Form;
            if (Regex.IsMatch(Form, @"[!]|sin|cos|tan|ln|log|lg|abs|sqrt"))
            {
                MucUuTien = 3;
                if (form == "log")
                    Ngoi = 2;
                else
                    Ngoi = 1;
            }
            else
            {
                switch (form)
                {
                    case "^":
                        {
                            MucUuTien = 2.75;
                            Ngoi = 2;
                        }
                        break;
                    case "-":
                        {
                            MucUuTien = 2.5;
                            Ngoi = 1;
                        }
                        break;
                    case "+":
                    case "–":
                        {
                            MucUuTien = 1;
                            Ngoi = 2;
                        }
                        break;
                    case "*":
                    case "/":
                        {
                            MucUuTien = 2;
                            Ngoi = 2;
                        }
                        break;
                    case ",":
                        MucUuTien = 0.5; break;
                    default:
                        MucUuTien = 0; break;
                }
            }
        }

        public string MotNgoi(string s)
        {
            {
                SoPhuc x = SoPhuc.Parse(s);
                switch (form)
                {
                    case "-":
                        return (-x).ToString();
                    case "sqrt":
                        {
                            return (x ^ 0.5).ToString();
                        }
                    case "!":
                        {
                            if (x.ao == 0)
                            {
                                double x2 = x.thuc;
                                if (x2 < 0 || x2 - (int)x2 != 0)
                                    return "Math Error";
                                else
                                {
                                    double result = 1;
                                    for (int i = 1; i <= x2; i++)
                                        result *= i;
                                    return result.ToString();
                                }
                            }
                            else
                                return "Math Error";
                        }
                    case "abs":
                        {
                            return x.Modun().ToString();
                        }
                    case "sin":
                        {
                            if (x.ao == 0)
                            {
                                if (MODE.Goc == MODE.ModeGoc.RAD)
                                    return Math.Round(Math.Sin(x.thuc), 14).ToString();
                                else
                                    return Math.Round(Math.Sin(x.thuc * Math.PI / 180), 14).ToString();
                            }
                            else
                                return "Math Error";
                        }
                    case "cos":
                        {
                            if (x.ao == 0)
                            {
                                if (MODE.Goc == MODE.ModeGoc.RAD)
                                    return Math.Round(Math.Cos(x.thuc), 14).ToString();
                                else
                                    return Math.Round(Math.Cos(x.thuc * Math.PI / 180), 14).ToString();
                            }
                            else
                                return "Math Error";
                        }
                    case "tan":
                        {
                            if (x.ao == 0)
                            {
                                if (MODE.Goc == MODE.ModeGoc.RAD)
                                    return Math.Round(Math.Tan(x.thuc), 14).ToString();
                                else
                                    return Math.Round(Math.Tan(x.thuc * Math.PI / 180), 14).ToString();
                            }
                            else
                                return "Math Error";
                        }
                    case "lg":
                        {
                            if (x.ao == 0)
                                return Math.Log10(x.thuc).ToString();
                            else
                                return "Math Error";
                        }
                    case "ln":
                        {
                            if (x.ao == 0)
                                return Math.Log(x.thuc).ToString();
                            else
                                return "Math Error";
                        }
                    default: return "Syntax Error";
                }
            }
        }

        public string HaiNgoi(string s1, string s2)
        {
            SoPhuc x1 = SoPhuc.Parse(s1);
            SoPhuc x2 = SoPhuc.Parse(s2);
            switch (form)
            {
                case "+": return (x1 + x2).ToString();
                case "–": return (x1 - x2).ToString();
                case "*": return (x1 * x2).ToString();
                case "/":
                    {
                        if (x2.thuc == 0 && x2.ao == 0)
                            return "Math Error";
                        else
                            return (x1 / x2).ToString();
                    }
                case "^":
                    {
                        if (x2.ao == 0)
                            return (x1 ^ x2.thuc).ToString();
                        else return "Math Error";
                    }

                case "log":
                    {
                        if (x2.ao == 0 && x1.ao == 0 && x1.thuc != 1)
                            return Math.Log(x2.thuc, x1.thuc).ToString();
                        else return "Math Error";
                    }
                default: return "Syntax Error";
            }
        }
    }
}
