using System;
using System.Collections.Generic;
using System.Diagnostics;
using FileParserNetStandard;

namespace Delegate_Exercise
{
    public class CsvHandler
    {    
        /// <summary>
        /// Takes a list of list of strings applies datahandling via dataHandler delegate and writes result as csv to writeFile.
        /// </summary>
        /// <param name="readFile"></param>
        /// <param name="writeFile"></param>
        /// <param name="dataHandler"></param>
        public void ProcessCsv(string readFile, string writeFile, Func<List<List<string>>, List<List<string>>> dataHandler)
        {
            FileHandler fh = new FileHandler();

            List<string> data = new List<string>(fh.ReadFile(readFile));
            List<List<string>> parsedCsv = fh.ParseCsv(data);

            fh.WriteFile(writeFile ,',', dataHandler(parsedCsv));
        }    
    }
}