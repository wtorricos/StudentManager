using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace StudentManagerBackEnd.Common.CSV
{
    public class CSVDataReader
    {
        public IEnumerable<IEnumerable<string>> Read(string path) 
        {
            return File.ReadLines(path).Select(l => l.Split(","));
        }
    }
}
