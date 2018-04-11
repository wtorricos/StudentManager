using System;
using System.Linq;
using StudentManagerBackEnd.Application;
using StudentManagerBackEnd.Domain.Students;
using StudentManagerBackEnd.Presentation.Views;
using Xunit;

namespace StudentManagerBackEnd.Test.Presentation.Views
{
    public class ConsoleStudentViewIntegrationTest
    {
        [Fact]
        public void ConsoleViewProcesTheArgumentsCorrectly() 
        {
            string[] args = { "./resources/SmallCSVSample.csv", "name=leia" };
            var bootstrap = new Bootstrap();
            var sut = bootstrap.StudentView;

            sut.ProcessArgs(bootstrap.StudentController, args);

            Assert.Equal(1, sut.Students.Count());
            Assert.Equal("Leia", sut.Students.First().Name);
        }

        [Fact]
        public void SearchByNameSortedAlphabetically()
        {
            //MediumCSVSample has 10 rows for every letter times the types(4) which
            //gives a total of 40 rows for the letter a
            string[] args = { "./resources/MediumCSVSample.csv", "name=a" };
            var bootstrap = new Bootstrap();
            var sut = bootstrap.StudentView;

            sut.ProcessArgs(bootstrap.StudentController, args);

            Assert.Equal(40, sut.Students.Count());
            //check first 4 are correct
            Assert.Equal("A0", sut.Students[0].Name);
            Assert.Equal("A0", sut.Students[1].Name);
            Assert.Equal("A0", sut.Students[2].Name);
            Assert.Equal("A0", sut.Students[3].Name);

            //check the sequence
            Assert.Equal("A1", sut.Students[4].Name);

            //check last 4 are correct
            Assert.Equal("A9", sut.Students[36].Name);
            Assert.Equal("A9", sut.Students[37].Name);
            Assert.Equal("A9", sut.Students[38].Name);
            Assert.Equal("A9", sut.Students[39].Name);
        }

        [Fact]
        public void SearchByStudentTypeSortByDate()
        {
            string[] args = { "./resources/MediumCSVSample.csv", "type=university" };
            var bootstrap = new Bootstrap();
            var sut = bootstrap.StudentView;

            sut.ProcessArgs(bootstrap.StudentController, args);

            Assert.Equal(260, sut.Students.Count());
            //Z9 birth was modified to be the most recent
            Assert.Equal("Z9", sut.Students[0].Name);
        }

        [Fact]
        public void SearchByGenderAndTypeAndSortingByDate()
        {
            string[] args = { "./resources/MediumCSVSample.csv", "type=university", "gender=male" };
            var bootstrap = new Bootstrap();
            var sut = bootstrap.StudentView;

            sut.ProcessArgs(bootstrap.StudentController, args);

            //It should be half the results than in the previous test
            Assert.Equal(130, sut.Students.Count());
            //Z9 birth was modified to be the most recent
            Assert.Equal("Z9", sut.Students[0].Name);
            Assert.True(sut.Students.All(s => s.Gender == Gender.MALE));
        }
    }
}
