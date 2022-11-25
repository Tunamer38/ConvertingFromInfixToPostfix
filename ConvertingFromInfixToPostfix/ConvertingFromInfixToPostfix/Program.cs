using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertingFromInfixToPostfix
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("YAMUK ALAN HESAPLAMA İŞLEMİ\n" + "<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>");
            Console.Write("Üst Kenar Uzunluğunu Giriniz: ");
            int UstKenar = Convert.ToInt32(Console.ReadLine());
            Console.Write("Alt Kenar Uzunluğunu Giriniz: ");
            int AltKenar = Convert.ToInt32(Console.ReadLine());
            Console.Write("Üst Kenar İle Alt Kenar Arası Mesafeyi (Yamuk Yükseklik) Giriniz: ");
            int Yukseklik = Convert.ToInt32(Console.ReadLine());
            string Denklem = String.Format("({0}+{1})/2*{2}", UstKenar, AltKenar, Yukseklik);
            Console.Write("Bilgisayara Atılan Çözüm Beklenen Denklem:\n" + Denklem);
            Console.Write("\n" + "Denklemin Postfix hali:\n" + ConvertPostFix(Denklem));

            Console.WriteLine("ÇOK BİLİNMEYENLİ DENKLEM\n" + "<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>");
            string Bilinmeyenli = "a/(b*(d-f/(b-b*a+d)+h)+x)+j";
            Console.WriteLine("POSTFİX'E ÇEVRİLECEK DENKLEM: \n"+ Bilinmeyenli);
            Console.WriteLine("Çevrilmiş Hali: \n"+ ConvertPostFix(Bilinmeyenli));
            Console.ReadLine();
        }
        public static string ConvertPostFix(string InFix)
        {
            string PostFix = "";
            Stack<char> Oprtr = new Stack<char>();
            for (int i = 0; i < InFix.Length; i++)
            {
                char Karakter = InFix[i];
                if (char.IsDigit(Karakter))
                {
                    while (i < InFix.Length && char.IsDigit(InFix[i]))
                    {
                        PostFix += InFix[i];
                        i++;
                    }
                    i--;
                    continue;
                }
                else if (Karakter == '(') Oprtr.Push(Karakter);
                else if (Karakter == '*' || Karakter == '+' || Karakter == '-' || Karakter == '/' || Karakter == '^')
                {
                    while (Oprtr.Count != 0 && Oprtr.Peek() != '(')
                    {
                        if (OnKontrol(Oprtr.Peek(), Karakter))
                            PostFix += Oprtr.Pop();
                        else
                            break;
                    }
                    Oprtr.Push(Karakter);
                }
                else if (Karakter == ')')
                {
                    while (Oprtr.Count != 0 && Oprtr.Peek() != '(')
                    {
                        PostFix += Oprtr.Pop();
                    }
                    if (Oprtr.Count != 0) Oprtr.Pop();
                }
                else PostFix += Karakter;
            }
            while (Oprtr.Count != 0)
            {
                PostFix += Oprtr.Pop();
            }
            return PostFix;
        }
        public static bool OnKontrol(char IlkIslem, char IkinciIslem)
        {
            string Sıralama = "(+-*/^";
            int[] Oncelikler = { 0, 1, 1, 2, 2, 3 };
            int IlkIslemOnceligi = Sıralama.IndexOf(IlkIslem);
            int IkinciIslemOnceligi = Sıralama.IndexOf(IkinciIslem);
            return (Oncelikler[IlkIslemOnceligi] >= Oncelikler[IkinciIslemOnceligi]);
        }

    }
}
