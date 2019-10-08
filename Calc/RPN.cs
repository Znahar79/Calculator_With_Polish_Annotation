using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calc
{
    class RPN
    {
        static DivideByZeroException ex = new DivideByZeroException();
        //static Exception exept;

        //Метод Calculate принимает выражение в виде строки и возвращает результат, в своей работе использует другие методы класса
        static public void Calculate(string input)
        {
            try
            {
            //int counter_of_digits = 0;
                //input = ZeroAdder(input);
                //Console.WriteLine(input);
                string output = GetExpression(input); //Преобразовываем выражение в постфиксную запись
                Console.WriteLine("В польской записи: " + output);
           // Console.WriteLine(output);
                double result = Counting(output); //Решаем полученное выражение
                Console.WriteLine("Результат: " + result); //Возвращаем результат
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
           //     //Console.Read();
           //     //return 0;
            }
        }
        //Метод для добавления нулей перед минусос для отрицательных чисел
        static private string ZeroAdder(string input)
        {
            bool have_open_bracket = false;
            
            string output = string.Empty;
            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                if (input[i] == '(') { have_open_bracket = true; }
                if (input[i] == ')') { have_open_bracket = false; }
                if (input[i]=='-' && output.Length == 0)
                {
                    output += " 0 ";
                }
                else if(input[i] == '-' && output.Length > 0)
                {
                    if(have_open_bracket == true) { output += " 0 "; }
                    else { output += " +0 "; }
                    
                }
                output += input[i];
            }
            return output;
        }

        //static private string BracketsAdder(string input)
        //{
        //    string output = string.Empty;
        //    bool have_multi = false;
        //    bool have_open_bracket = false;
        //    for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
        //    {
        //        if (input[i] == '*') { have_multi = true; }
        //        if (input[i] == '(') { have_open_bracket = true; }
        //        if (input[i] == ')') { have_open_bracket = false; }
        //    }
        //    return output;
        //}
        //Метод, преобразующий входную строку с выражением в постфиксную запись
        static private string GetExpression(string input)
        {
            //int counter_of_symb = 0;
            bool bracket_open = false;
            //bool bracket_close = false;
            string output = string.Empty; //Строка для хранения выражения
            Stack<char> operStack = new Stack<char>(); //Стек для хранения операторов
            //int counter_of_digits = 0;
            for (int i = 0; i < input.Length; i++) //Для каждого символа в входной строке
            {
                if (input[i] == ',')
                {
                    output += " 0,";
                }
                //Разделители пропускаем
                if (IsDelimeter(input[i]))
                    continue; //Переходим к следующему символу

                //Если символ - цифра, то считываем все число
                if (Char.IsDigit(input[i])) //Если цифра
                {
                    //Читаем до разделителя или оператора, что бы получить число
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        output += input[i]; //Добавляем каждую цифру числа к нашей строке
                        i++; //Переходим к следующему символу

                        if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }

                    output += " "; //Дописываем после числа пробел в строку с выражением
                    i--; //Возвращаемся на один символ назад, к символу перед разделителем
                }
                if (IsLog(input[i]))
                {
                    double a, b;
                    string _a = string.Empty;
                    string _b = string.Empty;
                    i = i + 4;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _a += input[i];
                        i++;
                    }
                    a = double.Parse(_a);
                    i += 1;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _b += input[i];
                        i++;
                        //if (i == input.Length) break; //Если символ - последний, то выходим из цикла
                    }
                    b = double.Parse(_b);
                    //i += 1;
                    if (Math.Log(a, b)>=0)
                    {
                        output += Math.Log(a, b).ToString();

                        
                        output += " "; //Дописываем после числа пробел в строку с выражением
                    }
                    else
                    {
                        output += "0 ";
                        output += (Math.Log(a, b)*(-1)).ToString();
                        output += " - ";
                    }

                    if (i == input.Length-1) { break; } else i++; //Если символ - последний, то выходим из цикла
                }
                if (IsSin(input[i]))
                {
                    double a;
                    string _a = string.Empty;
                    i = i + 4;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _a += input[i];
                        i++;
                    }
                    a = double.Parse(_a);
                    if (Math.Sin(a) >= 0)
                    {
                        output += Math.Sin(-a).ToString();


                        output += " "; //Дописываем после числа пробел в строку с выражением
                    }
                    else
                    {
                        output += "0 ";
                        output += (Math.Sin(a) * (-1)).ToString();
                        output += " - ";
                    }

                    if (i == input.Length - 1) { break; } else i++; //Если символ - последний, то выходим из цикла
                }
                if (IsCos(input[i]))
                {
                    double a;
                    string _a = string.Empty;
                    i = i + 4;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _a += input[i];
                        i++;
                    }
                    a = double.Parse(_a);
                    if (Math.Cos(a) >= 0)
                    {
                        output += Math.Cos(a).ToString();


                        output += " "; //Дописываем после числа пробел в строку с выражением
                    }
                    else
                    {
                        output += "0 ";
                        output += (Math.Cos(a) * (-1)).ToString();
                        output += " - ";
                    }

                    if (i == input.Length-1) { break; } else i++; //Если символ - последний, то выходим из цикла
                }
                if (IsTan(input[i]))
                {
                    double a;
                    string _a = string.Empty;
                    i = i + 4;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _a += input[i];
                        i++;
                    }
                    a = double.Parse(_a);
                    if (Math.Tan(a) >= 0)
                    {
                        output += Math.Tan(a).ToString();


                        output += " "; //Дописываем после числа пробел в строку с выражением
                    }
                    else
                    {
                        output += "0 ";
                        output += (Math.Tan(a) * (-1)).ToString();
                        output += " - ";
                    }

                    if (i == input.Length-1) { break; } else i++; //Если символ - последний, то выходим из цикла
                }
                if (IsAtan(input[i]))
                {
                    double a;
                    string _a = string.Empty;
                    i = i + 5;
                    while (!IsDelimeter(input[i]) && !IsOperator(input[i]))
                    {
                        _a += input[i];
                        i++;
                    }
                    a = double.Parse(_a);
                    if (Math.Atan(a) >= 0)
                    {
                        output += Math.Atan(a).ToString();


                        output += " "; //Дописываем после числа пробел в строку с выражением
                    }
                    else
                    {
                        output += "0 ";
                        output += (Math.Atan(a) * (-1)).ToString();
                        output += " - ";
                    }

                    if (i == input.Length-1) { break; } else i++; //Если символ - последний, то выходим из цикла
                }
                
                //Если символ - оператор
                if (IsOperator(input[i])) //Если оператор
                {
                    if (input[i] == '(') //Если символ - открывающая скобка
                    {
                        bracket_open = true;
                        //bracket_close = false;
                        operStack.Push(input[i]);
                    } //Записываем её в стек

                    else if (input[i] == ')') //Если символ - закрывающая скобка
                    {
                        if (bracket_open == false){ throw new CloseBracketException(); }
                        bracket_open = false;
                        //bracket_close = true;
                        //Выписываем все операторы до открывающей скобки в строку
                        char s = operStack.Pop();

                        while (s != '(')
                        {
                            output += s.ToString() + ' ';
                            s = operStack.Pop();
                        }
                    }
                    else //Если любой другой оператор
                    {

                        if (operStack.Count > 0) //Если в стеке есть элементы
                            while (GetPriority(input[i]) <= GetPriority(operStack.Peek())) //И если приоритет нашего оператора меньше или равен приоритету оператора на вершине стека
                            {
                                output += operStack.Pop().ToString() + " "; //То добавляем последний оператор из стека в строку с выражением
                                if (operStack.Count == 0)
                                    break;
                            }
                        operStack.Push(char.Parse(input[i].ToString())); //Если стек пуст, или же приоритет оператора выше - добавляем операторов на вершину стека

                    }
                }
                if(i == input.Length - 1 && bracket_open == true) { throw new BracketException(); }
                //if(i == input.Length - 1 && bracket_open == true && bracket_close == true ) { throw new CloseBracketException(); }
            }

            //Когда прошли по всем символам, выкидываем из стека все оставшиеся там операторы в строку
            while (operStack.Count > 0)
                output += operStack.Pop() + " ";

            return output; //Возвращаем выражение в постфиксной записи
        }

        //Метод, вычисляющий значение выражения, уже преобразованного в постфиксную запись
        static private double Counting(string input)
        {
            double result = 0; //Результат
            Stack<double> temp = new Stack<double>(); //временный стек для решения

            for (int i = 0; i < input.Length; i++) //Для каждого символа в строке
            {
                //Если символ - цифра, то читаем все число и записываем на вершину стека
                if (Char.IsDigit(input[i]))
                {
                    string a = string.Empty;

                    while (!IsDelimeter(input[i]) && !IsOperator(input[i])) //Пока не разделитель
                    {
                        a += input[i]; //Добавляем
                        i++;
                        if (i == input.Length) break;
                    }

                    temp.Push(double.Parse(a)); //Записываем в стек
                    i--;
                }
                else if (IsOperator(input[i])) //Если символ - оператор
                {
                    //if (input[i] == '-' && temp.Count() == 0) { temp.Push(0); temp.Push(0); }
                    //Берем два последних значения из стека
                    double a = temp.Pop();
                    double b = temp.Pop();

                    switch (input[i]) //И производим над ними действие, согласно оператору
                    {
                        case '+': result = b + a; break;
                        case '-': result = b - a; break;
                        case '*': result = b * a; break;
                        case '/':
                            if (a == 0) { throw ex; }
                            result = b / a;
                            break;
                        case '^': result = double.Parse(Math.Pow(double.Parse(b.ToString()), double.Parse(a.ToString())).ToString()); break;
                    }
                    temp.Push(result); //Результат вычисления записываем обратно в стек
                }
            }
            return temp.Peek(); //Забираем результат всех вычислений из стека и возвращаем его 
        }

        //Метод возвращает true, если проверяемый символ - разделитель ("пробел" или "равно")
        static private bool IsDelimeter(char c)
        {
            if ((" =".IndexOf(c) != -1))
                return true;
            return false;
        }
       
        //Метод возвращает true, если проверяемый символ - оператор
        static private bool IsOperator(char с)
        {
            if (("+-/*^()".IndexOf(с) != -1))
                return true;
            return false;
        }

        //Методs возвращают true, если проверяемый символ - функция
        static private bool IsLog(char с)
        {
            if (("lL".IndexOf(с) != -1))
                return true;
            return false;
        }
        static private bool IsSin(char с)
        {
            if (("sS".IndexOf(с) != -1))
                return true;
            return false;
        }
        static private bool IsCos(char с)
        {
            if (("cC".IndexOf(с) != -1))
                return true;
            return false;
        }
        static private bool IsTan(char с)
        {
            if (("tT".IndexOf(с) != -1))
                return true;
            return false;
        }
        static private bool IsAtan(char с)
        {
            if (("aA".IndexOf(с) != -1))
                return true;
            return false;
        }
        static private bool IsFac(char с)
        {
            if (("fF".IndexOf(с) != -1))
                return true;
            return false;
        }

        static private byte GetPriority(char s)
        {
            switch (s)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 3;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                //case 'l': return 7;
               // case 's': return 8;
               // case 'c': return 9;
                //case 't': return 10;

                default: return 6;
            }
        }
        
    }
}
