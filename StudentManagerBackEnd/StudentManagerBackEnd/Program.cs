using System;
using StudentManagerBackEnd.Application;
using StudentManagerBackEnd.Application.Students;

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
