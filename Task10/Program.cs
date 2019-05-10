using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task10
{

    class Program
    {
        class Point
        {
            double aCoeff;
            int iCoeff;
            bool alreadyUsed;
            public Point next, pred;

            public double ACoeff
            {
                get { return aCoeff; }
                set { aCoeff = value; }
            }
   
            public int ICoeff
            {
                get { return iCoeff; }
                set { iCoeff = value; }
            }

            public bool AlreadyUsed
            {
                get { return alreadyUsed; }
                set { alreadyUsed = value; }
            }

            public Point()
            {
                ACoeff = 0;
                ICoeff = 0;
                next = null;
                pred = null;
                alreadyUsed = false;
            }

            public Point(double a, int d)
            {
                ICoeff = d;
                ACoeff = a;
                next = null;
                pred = null;
                alreadyUsed = false;
            }

            public override string ToString()
            {
                return ACoeff+"x^"+ICoeff+" ";
            }
        }

        static Random rnd = new Random();

        static Point MakePoint(double a, int i)
        {
            Point p = new Point(a, i);
            return p;
        }

        static Point MakeList2(int size, bool rand) //формирование двунаправленного списка
                                                    //добавление в начало
        {
            int a = 0;
            int i = 0;
            if (size == 0) return null;
            if (size != 0)
            {
                if (rand)
                {
                    a = rnd.Next(0, 100);
                    i = rnd.Next(0, 3);
                }
                else
                {
                    Console.WriteLine("введите элементы списка");
                    a = ReadAnswer();
                    i = ReadAnswer();
                }
            }
            Point beg = MakePoint(a, i);
            for (int j = 1; j < size; j++)
                beg = AddPointToEnd(beg, rand);

            Console.WriteLine("список сформирован");
            return beg;
        }

        static Point AddPointToEnd(Point beg, bool rand)
        {
            int a = 0;
            int i = 0;
            
                if (rand)
                {
                    a = rnd.Next(0, 100);
                    i = rnd.Next(0, 3);
                }
                else
                {
                    Console.WriteLine("введите элементы списка");
                    a = ReadAnswer();
                    i = ReadAnswer();
                }
           
            return AddPointToEnd(beg, a, i);
            //добавить элемент в конец списка
        }

        static Point AddPointToEnd(Point beg, double a, int i)
        {
            Point p = MakePoint(a, i); 
            Point buf = beg;
            while (buf.next != null) buf = buf.next;
            buf.next = p;
            p.pred = buf;
            buf = p;
            return beg;
        }

        static Point DelThisElement(Point beg)
        {
            Point currentEl = beg;
            while (currentEl.next != null)
            {
                if (currentEl.ACoeff!=0)
                currentEl = currentEl.next;
                else
                {
                    currentEl.pred.next = currentEl.next; // удаляем нужный элемент
                    if (currentEl.next != null)
                        currentEl.next.pred = currentEl.pred; 
                }
            }
            return beg;
        }

        static Point DelFirstElem(Point beg)
        {
            beg = beg.next;
            if (beg != null)
                beg.pred = null;
            return beg;
        }



        static Point Mult(Point p1, Point p2)
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

        static Point Same(Point p)
        {
            Point res = new Point();
            Point buf = p;
            double lastTemp = p.ICoeff+1.45;
            while(buf!=null)
            {
                Point currentEl = buf.next;
                int temp = buf.ICoeff;
                double a = buf.ACoeff;
                if (temp != lastTemp)
                {
                    while (currentEl != null)
                    {
                        if (currentEl.ICoeff == temp)
                            a += currentEl.ACoeff;
                        currentEl = currentEl.next;
                    }
                    res = AddPointToEnd(res, a, temp);
                }
                lastTemp = buf.ICoeff;
                buf = buf.next;
            }
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


        public static int ReadAnswer()
        {
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
            p1 = MakeList2(2, false);
            Point p2 = new Point();
            p2 = MakeList2(2, false);
            Point p = new Point(p1.ACoeff * p2.ACoeff, p1.ICoeff + p2.ICoeff);
            p = Mult(p1, p2);
            ShowList(p);
            p = DelFirstElem(p);
            p = Same(p);
            p = DelFirstElem(p);
            ShowList(p);
        }
    }
}
