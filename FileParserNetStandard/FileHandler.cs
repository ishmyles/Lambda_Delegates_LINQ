using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace FileParserNetStandard
{
    public class FileHandler
    {   
        public FileHandler() { }

        /// <summary>
        /// Reads a file returning each line in a list.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public List<string> ReadFile(string filePath)
        {
            //List<string> lines = new List<string>();
            List<string> lines = File.ReadLines(filePath).ToList();

            return lines;
        }
       
        /// <summary>
        /// Takes a list of a list of data.  Writes to file, using delimeter to seperate data.  Always overwrites.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delimeter"></param>
        /// <param name="rows"></param>
        public void WriteFile(string filePath, char delimeter, List<List<string>> rows)
        {
            using (var writer = new StreamWriter(filePath))
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    for (int c = 0; c < rows[i].Count; c++)
                    {
                        if (c > 0 && (long.TryParse(rows[i][c], out long dateTime)) == true)
                            writer.Write(String.Format("{0:G}", new DateTime(dateTime)));
                        else
                            writer.Write(rows[i][c]);

                        if (c != (rows[i].Count - 1))
                            writer.Write(delimeter);
                    }

                    writer.WriteLine();
                }
            }
        }
        
        /// <summary>
        /// Takes a list of strings and seperates based on delimeter.  Returns list of list of strings seperated by delimeter.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="delimeter"></param>
        /// <returns></returns>
        public List<List<string>> ParseData(List<string> data, char delimeter)
        {
            List<List<string>> parsedData = new List<List<string>>();

            foreach (var value in data)
            {
                parsedData.Add(value.Split(delimeter).ToList());
            }

            return parsedData;
        }
        
        /// <summary>
        /// Takes a list of strings and seperates on comma.  Returns list of list of strings seperated by comma.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<List<string>> ParseCsv(List<string> data)
        {
            List<List<string>> parsedData = new List<List<string>>();

            foreach (var value in data)
            {
                parsedData.Add(value.Split(',').ToList());
            }

            return parsedData;
        }
    }
}