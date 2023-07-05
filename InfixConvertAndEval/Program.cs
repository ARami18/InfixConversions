using System;
using System.Collections.Generic;
using System.Linq;

namespace InfixConvertAndEval
{
    class Program
    {
        static void Main(string[] args) 
        {
            string filepath = "C:\\Fanshawe Academic\\Project 2_INFO_5101.csv";

            Console.WriteLine("Deserializing CSV File...");
            CSVFile deserializedFile = new();
            deserializedFile.CSVDeserialization(filepath);

            //Convert deserialized csv's infix data to Postfix and Prefix
            InfixToPostfix postfixFile = new(deserializedFile.InFix);
            InfixToPrefix  prefixFile = new(deserializedFile.InFix);

            //Lists to hold evaluation values and match values for later
            List<string> evaluations = new();
            List<string> matchResults = new();

            //Print out results of conversions
            Console.WriteLine("*****************************Infix to Postfix Conversion***************");
            DisplayPostfixConversion(deserializedFile.InFix, postfixFile.PostFix);
            Console.WriteLine();

            Console.WriteLine("*****************************Infix to Prefix Conversion***************");
            DisplayPrefixConversion(deserializedFile.InFix, prefixFile.PreFix);
            Console.WriteLine();

            Console.WriteLine("*****************************Summary Report***************");
            DisplaySummaryReport(deserializedFile.InFix, prefixFile.PreFix, postfixFile.PostFix, evaluations, matchResults);
            Console.WriteLine();
            Console.WriteLine("**********************************************************");

            Console.WriteLine("Would you like to generate an XML file? (y/n)");
            string answer = Console.ReadLine();
            HandleAnswer(answer, deserializedFile.InFix, prefixFile.PreFix, postfixFile.PostFix, evaluations, matchResults);
            Console.WriteLine("***********An XML file has been generated!****************");
        }

        //******************************HELPER METHODS ************************************

        //To display postfix converstion results
        public static void DisplayPostfixConversion(List<string> infix, List<string> postfix)
        {
            Console.WriteLine("{0,-25}{1,-25}", "Infix Expression", "Postfix Expression");

            for (int i = 0; i < infix.Count; i++)
            {
                Console.WriteLine("{0,-25}|{1,-25}|", infix[i], postfix[i]);
            }
        }

        //To display postfix converstion results
        public static void DisplayPrefixConversion(List<string> infix, List<string> prefix)
        {
            Console.WriteLine("{0,-25}{1,-25}", "Infix Expression", "Prefix Expression");

            for (int i = 0; i < infix.Count; i++)
            {
                Console.WriteLine("{0,-25}|{1,-25}|", infix[i], prefix[i]);
            }
        }

        //To display the summary report and load the list for the XML values
        public static void DisplaySummaryReport(List<string> infix, List<string> prefix, List<string> postfix, List<string> evaluations, List<string> matchResults)
        {
            
            Console.WriteLine("{0,-5}{1,-23}{2,-15}{3,-15}{4,-15}{5,-15}{6,-15}", "Sno", "Infix", "Prefix", "Postfix", "Prefix Res", "Postfix Res", "Match");

            CompareExpressions comparer = new CompareExpressions();

            for (int i = 0; i < infix.Count; i++)
            {
                string prefixEval = ExpressionEvaluation.PrefixExprEvaluator(prefix[i]);
                evaluations.Add(prefixEval);
                string postfixEval = ExpressionEvaluation.PostfixExprEvaluator(postfix[i]);

                string matchresult = (comparer.Compare(prefixEval, postfixEval) == 0) ? "True" : "False";
                matchResults.Add(matchresult);

                Console.WriteLine("{0,-5}|{1,-20}|{2,-15}|{3,-15}|{4,-15}|{5,-15}|{6,-15}|", i+1, infix[i], prefix[i], postfix[i], prefixEval, postfixEval, matchresult);
            }
        }

        //To handle the user's answer on whether to generate an XML file
        public static void HandleAnswer(string answer, List<string> infix, List<string> prefix, List<string> postfix, List<string> evaluations, List<string> matchResults)
        {
            int depth = 1;

            if (answer == "y")
            {
                using StreamWriter writer = new StreamWriter("summaryreport.xml");

                //Write start of the xml file 
                writer.WriteStartDocument()
                      .WriteStartRootElement();

                //Write each element
                for (int i = 0; i < infix.Count; i++)
                {
                    writer.WriteStartElement(depth)
                          .WriteAttribute("sno", Convert.ToString(i+1), depth)
                          .WriteAttribute("infix", infix[i], depth)
                          .WriteAttribute("prefix", prefix[i], depth)
                          .WriteAttribute("postfix", postfix[i], depth)
                          .WriteAttribute("evaluation", evaluations[i], depth)
                          .WriteAttribute("comparison", matchResults[i], depth)
                          .WriteEndElement(depth);
                }
                
                //Write end of the file
                writer.WriteEndRootElement();

                //Flush buffer
                writer.Flush();
            }
            else if (answer == "n")
            {
                //Quit application
                Console.WriteLine("Goodbye!");
                return; 
            }
            else
            {
                Console.WriteLine("Invalid answer. Please enter 'y' or 'n'.");
            }
        }
    }
}
