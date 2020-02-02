using System;

namespace Calculator
{
    public class FormulaParser
    {
        private const string SPECIALS = "+-/*%()";
        private const string INVALID_FORMULA = "Invalid formula";

        public double Perform(string formula)
        {
            // Numbers, operators and parenthesis are all separated by SPACE            
            if (!ValidateFormat(formula))
            {
                throw new ArgumentException(INVALID_FORMULA);
            }

            var result = ProcessFormula(formula);
            if (!string.IsNullOrEmpty(result) && double.TryParse(result, out double finalResult))
            {
                return finalResult;
            }
            else
            {
                throw new ArgumentException(INVALID_FORMULA);
            }
        }

        private bool ValidateFormat(string formula)
        {
            var parts = formula.Split(" ");

            // Basic validation
            if (parts.Length < 3)
            {
                return false;
            }

            var openParenthesisCount = 0;
            var closeParenthesisCount = 0;

            foreach (var part in parts)
            {
                // Make sure only numbers and the defined operators
                if (double.TryParse(part, out _) || SPECIALS.Contains(part))
                {
                    if (part.Equals("("))
                    {
                        openParenthesisCount++;
                    }
                    else if (part.Equals(")"))
                    {
                        closeParenthesisCount++;
                    }

                    continue;
                }
                else
                {
                    return false;
                }
            }

            // Make sure there is an equal opening and closing parenthesis (, )
            if (openParenthesisCount != closeParenthesisCount)
            {
                return false;
            }

            return true;
        }

        private string ProcessFormula(string formula)
        {
            // Search and process the formula based on precedence with Parenthesis
            int openParenthesis = -1;
            int closeParenthesis = -1;

            var chars = formula.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == '(' && closeParenthesis == -1)
                {
                    openParenthesis = i;
                }
                else if (chars[i] == ')' && openParenthesis != -1)
                {
                    // First proper operation found
                    closeParenthesis = i;
                    break;
                }
                else if (chars[i] == ')' && openParenthesis == -1)
                {
                    throw new ArgumentException(INVALID_FORMULA);
                }
            }

            if (openParenthesis != -1 && closeParenthesis != -1)
            {
                var subFormula = formula.Substring(openParenthesis + 1, closeParenthesis - openParenthesis - 1);
                subFormula = subFormula.Trim();

                var parts = subFormula.Split(" ");
                var result = ProcessArithmeticByParts(parts);

                formula = formula.Replace(string.Format("( {0} )", subFormula), result);
                return ProcessFormula(formula);
            }
            else if (openParenthesis == -1 && closeParenthesis == -1)
            {
                // No parenthesis, process by precedence
                formula = formula.Trim();

                var parts = formula.Split(" ");
                return ProcessArithmeticByParts(parts);
            }
            else
            {
                throw new ArgumentException(INVALID_FORMULA);
            }
        }

        private string ProcessArithmeticByParts(string[] parts)
        {
            if (parts.Length < 3)
            {
                throw new ArgumentException(INVALID_FORMULA);
            }

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "*" || parts[i] == "/" || parts[i] == "%")
                {
                    // Multiplication or Division
                    if (i != 0 && i != parts.Length - 1)
                    {
                        if (double.TryParse(parts[i - 1], out double first) && double.TryParse(parts[i + 1], out double second))
                        {
                            double result = 0;
                            if (parts[i] == "*")
                            {
                                result = Multiply(first, second);
                            }
                            else if (parts[i] == "/")
                            {
                                result = Divide(first, second);
                            }
                            else if (parts[i] == "%")
                            {
                                result = Modulus(first, second);
                            }

                            string[] newParts = new string[parts.Length - 2];
                            Array.Copy(parts, 0, newParts, 0, i - 1);                            
                            newParts[i - 1] = result.ToString();
                            if (i + 1 < parts.Length - 1)
                            {
                                Array.Copy(parts, i + 2, newParts, i, newParts.Length - i);
                            }

                            if (newParts.Length == 1)
                            {
                                return newParts[0];
                            }
                            else if (newParts.Length >= 3)
                            {
                                return ProcessArithmeticByParts(newParts);
                            }
                        }
                    }

                    throw new ArgumentException(INVALID_FORMULA);
                }
            }

            for (int j = 0; j < parts.Length; j++)
            {
                if (parts[j] == "+" || parts[j] == "-")
                {
                    // Add and substraction
                    if (j != 0 && j != parts.Length - 1)
                    {
                        if (double.TryParse(parts[j - 1], out double first) && double.TryParse(parts[j + 1], out double second))
                        {
                            double result = 0;
                            if (parts[j] == "+")
                            {
                                result = Add(first, second);
                            }
                            else if (parts[j] == "-")
                            {
                                result = Substract(first, second);
                            }

                            string[] newParts = new string[parts.Length - 2];
                            Array.Copy(parts, 0, newParts, 0, j - 1);
                            newParts[j - 1] = result.ToString();
                            if (j + 1 < parts.Length - 1)
                            {
                                Array.Copy(parts, j + 2, newParts, j, newParts.Length - j);
                            }

                            if (newParts.Length == 1)
                            {
                                return newParts[0];
                            }
                            else if (newParts.Length >= 3)
                            {
                                return ProcessArithmeticByParts(newParts);
                            }
                        }
                    }

                    throw new ArgumentException(INVALID_FORMULA);
                }
            }

            throw new ArgumentException(INVALID_FORMULA);
        }

        private double Add(double first, double second)
        {
            return first + second;
        }

        private double Substract(double first, double second)
        {
            return first - second;
        }

        private double Divide(double first, double second)
        {
            return first / second;
        }

        private double Multiply(double first, double second)
        {
            return first * second;
        }

        private double Modulus(double first, double second)
        {
            return first % second;
        }
    }
}
