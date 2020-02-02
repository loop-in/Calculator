using System;

namespace Calculator
{
    class Program
    {
        static void Main()
        {
            bool escape = false;

            while (!escape)
            {
                Console.WriteLine("Insert the formula:");
                var formula = Console.ReadLine();

                try
                {
                    var result = Calculate(formula);
                    Console.WriteLine(string.Format("Result: {0}", result));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Please try again. Error: {0}.", ex.Message);
                }
                finally
                {
                    Console.WriteLine("Press ESC to exit or any key to continue.");
                }

                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Escape)
                {
                    escape = true;
                }
            }
        }

        public static double Calculate(string formula)
        {
            var parser = new FormulaParser();
            return parser.Perform(formula);
        }
    }
}
