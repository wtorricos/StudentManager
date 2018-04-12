using System;
using StudentManagerBackEnd.Application;
using StudentManagerBackEnd.Application.Students;

namespace StudentManagerBackEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var bootstrap = new Bootstrap();
            var sut = bootstrap.StudentView;

            sut.ProcessArgs(bootstrap.StudentController, args);
            sut.DisplayStudents();
        }
    }
}
