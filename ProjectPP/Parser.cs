using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectPP
{
    public class Parser
    {
        public static string ConvertToPostfix(string expression)
        {
            string postfix = "";
            Stack<char> stack = new Stack<char>();

            foreach (char c in expression)
            {
                if (char.IsDigit(c))
                {
                    postfix += c;
                }
                else if (IsOperator(c))
                {
                    while (stack.Count > 0 && IsOperator(stack.Peek()) && OperatorPriority(c) <= OperatorPriority(stack.Peek()))
                    {
                        postfix += stack.Pop();
                    }
                    stack.Push(c);
                }
                else if (c == '(')
                {
                    stack.Push(c);
                }
                else if (c == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop();
                }
            }

            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }

            return postfix;
        }

        public static double EvaluatePostfix(string postfixExpression)
        {
            Stack<double> stack = new Stack<double>();

            foreach (char c in postfixExpression)
            {
                if (char.IsDigit(c))
                {
                    stack.Push(double.Parse(c.ToString()));
                }
                else if (IsOperator(c))
                {
                    double operand2 = stack.Pop();
                    double operand1 = stack.Pop();
                    double result = PerformOperation(c, operand1, operand2);
                    stack.Push(result);
                }
            }

            return stack.Pop();
        }

        static bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        static int OperatorPriority(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return 0;
            }
        }

        static double PerformOperation(char operation, double operand1, double operand2)
        {
            switch (operation)
            {
                case '+':
                    return operand1 + operand2;
                case '-':
                    return operand1 - operand2;
                case '*':
                    return operand1 * operand2;
                case '/':
                    return operand1 / operand2;
                default:
                    return 0;
            }
        }
    }
}
