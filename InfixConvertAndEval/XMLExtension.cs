using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixConvertAndEval
{
    //Class that writes out XML file using StreamWriter extension methods
    public static class XMLExtension
    {
        public static StreamWriter WriteStartDocument(this StreamWriter writer)
        {
            writer.Write("<?xml version=\"1.0\" encoding=\"UTF-8\" ?>\n");
            return writer;  
        }

        public static StreamWriter WriteStartRootElement(this StreamWriter writer)
        {
            writer.Write("<root>\n");
            return writer;
        }

        public static StreamWriter WriteEndRootElement(this StreamWriter writer)
        {
            writer.Write("</root>");
            return writer;
        }

        public static StreamWriter WriteStartElement(this StreamWriter writer, int depth)
        {
            writer.Write(new string(' ', depth * 4));
            writer.Write("<element>\n");
            return writer;
        }

        public static StreamWriter WriteEndElement(this StreamWriter writer, int depth)
        {
            writer.Write(new string(' ', depth * 4));
            writer.Write("</element>\n");
            return writer;
        }

        public static StreamWriter WriteAttribute(this StreamWriter writer, string attrName, string value, int depth)
        {
            writer.Write(new string(' ', depth * 8));
            writer.Write($"<{attrName}>{value}</{attrName}>\n");
            return writer;
        }

    }
}
