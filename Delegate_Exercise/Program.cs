using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using FileParserNetStandard;

public delegate List<List<string>> Parser(List<List<string>> data);

namespace Delegate_Exercise
{
    internal class Delegate_Exercise
    {
        public static void Main(string[] args)
        {
            string _readFilePath = "/Users/ishmyles/Desktop/Dip-Seminar-Delegates-Lambda-Linq_Exercises-master/data.csv";
            string _writeFilePath = "/Users/ishmyles/Desktop/Dip-Seminar-Delegates-Lambda-Linq_Exercises-master/processed_data.csv";
            DataParser dp = new DataParser();
            CsvHandler ch = new CsvHandler();

            Func<List<List<string>>, List<List<string>>> pDataHandler = dp.StripQuotes;
            pDataHandler += dp.StripWhiteSpace;
            pDataHandler += StripHashTag;

            ch.ProcessCsv(_readFilePath, _writeFilePath, pDataHandler);
        }

        public static List<List<string>> StripHashTag(List<List<string>> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                for (int c = 0; c < data[i].Count; c++)
                {
                    data[i][c] = data[i][c].Trim('#');
                }
            }

            return data;
        }
    }
}