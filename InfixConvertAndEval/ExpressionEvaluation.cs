using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace InfixConvertAndEval
{
    public class ExpressionEvaluation
    {
        
        //Method to evaluate one PREFIX expression
        //Using an expression tree
        public static string PrefixExprEvaluator(string prefix)
        {
            //String containing result as a string
            string result;

            Stack<Expression> stack = new();
            string[] tokens = prefix.Select(c => c.ToString()).ToArray();

            //Iterate in reverse
            for (int i = tokens.Length - 1; i >= 0; i--)
            {
                //Check if current token is number
                if (double.TryParse(tokens[i], out double number))
                {
                    stack.Push(Expression.Constant(number));
                }
                else
                {
                    //If not, pop two
                    //And construct a binary expr based on the operator token
                    //Then push result back on the stack
                    Expression rightside = stack.Pop();
                    Expression leftside = stack.Pop();

                    switch (tokens[i])
                    {
                        case "+":
                            stack.Push(Expression.Add(leftside, rightside));
                            break;
                        case "-":
                            stack.Push(Expression.Subtract(rightside, leftside));
                            break;
                        case "*":
                            stack.Push(Expression.Multiply(leftside, rightside));
                            break;
                        case "/":
                            stack.Push(Expression.Divide(rightside, leftside));
                            break;
                        default:
                            throw new ArgumentException($"Invalid operator {tokens[i]}");
                    }
                }
            }

            //Evaluate compile, and invoke the resulting expression
            //Then add to the results list as a string    
            double res = Expression.Lambda<Func<double>>(stack.Pop()).Compile()();
            res = Math.Round(res, 1);
            result = Convert.ToString(res);           

            return result;
        }



        //Method to evaluate one POSTFIX expression
        //Using an expression tree
        public static string PostfixExprEvaluator(string postfix)
        {
            //String containing result as a string
            string result;

            Stack<Expression> stack = new();
            string[] tokens = postfix.Select(c => c.ToString()).ToArray();
            

            //Iterate in order
            foreach (string token in tokens)
            {
                //Check if current token is number
                if (double.TryParse(token, out double number))
                {
                    stack.Push(Expression.Constant(number));
                }
                else
                {
                    //If not a number and stack empty, pop two
                    //And construct a binary exp based on the operator token
                    //Then push result back on the stack
                    if (stack.Count < 2)
                    {
                        throw new ArgumentException($"Invalid expression: {postfix}");
                    }

                    Expression leftside = stack.Pop();
                    Expression rightside = stack.Pop();                   

                    switch (token)
                    {
                        case "+":
                            stack.Push(Expression.Add(leftside, rightside));
                            break;
                        case "-":
                            stack.Push(Expression.Subtract(rightside, leftside));
                            break;
                        case "*":
                            stack.Push(Expression.Multiply(leftside, rightside));
                            break;
                        case "/":
                            stack.Push(Expression.Divide(rightside, leftside));
                            break;
                        default:
                            throw new ArgumentException($"Invalid operator {token}");
                    }

                }
            }//End For

            if (stack.Count != 1)
            {
                throw new ArgumentException($"Invalid expression: {postfix}");
            }

            //Evaluate compile, and invoke the resulting expression
            //Then add to the results list as a string    
            double res = Expression.Lambda<Func<double>>(stack.Pop()).Compile()();
            res = Math.Round(res, 1);
            result = Convert.ToString(res);

            return result;
        }


    }
}
