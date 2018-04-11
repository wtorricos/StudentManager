using System;
using System.Linq;
using StudentManagerBackEnd.Application;
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
    }
}
