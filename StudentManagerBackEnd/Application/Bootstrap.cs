using System;
using StudentManagerBackEnd.Application.Student;

namespace StudentManagerBackEnd.Application
{
    public class Bootstrap
    {
        public Bootstrap()
        {
            this.StudentFacade = new StudentFacade(); 
        }

        public StudentFacade StudentFacade { get; }
    }
}
