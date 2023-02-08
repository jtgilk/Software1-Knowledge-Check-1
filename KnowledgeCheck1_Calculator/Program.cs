using System;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace KnowledgeCheck1_Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            startup();
        }
        static void startup()
        {
            Console.WriteLine("Hello. Press 1 for addition, 2 for subtraction, 3 for multiplication, and 4 for division");

            var input = Console.ReadLine();
            calc(input);
        }
        static void Redo(string redo)
        {
            switch (redo)
            {
                case "Y" or "y":
                    Console.WriteLine("Restarting");
                    startup();
                    break;

                case "N" or "n":
                    break;
                default:
                    Console.WriteLine("Unknown input");
                    break;
            }
        }
        static void calc(string input)
        {
            int type;
            string calcType;
            string calcSymbol;
            if (int.TryParse(input, out type)) ;
            else
            {
                goto End;
            }
            switch (type)
            {
                case 1:
                    calcType = "Add";
                    calcSymbol = "+";
                    break;
                case 2:
                    calcType = "Subtract";
                    calcSymbol = "-";
                    break;
                case 3:
                    calcType = "Multiply";
                    calcSymbol = "*";
                    break;
                case 4:
                    calcType = "Divide";
                    calcSymbol = "/";
                    break;
                default: goto End;
            }
            //Create and then point to a Calculator obj before searching and invoking the correct method
            Type typeM = typeof(Calculator);
            object obj = Activator.CreateInstance(typeM);
            MethodInfo methodInfo = typeM.GetMethod(calcType);
            Console.WriteLine("Enter 2 integers to " + calcType);
            var first = Console.ReadLine();
            var second = Console.ReadLine();
            // check input is 2 integers
            if (int.TryParse(first, out int int1) && int.TryParse(second, out int int2))
            {
                Console.Write($"{int1} {calcSymbol} {int2} = ");
                //Create obj array to use as arguments when invokeing the calculator class found earlier
                object[] calcValues = new object[] { int1, int2 };
                Console.WriteLine(methodInfo.Invoke(obj, calcValues));
                return;
            }
            else
            {
                Console.WriteLine("One or more of the numbers is not an int");
                Console.WriteLine("Would you like to try again? Y/N");
                string redo = Console.ReadLine();
                Redo(redo);
                return;
            }
        End:
            Console.WriteLine("Invalid input");
            return;
        }
    }
}