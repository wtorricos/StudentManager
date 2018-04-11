using System;
using StudentManagerBackEnd.Application.Students;
using StudentManagerBackEnd.Common.CSV;
using StudentManagerBackEnd.DataAccess.InMemoryRepositories;
using StudentManagerBackEnd.Presentation.ModelView;
using StudentManagerBackEnd.Presentation.Views;

namespace StudentManagerBackEnd.Application
{
    public class Bootstrap
    {
        public ConsoleStudentView StudentView { get; }

        public StudentController StudentController { get; }

        public Bootstrap()
        {
            var studentFacade = new StudentFacade(
                new CSVDataReader(),
                new StudentRepository());

            this.StudentView = new ConsoleStudentView();
            this.StudentController = new StudentController(this.StudentView, studentFacade);
        }
    }
}
