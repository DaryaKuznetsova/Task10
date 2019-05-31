using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{

    class Program
    {
        class Point
        {
            int aCoeff;
            int iCoeff;
            public Point next, pred;

            public int ACoeff
            {
                get { return aCoeff; }
                set { aCoeff = value; }
            }
   
            public int ICoeff
            {
                get { return iCoeff; }
                set { iCoeff = value; }
            }

            public Point()
            {
                ACoeff = 0;
                ICoeff = 0;
                next = null;
                pred = null;
            }

            public Point(int a, int d)
            {
                ICoeff = d;
                ACoeff = a;
                next = null;
                pred = null;
            }

            public override string ToString()
            {
                return ACoeff+"x^"+ICoeff+" ";
            }
        }

        static Random rnd = new Random();

        static Point MakePoint(int a, int i)
        {
            Point p = new Point(a, i);
            return p;
        }

        static Point MakeList2(string filename) //формирование двунаправленного списка                                                   //добавление в начало
        {
            int a = 0;
            int i = 0;
            string temp;
            StreamReader sr = new StreamReader(filename);
            try
            {
                do
                {
                    temp = Convert.ToString(sr.ReadLine());
                    string[] words = temp.Split(new char[] { ' ' });
                    a = Convert.ToInt32(words[0]);
                    i = Convert.ToInt32(words[1]);
                } while (a == 0);
            }
            catch(Exception )
            {
                Console.WriteLine("!!!!!!");
            }
            Point beg = MakePoint(a, i);
            while ((temp=sr.ReadLine()) != null)
            {
               string[] words = temp.Split(new char[] { ' ' });
                a = Convert.ToInt32(words[0]);
                i = Convert.ToInt32(words[1]);
                if(a!=0)
                beg = AddPointToEnd(beg, a, i);
            }
            Console.WriteLine("список сформирован");
            sr.Close();
            return beg;
        }


        static Point AddPointToEnd(Point beg, int a, int i)
        {
            Point p = MakePoint(a, i); 
            Point buf = beg;
            while (buf.next != null) buf = buf.next;
            buf.next = p;
            p.pred = buf;
            buf = p;
            return beg;
        }

        static Point DelThisElement(Point beg) // удаляем элемент с нулевым коэффициентом
        {
            Point currentEl = beg;
            while (currentEl.next != null)
            {
                if (currentEl.ACoeff!=0)
                currentEl = currentEl.next;
                else
                {
                    currentEl  = Delete(currentEl);
                }
            }
            while (currentEl.pred != null)
                currentEl = currentEl.pred;
            return currentEl;
        }

        static Point Delete(Point beg)
        {
            Point currentEl = beg;
            if (currentEl.pred == null) // удаляем первый элемент
            {
                currentEl = DelFirstElem(currentEl);
            }
            else
            {
                currentEl.pred.next = currentEl.next; // удаляем нужный элемент
                if (currentEl.next != null)
                    currentEl.next.pred = currentEl.pred;
            }
            return currentEl;
        }

        static Point DelFirstElem(Point beg)
        {
            beg = beg.next;
            if (beg != null)
                beg.pred = null;
            return beg;
        }

        static Point Mult(Point p1, Point p2) // умножение
        {
            Point res = new Point();
            Point buf = p1;
            while (buf!= null)
            {
                Point buf2 = p2;
                while (buf2!= null)
                {
                    res = AddPointToEnd(res, buf.ACoeff * buf2.ACoeff, buf.ICoeff + buf2.ICoeff);
                    buf2 = buf2.next;
                }
                buf = buf.next;
            }
            return res;
        }


        static Point Same(Point beg) // подобные
        {
            Point res = new Point();
            Point currentEl = new Point();
            while(beg.next!=null) // от первого слагаемого
            {
                currentEl = beg.next;
                int a = beg.ACoeff;
                int i = beg.ICoeff;
                while(currentEl.next!=null) // посик слагаемых с той же степенью
                {
                    if(currentEl.ICoeff==i) // если степень равна, коэффициенты складываем
                    {
                        a += currentEl.ACoeff;
                        currentEl.pred.next = currentEl.next; // слагаемое вычеркиваем (уничтожилось)
                        currentEl.next.pred = currentEl.pred;
                    }                    
                        currentEl = currentEl.next; // переходим к следующему
                }
               
                // вышли из цикла проверяем последний элемент
                if (currentEl.ICoeff == i)
                {
                    a += currentEl.ACoeff;
                    currentEl.pred.next = null;
                }
                while (currentEl.pred != null) currentEl = currentEl.pred; // возвращаемся в начало списка
                if (a != 0)
                    res = AddPointToEnd(res, a, i); // добавляем в новый список новое слагаемое
                beg = beg.next;
            }
            res = AddPointToEnd(res, beg.ACoeff, beg.ICoeff); // добавляем в список последнее слагаемое
            return res;

        }

        static void ShowList(Point beg)
        {
            Point p = beg;
            if (beg != null)
            {
                while (p.next != null)
                {
                    Console.Write(p);
                    p = p.next;
                }
                Console.Write(p);
                Console.WriteLine();
            }
            else Console.WriteLine("список пуст");
        }

        static void RewriteList(Point beg)
        {
            StreamWriter sw = new StreamWriter("output.txt");
            Point p = beg;
            if (beg != null)
            {
                while (p.next!= null)
                {
                    sw.WriteLine(p.ACoeff + " " + p.ICoeff);
                    p = p.next;
                }
                sw.WriteLine(p.ACoeff + " " + p.ICoeff);
            }
            else Console.WriteLine("список пуст");
            sw.Close();
        }


        public static int ReadAnswer()
        {;
            int a = 0;
            bool ok = false;
            do
            {
                try
                {
                    a = Convert.ToInt32(Console.ReadLine());
                    ok = true;
                }
                catch (Exception)
                {
                    Console.WriteLine("Пожалуйста, введите целое число.");
                    ok = false;
                }
            } while (!ok);
            return a;
        }
        static void Main(string[] args)
        {
            Point p1 = new Point();
            p1 = MakeList2("input1.txt");
            Point p2 = new Point();
            p2 = MakeList2("input2.txt");
            Point p = new Point(p1.ACoeff * p2.ACoeff, p1.ICoeff + p2.ICoeff);
            p = Mult(p1, p2);
            p = Same(p);
            p = DelThisElement(p);
            ShowList(p);
            RewriteList(p);
        }
    }
}
