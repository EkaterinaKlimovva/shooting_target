using System;

namespace Стрельба_по_мишене
{
    class Program
    {
        static void Main(string[] args)
        {
            // Максимальное значение в пределах которого вычисляется координаты выстрела
            short MaxValue = 15;
            // Задержка при вычислении и отображении изменяющихся координат
            short Sleep = 30;
            // Шаг с которым идут круги на мишени
            short Step = 1;
            // Количество секций на мишени (остальное "молоко")
            short MaxScore = 10;

            double X, Y; // Значение выстрела по оси Х и Y
            int Score = 0, allScore = 0, v = 1; // Счёт за один выстрел, общий счёт
            string close; // Переменная для проверки выхода из игры
            
            Console.WriteLine("Изменить установки игры? (Y - да)");
            if (Console.ReadLine() == "Y")
            {
                do
                {
                    Console.WriteLine("Введите ширину всей мишени [1, 50] (15 по умолчанию):");
                    MaxValue = Convert.ToInt16(Console.ReadLine());

                    Console.WriteLine("Введите ширину одной секции [1, 10] (1 по умолчанию):");
                    Step = Convert.ToInt16(Console.ReadLine());

                    Console.WriteLine("Введите количество секций [1, 50] (10 по умолчанию):");
                    MaxScore = Convert.ToInt16(Console.ReadLine());

                    Console.WriteLine("Введите задержку [10, 300] (30 по умолчанию):");
                    Sleep = Convert.ToInt16(Console.ReadLine());

                } while (Proverka(MaxValue, Step, MaxScore, Sleep) == false);
            }
            
            do
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("**************************************");
                Console.WriteLine("Выстрел: " + v);
                Console.WriteLine("**************************************");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("Определение X ... Нажмите клавишу ...");
                X = Vistrel(MaxValue, Sleep);
                Console.WriteLine("X = " + X);
                Console.ReadKey(true);
                Console.ReadLine();
                Console.WriteLine("Определение Y ... Нажмите клавишу ...");
                Y = Vistrel(MaxValue, Sleep);
                Console.WriteLine("Y = " + Y);
                Console.ReadKey(true);

                Score = Schet(X, Y, MaxScore, Step, out Score);
                allScore += Score;

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("**************************************");
                Console.WriteLine("Выстрел: " + Score + " Общий счёт: " + allScore);
                Console.WriteLine("**************************************");
                Console.ForegroundColor = ConsoleColor.White;
                v++;

                Console.WriteLine("Выйти? (Y - да) ");
                close = Console.ReadLine();
            } while (close != "Y");
 
        }

        // Проверка введённых данных на совместимость
        public static bool Proverka(short value, short shag, short score, short pause)
        {
            if ((value >= 1 && value <= 50) && (shag >= 1 && shag <= 10) && (score >= 1 && score <= 50) && (pause >= 10 && pause <= 300) && (value >= shag * score))
                return true;
            else
            {
                Console.WriteLine("Вы ввели неправильные значения, повторите ввод");
                Console.WriteLine();
                return false;
            }        
        }

        // Выстрел
        public static double Vistrel(short value, short pause)
        {
            double num; // Числа, которые "бегают на экране"
            Random random = new Random();

            num = random.Next(-value, +value);
            while (!Console.KeyAvailable)
            {
                Console.Write(num);
                Console.CursorLeft = 0;
                num += random.NextDouble();

                if (num > value)
                    num = -value + (num - value);

                System.Threading.Thread.Sleep(pause);
            }
            return num;
        }


        // Подсчёт очков
        public static int Schet(double X, double Y, short MaxScore, short shag, out int Score)
        {
            Score = MaxScore;
            if (Math.Abs(X) > MaxScore || Math.Abs(Y) > MaxScore)
            {
                Score = 0;
            } else
            {
                while (Math.Abs(X) >= shag || Math.Abs(Y) >= shag)
                {
                    X = Math.Abs(X) - shag;
                    Y = Math.Abs(Y) - shag;
                    Score--;
                }
            }
            return Score;
        }
    }
}
