using System;
using StudentManagerBackEnd.Application.Students;
using StudentManagerBackEnd.Common.CSV;
using StudentManagerBackEnd.DataAccess.InMemoryRepositories;

namespace StudentManagerBackEnd.Application
{
    public class Bootstrap
    {
        public Bootstrap()
        {
            this.StudentFacade = new StudentFacade(
                new CSVDataReader(),
                new StudentRepository()); 
        }

        public StudentFacade StudentFacade { get; }
    }
}
