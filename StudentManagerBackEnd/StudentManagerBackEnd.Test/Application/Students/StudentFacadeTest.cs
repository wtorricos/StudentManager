using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagerBackEnd.Application.Students;
using StudentManagerBackEnd.Common.CSV;
using StudentManagerBackEnd.DataAccess;
using StudentManagerBackEnd.DataAccess.InMemoryRepositories;
using Xunit;

namespace StudentManagerBackEnd.Test.Application.Students
{
    public class StudentFacadeTest
    {
        private StudentRepository repository;

        [Theory]
        [InlineData("SmallCSVSample.csv", 2)]
        public void LoadCreatesAnStudentForEachRowOfTheCSV(string fileName, int expectedItems) 
        {
            var sut = this.GetSut();

            sut.LoadData("./resources/" + fileName);

            var loadedData = this.repository.Get(new QueryParameters(1, 5));

            Assert.Equal(expectedItems, loadedData.Total);

        }

        [Fact]
        public void SearchByNameReturnsTheExpectedResults() 
        {
            var sut = this.GetSut();    
            sut.LoadData("./resources/SmallCSVSample.csv");

            var result = sut.Search(
                new QueryParameters(
                    page: 1,
                    size: 5,
                    sortingFields: new List<string> { "name" }));

            Assert.Equal(2, result.Total);
            Assert.Equal("Leia", result.ToList()[0].Name);
            Assert.Equal("Luke", result.ToList()[1].Name);
        }

        private StudentFacade GetSut() {
            this.repository = new StudentRepository();
            return new StudentFacade(
                new CSVDataReader(),
                this.repository);
        }
    }
}
