using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc
{
    class Program
    {
        static void Main(string[] args)
        {
            //try
            //{
                while (true) //Бесконечный цикл
                {
                    Console.Write("Введите выражение: "); //Предлагаем ввести выражение
                    RPN.Calculate(Console.ReadLine()); //Считываем, и выводим результат
                }
            //}
           // catch(Exception e)
            //{
             //   Console.WriteLine(e.Message);
              //  Console.Read();
           // }
        }
    }
}
