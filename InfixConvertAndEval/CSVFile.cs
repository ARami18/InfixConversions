

namespace InfixConvertAndEval
{
    public class CSVFile
    {

        //Infix expressions stored in string List
        public List<string> InFix { get; set; }
        public List<string> SerialNums { get; set; }

        //De-serialize CSV input to C# List named InFix
        public void CSVDeserialization(string filepath)
        {
            if (Path.GetExtension(filepath) != ".csv")
                throw new FileLoadException("File provided is not a CSV file...");


            InFix = new List<string>();
            SerialNums = new List<string>();

            using (var reader = new StreamReader(filepath))
            {
                string ln;
                while ((ln = reader.ReadLine()) != null)
                {
                    string[] vals = ln.Split(',');
                    SerialNums.Add(vals[0]);
                    InFix.Add(vals[1]);              
                }

                SerialNums.RemoveAt(0);
                InFix.RemoveAt(0);
            }
        }
    }
}
