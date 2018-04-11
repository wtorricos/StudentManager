using System;
using StudentManagerBackEnd.Application;
using StudentManagerBackEnd.Application.Student;

namespace StudentManagerBackEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            StudentFacade studentFacade = new Bootstrap().StudentFacade;
        }
    }
}
