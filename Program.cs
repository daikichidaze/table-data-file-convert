using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ChatbotTestFileconversion
{
    class Program
    {
        static string strCurrentDirectry = System.IO.Directory.GetCurrentDirectory();

        static void Main(string[] args)
        {
            string inputFileName;
            string outputFileName;
            string inputFileExtention;
            string outputFileExtention;
            
            
            try
            {
                inputFileName = args[0];
                outputFileName = args[1];

                inputFileExtention = inputFileName.Split('.').Last();
                outputFileExtention = outputFileName.Split('.').Last();
            }
            catch (ArgumentException ex)
            {
                System.Console.WriteLine(ex);
                throw;
            }

            string inputSplitter = SelectSplitter(inputFileExtention);
            string outputSplitter = SelectSplitter(outputFileExtention);

            ConvertTableData(inputFileName, outputFileName, inputSplitter, outputSplitter);
        }

        static string SelectSplitter(string argFileExtention)
        {
            string splitter;
            switch(argFileExtention)
            {
                case "csv":
                    splitter = ",";
                    break;
                case "tsv":
                    splitter = "\t";
                    break;
                default:
                    throw new ArgumentException("File extention name is worng");
            }
            return splitter;
        }
        static void ConvertTableData(string argInputFileName, string argOutputFileName, string argInputSplitter, string argOutputSplitter)
        {
            List<string[]> contents = ReadTableFile(argInputFileName, argInputSplitter);
            WriteTableFile(contents, argOutputFileName,argOutputSplitter);
        }
        static List<string[]> ReadTableFile(string argInputFileName, string argParserCharacter)
        {
            var tableContents = new List<string[]>();

            try
            {
                StreamReader reader = new StreamReader(Path.Combine(strCurrentDirectry, argInputFileName), Encoding.GetEncoding("UTF-8"));
                while (reader.Peek() >= 0) 
                {
                    string[] cols = reader.ReadLine().Split(argParserCharacter);
                    tableContents.Add(cols);
	            }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(Path.Combine(strCurrentDirectry, argInputFileName));
                System.Console.WriteLine(ex);
                throw;
            }
            return tableContents;
        }
        static void WriteTableFile(List<string[]> argTableContents, string argOutputFileName, string argParserCharacter)
        {
            try
            {
                using (var sw = new StreamWriter(argOutputFileName, false, System.Text.Encoding.UTF8))
                {
                    foreach (var content in argTableContents)
                    {
                        sw.WriteLine(String.Join(argParserCharacter, content));
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(argOutputFileName);
                System.Console.WriteLine(ex);
                throw;
            }

        }
    }
}
