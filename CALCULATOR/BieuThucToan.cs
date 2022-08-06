using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CALCULATOR
{
    class BieuThucToan
    {
        public string form;

        public BieuThucToan()
        {

        }
        public BieuThucToan(string Form)
        {
            form = Form;
        }

        public void HoanThienBieuThuc()
        {
            form = form.Replace(" ", "");
            form = Regex.Replace(form, @"[pP][iI]", "π");
            form = Regex.Replace(form, @"[sS][iI][nN]", "sin");
            form = Regex.Replace(form, @"[cC][oO][sS]", "cos");
            form = Regex.Replace(form, @"[tT][aA][nN]", "tan");
            form = Regex.Replace(form, @"[lL][oO][gG]", "log");
            form = Regex.Replace(form, @"[lL][gG]", "lg");
            form = Regex.Replace(form, @"[lL][nN]", "ln");
            form = Regex.Replace(form, @"[sS][qQ][rR][tT]", "sqrt");
            form = Regex.Replace(form, @"[aA][bB][sS]", "abs");

            // thêm dấu nhân vào các vị trí bị khuyết
            // ví dụ 2e ->2*e; 2sin(45) -> 2*sin(45); 9(8)(9) -> 9*(8)*(9)
            Regex regex1 = new Regex(@"(?<=[πei)!\d])(?=sin|cos|tan|log|lg|ln|abs|sqrt|[(πe]|i(?=[\^]))|(?<=[πei)!])(?=[\di])");
            form = regex1.Replace(form, "*");

            // gom các cặp dấu -- hoặc -– hoặc –– hoặc +++++ thành 1 dấu +
            // ví dụ --++-9  -> -9
            Regex regex2 = new Regex(@"\+*[–-]\+*[-–]\+*|\+{2,}");
            form = regex2.Replace(form, "+");

            // gom các cặp dấu -+ +- –+ +– hoặc +-+ +–+ thành 1 dấu -
            // vd: 9+-3 ->9-3;     9+-+3 -> 9-3
            Regex regex3 = new Regex(@"\+*[\-–]\+*");
            form = regex3.Replace(form, "-");

            // xóa dấu cộng nếu nằm sau *,^,/ và trc số; 
            // vd: 9*++3 -> 9*3;     +++9 -> 9
            Regex regex4 = new Regex(@"(?<=[\*\^\/(])[+]+(?=\d)|^[+]+(?=\d)");
            form = regex4.Replace(form, "");
            // phân biệt dấu âm hay dấu trừ( trong trường hợp gõ bằng bàn phím, dấu - vừa là dấu âm vừa là dấu trừ nên khi xử lý cần phân biệt)
            // vd 8-9 là dấu trừ -> 8–9; -2^2 là dấu âm -> giữ nguyên;
            Regex regex5 = new Regex(@"(?<=[\d\)eπi])\-(?=[\-\d\(eπiasctl])");
            form = regex5.Replace(form, @"–");

            // thay e=2.71828.... π=3.14159....
            form = form.Replace("e", Math.E.ToString());
            form = form.Replace("π", Math.PI.ToString());
        }

        // tách chuỗi biểu thức trung tố thành mảng các phần tử gồm số, toán tử, hàm số, ngoặc,....
        // nếu tổng độ dài các phần tử trong mảng khác độ dài của biểu thức ban đầu -> lỗi cú pháp
        public bool TachBieuThuc(out PhanTuBieuThuc[] TrungTo)
        {
            Regex regex = new Regex(@"sin|cos|tan|lg|ln|log|abs|sqrt|(?<=sin|cos|tan|ln|lg|abs|sqrt|[\^])\-\d+(\.\d+)?i?|\d+(\.\d+)?i?|[+\-*\/^,()!–i]");

            MatchCollection MC = regex.Matches(form);
            TrungTo = new PhanTuBieuThuc[MC.Count];
            int DoDaiTrungTo = 0;
            int i = 0;
            foreach (Match m in MC)
            {
                PhanTuBieuThuc PhanTu = new PhanTuBieuThuc(m.ToString());
                TrungTo[i] = PhanTu;
                DoDaiTrungTo += TrungTo[i].form.Length;
                i++;
            }
            if (DoDaiTrungTo == form.Length)
                return true;
            else
                return false;
        }

        // thuật toán ký pháp nghịch đảo ba lan
        public Queue<PhanTuBieuThuc> RPN(PhanTuBieuThuc[] TrungTo)
        {
            Queue<PhanTuBieuThuc> HauTo = new Queue<PhanTuBieuThuc>();
            Stack<PhanTuBieuThuc> PhanTu = new Stack<PhanTuBieuThuc>();

            // try catch bắt lỗi cú pháp, nếu thực hiện thuật toán xảy ra lỗi thì xóa hết phần tử trong stack và trả về stack rỗng
            try
            {
                for (int i = 0; i < TrungTo.Length; i++)
                {
                    if (Regex.IsMatch(TrungTo[i].form, @"\-?\d+(\.\d+)?|^i$"))
                        HauTo.Enqueue(TrungTo[i]);
                    else if (TrungTo[i].form == "(" || PhanTu.Count == 0)
                        PhanTu.Push(TrungTo[i]);
                    else
                    {
                        switch (TrungTo[i].form)
                        {
                            case ")":
                                {
                                    while (PhanTu.Peek().form != "(")
                                    {
                                        if (PhanTu.Peek().form == ",")
                                            PhanTu.Pop();
                                        else
                                            HauTo.Enqueue(PhanTu.Pop());
                                    }
                                    PhanTu.Pop();
                                }
                                break;
                            default:
                                {
                                    if (PhanTu.Peek().MucUuTien < TrungTo[i].MucUuTien || (PhanTu.Peek().form == "^" && TrungTo[i].form == "-"))
                                        PhanTu.Push(TrungTo[i]);
                                    else
                                    {
                                        do
                                        {
                                            if (PhanTu.Peek().form == ",")
                                                PhanTu.Pop();
                                            else
                                                HauTo.Enqueue(PhanTu.Pop());
                                        } while (PhanTu.Count > 0 && PhanTu.Peek().MucUuTien >= TrungTo[i].MucUuTien);
                                        PhanTu.Push(TrungTo[i]);
                                    }
                                }
                                break;
                        }
                    }
                }
                while (PhanTu.Count > 0)
                    HauTo.Enqueue(PhanTu.Pop());
            }
            catch
            {
                HauTo.Clear();
            }
            return HauTo;
        }

        // tính giá trị biểu thức hậu tố
        public string GiaTri()
        {
            HoanThienBieuThuc();
            PhanTuBieuThuc[] TrungTo;

            if (!TachBieuThuc(out TrungTo))
                return "Syntax Error";

            Queue<PhanTuBieuThuc> HauTo = RPN(TrungTo);

            if (HauTo.Count == 0)
                return "Syntax Error";

            Stack<string> KetQua = new Stack<string>();
            PhanTuBieuThuc PhanTu = new PhanTuBieuThuc();
            do
            {
                PhanTu = HauTo.Dequeue();
                // kiểm tra phần tử có phải là số không
                if (Regex.IsMatch(PhanTu.form, @"\-?\d+(\.\d+)?i?|^i$"))
                    KetQua.Push(PhanTu.form);
                else
                {
                    if (PhanTu.Ngoi == 1)
                    {
                        string x;
                        try
                        {
                            x = KetQua.Pop();
                        }
                        catch
                        {
                            return "Syntax Error";
                        }
                        string giatri = PhanTu.MotNgoi(x);
                        if (giatri == "Math Error")
                            return giatri;
                        else
                            KetQua.Push(PhanTu.MotNgoi(x));
                    }
                    else if (PhanTu.Ngoi == 2)
                    {
                        string x2, x1;
                        try
                        {
                            x2 = KetQua.Pop();
                            x1 = KetQua.Pop();
                        }
                        catch
                        {
                            return "Syntax Error";
                        }
                        string giatri = PhanTu.HaiNgoi(x1, x2);
                        if (giatri == "Math Error")
                            return giatri;
                        KetQua.Push(PhanTu.HaiNgoi(x1, x2));
                    }
                }
            } while (HauTo.Count > 0);
            if (KetQua.Count > 1)
                return "Syntax Error";
            else
                return KetQua.Pop();
        }
    }
}
