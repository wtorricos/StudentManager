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

        public PaginatedResult<Student> Get(QueryParameters queryParameters)
        {
            var result = this.FilterStudents(queryParameters, this.students);
            result = this.SortStudents(queryParameters, result);
            int total = result.Count();
            result = FilterByPageAndSize(queryParameters, result);
            return new PaginatedResult<Student>(queryParameters.Page, total, result);
        }

        private IEnumerable<Student> FilterStudents(QueryParameters queryParameters, IEnumerable<Student> result)
        {
            if (queryParameters.FilteringFields.Count() > 0)
            {
                foreach (var filteringField in queryParameters.FilteringFields)
                {
                    if (filteringField.Key == "studentType")
                    {
                        result = result.Where(s => s.StudentType == filteringField.Value);
                    }
                    else if (filteringField.Key == "name")
                    {
                        result = result.Where(s => s.Name.Contains(filteringField.Value));
                    }
                    else if (filteringField.Key == "gender")
                    {
                        result = result.Where(s => s.Gender == filteringField.Value);
                    }
                    else
                    {
                        throw new NotSupportedException("For the moment only filtering by gender or name is supported");
                    }
                }
            }

            return result;
        }

        private IEnumerable<Student> SortStudents(QueryParameters queryParameters, IEnumerable<Student> result) {
            if (queryParameters.SortingFields.Count() > 0)
            {
                foreach (var sortingField in queryParameters.SortingFields)
                {
                    if (sortingField == "birth")
                    {
                        result = result.OrderBy(student => student.Birth);
                    }
                    else if (sortingField == "name")
                    {
                        result = result.OrderBy(student => student.Name);
                    }
                    else
                    {
                        throw new NotSupportedException("For the moment only sorting by birth or name is supported");
                    }
                }
            }

            return result;
        }

        private List<Student> FilterByPageAndSize(QueryParameters queryParameters, IEnumerable<Student> result)
        {
            return result
                .Skip((queryParameters.Page - 1) * queryParameters.Size)
                .Take(queryParameters.Size)
                .ToList();
        }
    }
}
