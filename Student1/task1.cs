using System; 
using System.Collections.Generic;

public class MainClass
{
    class ChecksComparer : IComparer<string[]> // класс для сравнение значений в листе по номеру чека
    {
        public int Compare(string[] s1, string[] s2)
        {
            int a = Convert.ToInt32(s1[0]);
            int b = Convert.ToInt32(s2[0]);
            if (a > b)
            {
                return 1;
            }
            else if (a < b)
            {
                return -1;
            }
            return 0;
        }
    }
    public static string StringArrToString(string[] s)
    {
        string ss = string.Empty;
        for (int i = 0; i < s.Length; i++)
        {
            ss += s[i] + " ";
        }
        return ss.TrimEnd();
    }
    public static void ListToStringArr(List<string[]> list, string[] s) 
    {
        for(int i = 0, j = 1; i < list.Count; i++, j++)
        {
            string[] ss = list[i];
            s[j] = string.Empty;
            s[j] = StringArrToString(ss);
        }
    }
    public static string Balance(string book)
    {
        string s = string.Empty;
        for (int i = 0; i < book.Length; i++) // пишу в строку только нужные символы
        {
            if (book[i] >= 'A' && book[i] <= 'Z'  book[i] >= 'a' && book[i] <= 'z'  book[i] >= '0' && book[i] <= '9'  book[i] == ' '  book[i] == '.' || book[i] == '\n')
            {
                s += Convert.ToString(book[i]);
            }
        }
        s = s.Replace('.', ','); // моя студия не преобразует в double, если стоит "." между цифрами
        string[] s0 = s.Split('\n', StringSplitOptions.RemoveEmptyEntries); // разбиваю строку на строки
        List<string[]> check_numbers = new List<string[]>(s0.Length - 1);
        for (int i = 0, j = 1; i < s0.Length - 1; i++, j++) // закидываю в лист строки, которые нужно отсортировать по номеру чека
        {
            check_numbers.Add(s0[j].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
        ChecksComparer cn = new ChecksComparer();
        check_numbers.Sort(cn); // сортирую по номеру чека
        ListToStringArr(check_numbers, s0);
        double[] Balance = new double[s0.Length];
        Balance[0] = Math.Round(double.Parse(s0[0]), 2, MidpointRounding.AwayFromZero);
        double total_expence = 0.0;
        for (int i = 1; i < Balance.Length; i++) // считаю баланс по порядку чеков
        {
            string[] s1 = s0[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            s0[i] = string.Empty;
            s0[i] = StringArrToString(s1);
            Balance[i] = Math.Round(Balance[i - 1] - Math.Round(double.Parse(s1[s1.Length - 1]), 2, MidpointRounding.AwayFromZero), 2, MidpointRounding.AwayFromZero);
            total_expence += Math.Round(double.Parse(s1[s1.Length - 1]), 2, MidpointRounding.AwayFromZero);
        }
        double average_expense = total_expence / (Balance.Length - 1);
        string s2 = "Original Balance: " + s0[0];
        for (int i = 1; i < s0.Length; i++)
        {
            s2 += "\n" + s0[i] + " Balance " + Balance[i].ToString("F2");
        }
        s2 += "\n" + "Total expense " + (Math.Round(total_expence, 2, MidpointRounding.AwayFromZero)).ToString("F2") + "\n" + "Average expense " + average_expense.ToString("F2");
        s2 = s2.Replace(',', '.');
        return s2;
    }
    public static void Main()
    {
        string test1 = "1000.00!=\n125 Market !=:125.45\n126 Hardware =34.95\n127 Video! 7.45\n128 Book   :14.32\n129 Gasoline ::16.10";
        string test2 = "1233.00\n125 Hardware;! 24.80?\n123 Flowers 93.50;\n127 Meat 120.90\n120 Picture 34.00\n124 Gasoline 11.00\n" + "123 Photos;! 71.40?\n122 Picture 93.50\n132 Tyres;! 19.00,?;\n129 Stamps; 13.60\n129 Fruits{} 17.60\n129 Market;! 128.00?\n121 Gasoline;! 13.60?";
        string test3 = "1242.00\n122 Hardware; !13.60\n127 Hairdresser 13.10\n123 Fruits 93.50?;\n132 Stamps;!{ 13.60?;\n160 Pen;! 17.60?;\n002 Car;! 34.00";
        Console.WriteLine(Balance(test3));
    }
}