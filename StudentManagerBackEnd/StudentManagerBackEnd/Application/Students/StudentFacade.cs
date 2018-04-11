using System;
using StudentManagerBackEnd.Common.CSV;
using StudentManagerBackEnd.Domain.Students;
using StudentManagerBackEnd.DataAccess.InMemoryRepositories;
using StudentManagerBackEnd.DataAccess;
using System.Linq;

namespace StudentManagerBackEnd.Application.Students
{
    public class StudentFacade
    {
        private readonly CSVDataReader csvDataReader;
        private readonly IRepository<Student> studentRepository;

        public StudentFacade(
            CSVDataReader csvDataReader, 
            IRepository<Student> studentRepository) 
        {
            this.csvDataReader = csvDataReader;
            this.studentRepository = studentRepository;
        }

        public void LoadData(string path) 
        {
            var data = this.csvDataReader.Read(path);
            data.Select(row => new Student(row))
                .ToList()
                .ForEach(student => this.studentRepository.Create(student));
        }

        public PaginatedResult<Student> Search(QueryParameters queryParameters) {
            return this.studentRepository.Get(queryParameters);
        }
    }
}
