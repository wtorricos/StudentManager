using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagerBackEnd.DataAccess.InMemoryRepositories;
using StudentManagerBackEnd.Domain.Students;
using Xunit;

namespace StudentManagerBackEnd.Test.DataAccess.InMemoryRepository
{
    public class StudentRepositoryTest
    {
        [Fact]
        public void CreateAssignsAnIdToTheNewStudent() 
        {
            var sut = new StudentRepository();
            var student = new Student
            {
                Name = "Jhon",
                Age = 12
            };

            Assert.Equal(Guid.Empty, student.Id);

            var actual = sut.Create(student);

            Assert.NotEqual(Guid.Empty, student.Id);
        }

        [Fact]
        public void GetReturnsTheExpectedStudent()
        {
            var sut = new StudentRepository();
            var student = sut.Create(
                new Student
                {
                    Name = "Jhon",
                    Age = 12
                });

            var actual = sut.Get(student.Id);

            Assert.Equal(student.Id, actual.Id);
            Assert.Equal(student.Name, actual.Name);
            Assert.Equal(student.Age, actual.Age);
        }

        [Fact]
        public void DeleteRemovesTheStudent()
        {
            var sut = new StudentRepository();
            var student = sut.Create(
                new Student
                {
                    Name = "Jhon",
                    Age = 12
                });

            sut.Delete(student.Id);

            Assert.Null(sut.Get(student.Id));
        }

        [Theory]
        [InlineData(1, 1, 1)]
        [InlineData(1, 2, 2)]
        [InlineData(2, 2, 1)]
        public void GetWithPaginationReturnsTheCorrectNumberOfItems(int page, int size, int expectedItems)
        {
            var sut = new StudentRepository();
            sut.Create(new Student { Name = "Jhon", Age = 12 });
            sut.Create(new Student { Name = "Pepe", Age = 12 });
            sut.Create(new Student { Name = "Maria", Age = 12 });

            var result = sut.Get(page, size);

            Assert.Equal(expectedItems, result.Count());
            Assert.Equal(3, result.Total);
            Assert.Equal(page, result.Page);
        }

        [Theory]
        [InlineData(1, 1, "Jhon")]
        [InlineData(2, 1, "Pepe")]
        [InlineData(3, 1, "Maria")]
        public void GetWithPaginationReturnsTheCorrectItems(int page, int size, String expectedName)
        {
            var sut = new StudentRepository();
            sut.Create(new Student { Name = "Jhon", Age = 12 });
            sut.Create(new Student { Name = "Pepe", Age = 12 });
            sut.Create(new Student { Name = "Maria", Age = 12 });

            var result = sut.Get(page, size);

            Assert.Equal(expectedName, result.First().Name);
            Assert.Equal(3, result.Total);
            Assert.Equal(page, result.Page);
        }
    }
}
