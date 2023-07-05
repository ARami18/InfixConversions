using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixConvertAndEval
{
    public class InfixToPrefix
    {
        public List<string> PreFix { get; set; }

        public InfixToPrefix(List<string> infixExpr) 
        {
            PreFix = new List<string>();

            foreach (string ifx in infixExpr)
            {
                string pfx = ConvertToPreFix(ifx);
                PreFix.Add(pfx);
            }
        }

        //Method to perform conversion on each individual infix expression
        private string ConvertToPreFix(string ifx)
        {
            Stack<char> stack = new();
            string pfix = "";

            // Reverse the infix expression
            char[] reverseIfx = ifx.ToCharArray();
            Array.Reverse(reverseIfx);
            string reversedIfx = new string(reverseIfx);

            //Convert reversed infix to "nearly" postfix
            for (int i = 0; i < reversedIfx.Length; i++)
            {
                char oneChr = reversedIfx[i];

                if (IsOperand(oneChr))
                {
                    pfix += oneChr;
                }
                else if (IsOperator(oneChr))
                {
                    while (stack.Count > 0 && stack.Peek() != ')' && Precedence(stack.Peek()) > Precedence(oneChr))
                    {
                        pfix += stack.Pop();
                    }
                    stack.Push(oneChr);
                }
                else if (oneChr == ')')
                {
                    stack.Push(oneChr);
                }
                else if (oneChr == '(')
                {
                    while (stack.Count > 0 && stack.Peek() != ')')
                    {
                        pfix += stack.Pop();
                    }
                    stack.Pop();
                }
            } // End For

            while (stack.Count > 0)
            {
                pfix += stack.Pop();
            }

            // Reverse the "nearly" postfix expression to get the final prefix
            char[] reversePfx = pfix.ToCharArray();
            Array.Reverse(reversePfx);
            string result = new string(reversePfx);

            return result;

        }//END ConvertToPreFix

        //Method to evaluate if a char is operand
        private bool IsOperand(char c)
        {
            return Char.IsLetterOrDigit(c);
        }

        //Method to evaluate if a char is operand
        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }

        //Method to determine precedence
        private int Precedence(char c)
        {
            if (c == '+' || c == '-')
            {
                return 1;
            }
            else if (c == '*' || c == '/')
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }

    }
}
