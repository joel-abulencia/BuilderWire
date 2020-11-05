using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace BuilderWireCodeExam
{
    public class TextManipulator
    {
        public string ProcessOutput(string refArticle, string refWords, string abbrev)
        {
            // Generate constant abbreviations to be used as reference
            var abbreviations = GetAbbreviations(abbrev);
            refArticle = refArticle.ToLower();
            // Find and replace abbreviations within the article so as not to be confused with sentence separators
            foreach (string abb in abbreviations)
            {
                if (refArticle.Contains(abb))
                {
                    var replaceValue = abb.Replace('.', '@');
                    refArticle = refArticle.Replace(abb, replaceValue);
                }
            }
            // Derive a list from the Words input file to simplify processing
            var wordsArray = SplitToWords(refWords);
            // Derive a list from the Article input file containing the diff sentences to simplify processing and indexing
            var sentencesArray = SplitToSentences(refArticle);

            var outputList = new List<string>();
            // Retrieve an alphabet list pertaining to the number of repititions the letters in the alphabet will repeat
            var alphabets = GetAlphabetList(wordsArray.Count());
            // Start processing and Generating the output
            for( int j = 0; j <  wordsArray.Count(); j++)
            {
                int instancesCount = 0;
                string instancesSentencesNumber = "";
                for (int i = 0; i < sentencesArray.Count(); i++)
                {
                    // For the sentences derived from the Article, separate the individual words
                    var wordsInSentence = SplitFromSentencesToWords(sentencesArray[i]);
                    foreach (string perWord in wordsInSentence)
                    {
                        if (String.Equals(RemoveSpecialCharacters(wordsArray[j]), RemoveSpecialCharacters(perWord), StringComparison.OrdinalIgnoreCase))
                        {
                            instancesCount += 1;
                            instancesSentencesNumber += string.IsNullOrEmpty(instancesSentencesNumber) ? (i + 1).ToString() : "," + (i + 1).ToString();
                        }
                    }
                }
                var wordReference = " " + alphabets[j] + ". " + wordsArray[j] + " {" + instancesCount.ToString() + ":" + instancesSentencesNumber + "}";
                outputList.Add(wordReference);
            }
            var result = string.Join(Environment.NewLine, outputList);
            return result;
        }
        private string[] SplitToWords(string rawWords)
        {
            string[] stringArray = rawWords.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            return stringArray;
        }
        private string[] SplitToSentences(string rawArticle)
        {
            string[] stringArray = rawArticle.Split('.');
            return stringArray;
        }
        private string[] SplitFromSentencesToWords(string sentence)
        {
            string[] stringArray = sentence.Split(' ');
            return stringArray;
        }
        private List<string> GetAlphabetList(int wordsCount)
        {
            var list = new List<string>();
            var alphabetIterations = (wordsCount / 26) + 1;
            string alphabet = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < alphabetIterations; i++)
            {
                foreach (char c in alphabet)
                {
                    var addAlpha = c.ToString();
                    if (i != 0)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            addAlpha = addAlpha + c.ToString();
                        }
                    }
                    list.Add(addAlpha);
                }
            }
            return list;
        }
        private List<string> GetAbbreviations(string abb)
        {
            var list = abb.Split(',').ToList();
            return list;
        }
        private static string RemoveSpecialCharacters(string str)
        {
            return Regex.Replace(str, @"[^\w\d\s]", "");
        }
    }
}
