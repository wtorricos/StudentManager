using System;
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

        private StudentFacade GetSut() {
            this.repository = new StudentRepository();
            return new StudentFacade(
                new CSVDataReader(),
                this.repository);
        }
    }
}
