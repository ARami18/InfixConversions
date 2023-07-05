using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace InfixConvertAndEval
{
    public class InfixToPostfix
    {
        public List<string> PostFix { get; set; }

        //Class constructor
        public InfixToPostfix(List<string> infixExpr) 
        {
            PostFix = new List<string>();

            foreach(string ifx in infixExpr)
            {
                string pfx = ConvertToPostFix(ifx);
                PostFix.Add(pfx);
            }
        }

        //Method to perform conversion on each individual infix expression
        private string ConvertToPostFix(string ifx)
        {
            Stack<char> stack = new();
            string postfix = "";

            for (int i = 0; i < ifx.Length; i++) 
            {
                char oneChr = ifx[i];

                if (IsOperand(oneChr))
                {
                    postfix += oneChr;
                }
                else if (IsOperator(oneChr))
                {
                    while (stack.Count > 0 && stack.Peek() != '(' && Precedence(stack.Peek()) >= Precedence(oneChr))
                    {
                        postfix += stack.Pop();
                    }
                    stack.Push(oneChr);
                }
                else if (oneChr == '(')
                {
                    stack.Push(oneChr);
                }
                else if (oneChr == ')')
                {
                    while (stack.Count > 0 && stack.Peek() != '(')
                    {
                        postfix += stack.Pop();
                    }
                    stack.Pop();
                }
            }// End For

            while (stack.Count > 0)
            {
                postfix += stack.Pop();
            }

            return postfix;

        }//END ConvertToPostFix

        
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
