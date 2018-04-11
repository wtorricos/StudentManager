using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace StudentManagerBackEnd.Test.Utils
{
    public class CsvDataGenerator
    {
        [Fact(Skip = "Only used to manually populate data that will be used in other tests, not intended to check anything")]
        public void generateSampleCsvFileToUseInOtherTests()
        {
            var path = "./resources/MediumCSVSample.csv";
            IEnumerable<string> lines = new List<string>();
            lines = lines.Concat(this.GetNStudentsOfType(10, "Kinder"));
            lines = lines.Concat(this.GetNStudentsOfType(10, "Elementary"));
            lines = lines.Concat(this.GetNStudentsOfType(10, "High"));
            lines = lines.Concat(this.GetNStudentsOfType(10, "University"));
            System.IO.File.WriteAllLines(path, lines.ToArray());
        }


        public List<string> GetNStudentsOfType(int n, string type)
        {
            var result = new List<string>();
            for (int i = 0; i < n; i++)
            {
                for (char c = 'A'; c <= 'Z'; c++)
                {
                    string name = c.ToString() + i;
                    DateTime date = DateTime.Now;
                    string birth = string.Format(
                        "{0}{1}{2}{3}{4}{5}",
                        date.Year,
                        date.Month < 10 ? "0" + date.Month : "" + date.Month,
                        date.Day < 10 ? "0" + date.Day : "" + date.Day,
                        date.Hour < 10 ? "0" + date.Hour : "" + date.Hour,
                        date.Minute < 10 ? "0" + date.Minute : "" + date.Minute,
                        date.Second < 10 ? "0" + date.Second : "" + date.Second);
                    string gender = i % 2 == 0 ? "F" : "M";
                    var line = string.Format("{0},{1},{2},{3}", type, name, gender, birth);
                    result.Add(line);
                }
            }

            return result;
        }
    }
}
