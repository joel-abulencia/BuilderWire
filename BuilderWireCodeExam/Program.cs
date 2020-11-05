using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Configuration;

namespace BuilderWireCodeExam
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string articleTextPath = ConfigurationManager.AppSettings["Article"];
            string wordsTextPath = ConfigurationManager.AppSettings["Words"];
            string outputTextPath = ConfigurationManager.AppSettings["Output"];
            string abbreviationsText = ConfigurationManager.AppSettings["Abbreviations"];
            //Generate input txt files
            string article = System.IO.File.ReadAllText(@articleTextPath);
            string words = System.IO.File.ReadAllText(@wordsTextPath);
            string abbreviations = abbreviationsText;
            var _processor = new TextManipulator();
            var output = _processor.ProcessOutput(article, words, abbreviations);
            System.IO.File.WriteAllText(outputTextPath, output);
        }
    }
}
