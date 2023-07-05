using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixConvertAndEval
{
    public class CompareExpressions : IComparer<string>
    {

        public int Compare(string postfix, string prefix)
        {

            double resultX, resultY;

            // Attempt to parse the string result of the evaluated postfix expression to a double
            if (!double.TryParse(postfix, out resultX))
            {
                throw new ArgumentException("Invalid input string for postfix expression.");
            }

            // Attempt to parse the string result of the evaluated prefix expression to a double
            if (!double.TryParse(prefix, out resultY))
            {
                throw new ArgumentException("Invalid input string for prefix expression.");
            }

            // Compare the results of the evaluated postfix and prefix expressions 
            if (resultX == resultY)
            {
                return 0; // Equal
            }
            else if (resultX < resultY)
            {
                return -1; // Less than
            }
            else
            {
                return 1; // Greater than
            }
        }
    }
}
