using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagerBackEnd.Domain.Students;


namespace StudentManagerBackEnd.DataAccess.InMemoryRepositories
{
    public class StudentRepository : IRepository<Student>
    {
        private List<Student> students;

        public StudentRepository()
        {
            this.students = new List<Student>();
        }

        public Student Create(Student student)
        {
            student.Id = Guid.NewGuid();
            this.students.Add(student);

            return student;
        }

        public Student Get(Guid id)
        {
            return this.students.Where(student => student.Id == id).FirstOrDefault();
        }

        public void Delete(Guid id)
        {
            this.students = this.students.Where(student => student.Id != id).ToList();
        }

        public PaginatedResult<Student> Get(int page, int size)
        {
            var result = this.students.Skip((page - 1) * size).Take(size).ToList();
            return new PaginatedResult<Student>(page, this.students.Count, result);
        }
    }
}
