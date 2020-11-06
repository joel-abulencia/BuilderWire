using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuilderWireCodeExam;
using System.Configuration;

namespace UnitTest
{
    [TestClass]
    public class TextManipulatorTest
    {
        [TestMethod]
        public void Test_ProcessOutput()
        {
            string expectedResult = System.IO.File.ReadAllText(@"C:\Users\joela\OneDrive\Documents\Desktop\IT Shit\BuilderWire\Output\Output.txt");
            string refArticle = System.IO.File.ReadAllText(@"C:\Users\joela\OneDrive\Documents\Desktop\IT Shit\BuilderWire\Input\Article.txt");
            string refWords = System.IO.File.ReadAllText(@"C:\Users\joela\OneDrive\Documents\Desktop\IT Shit\BuilderWire\Input\Words.txt");
            string abbrev = "mr.,mrs.,e.g.";

            var textManipulatorTest = new TextManipulator();
            var actualResult = textManipulatorTest.ProcessOutput(refArticle, refWords, abbrev);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
