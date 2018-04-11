using System;
using System.Linq;
using StudentManagerBackEnd.Common.CSV;
using Xunit;
namespace StudentManagerBackEnd.Test.CSV
{
    public class CSVDataReaderTest
    {
        [Fact]
        public void ReadLoadsTheExpectedInfoFromACSVFile() 
        {
            var sut = new CSVDataReader();

            var result = sut.Read("./resources/SmallCSVSample.csv");

            Assert.Equal(2, result.Count());

            var first = result.First().ToList();
            Assert.Equal("Kinder", first[0]);
            Assert.Equal("Leia", first[1]);
            Assert.Equal("F", first[2]);
            Assert.Equal("20151231145934", first[3]);

            var second = result.Skip(1).First().ToList();
            Assert.Equal("High", second[0]);
            Assert.Equal("Luke", second[1]);
            Assert.Equal("M", second[2]);
            Assert.Equal("20130129080903", second[3]);
        }
    }
}
