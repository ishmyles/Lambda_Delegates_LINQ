using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using ObjectLibrary;

namespace FileParserNetStandard {
    
    //public class Person { }  // temp class delete this when Person is referenced from dll
    
    public class PersonHandler {
        public List<Person> People;

        /// <summary>
        /// Converts List of list of strings into Person objects for People attribute.
        /// </summary>
        /// <param name="people"></param>
        public PersonHandler(List<List<string>> people)
        {
            People = new List<Person>();

            foreach (var value in people.Skip(1))
            {
                People.Add(new Person(
                        int.Parse(value[0]),
                        value[1],
                        value[2],
                        DateTime.Parse(value[3])
                    ));
            }
            #region Alternative Solution
            //People = new List<Person>();

            //for (int i = 1; i < people.Count; i++)
            //{
            //    People[(i - 1)] = new Person
            //        (
            //            int.Parse(people[(i - 1)][0]),
            //            people[(i - 1)][1],
            //            people[(i - 1)][2],
            //            DateTime.Parse(people[(i - 1)][3])
            //        );
            //}
            #endregion
        }

        /// <summary>
        /// Gets oldest people
        /// </summary>
        /// <returns></returns>
        public List<Person> GetOldest()
        {
            DateTime oldest = People.Select(p => p.Dob).Min();

            return People.Where(p => p.Dob == oldest).ToList();
        }

        /// <summary>
        /// Gets string (from ToString) of Person from given Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetString(int id)
        {
            return People.Find(p => p.Id == id).ToString();
        }

        public List<Person> GetOrderBySurname()
        {
            return People.OrderBy(p => p.Surname).ToList();
        }

        /// <summary>
        /// Returns number of people with surname starting with a given string.  Allows case-sensitive and case-insensitive searches
        /// </summary>
        /// <param name="searchTerm"></param>
        /// <param name="caseSensitive"></param>
        /// <returns></returns>
        public int GetNumSurnameBegins(string searchTerm, bool caseSensitive)
        {
            return People.Select(p => p.Surname)
                .Where(s => s.StartsWith(searchTerm, !caseSensitive, null))
                .Count();
        }
        
        /// <summary>
        /// Returns a string with date and number of people with that date of birth.  Two values seperated by a tab.  Results ordered by date.
        /// </summary>
        /// <returns></returns>
        public List<string> GetAmountBornOnEachDate()
        {
            List<string> result = new List<string>();

            People.OrderBy(p => p.Dob)
                .GroupBy(g => g.Dob).ToList()
                .ForEach(i => result.Add($"{i.Key}\t{i.Count()}"));

            return result;
        }
    }
}