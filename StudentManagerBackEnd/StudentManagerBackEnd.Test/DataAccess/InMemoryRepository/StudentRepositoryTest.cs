using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagerBackEnd.DataAccess;
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

            var result = sut.Get(new QueryParameters(page, size));

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

            var result = sut.Get(new QueryParameters(page, size));

            Assert.Equal(expectedName, result.First().Name);
            Assert.Equal(3, result.Total);
            Assert.Equal(page, result.Page);
        }

        [Fact]
        public void GetWithPaginationByNameSortedAlphabeticallyReturnsTheCorrectItems()
        {
            var sut = new StudentRepository();
            sut.Create(new Student { Name = "JhonE", Age = 12 });
            sut.Create(new Student { Name = "JhonC", Age = 12 });
            sut.Create(new Student { Name = "JhonA", Age = 12 });
            sut.Create(new Student { Name = "JhonB", Age = 12 });
            sut.Create(new Student { Name = "JhonD", Age = 12 });

            var result = sut.Get(new QueryParameters(1, 3, new List<string> { "name" }));

            Assert.Equal(5, result.Total);
            Assert.Equal(1, result.Page);
            var actualArray = result.ToArray();
            Assert.Equal("JhonA", actualArray[0].Name);
            Assert.Equal("JhonB", actualArray[1].Name);
            Assert.Equal("JhonC", actualArray[2].Name);
        }

        [Fact]
        public void GetByStudentTypeSortedByDateReturnsTheCorrectResults()
        {
            var sut = new StudentRepository();
            sut.Create(new Student { Name = "JhonE", Birth = new DateTime().AddMinutes(5), Type = StudentType.ELEMENTARY });
            sut.Create(new Student { Name = "JhonC", Birth = new DateTime().AddMinutes(3), Type = StudentType.ELEMENTARY });
            sut.Create(new Student { Name = "JhonA", Birth = new DateTime().AddMinutes(1), Type = StudentType.HIGH });
            sut.Create(new Student { Name = "JhonB", Birth = new DateTime().AddMinutes(2), Type = StudentType.KINDER });
            sut.Create(new Student { Name = "JhonD", Birth = new DateTime().AddMinutes(4) });

            var result = sut.Get(new QueryParameters(
                page: 1,
                size: 3,
                sortingFields: new List<string> { "birth" },
                filteringFields: new List<KeyValuePair<String, String>> { new KeyValuePair<string, string>("type", StudentType.ELEMENTARY ) }));

            var actualArray = result.ToArray();
            Assert.Equal("JhonE", actualArray[0].Name);
            Assert.Equal("JhonC", actualArray[1].Name);
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.Total);
            Assert.Equal(1, result.Page);
        }

        [Fact]
        public void GetByGenderAndTypeSortedByDateReturnsTheCorrectResults()
        {
            var sut = new StudentRepository();
            sut.Create(new Student { Name = "JhonE", Gender = Gender.MALE, Birth = new DateTime().AddMinutes(5), Type = StudentType.ELEMENTARY });
            sut.Create(new Student { Name = "MariaC", Gender = Gender.FEMALE, Birth = new DateTime().AddMinutes(3), Type = StudentType.ELEMENTARY });
            sut.Create(new Student { Name = "MariaA", Gender = Gender.FEMALE, Birth = new DateTime().AddMinutes(1), Type = StudentType.HIGH });
            sut.Create(new Student { Name = "MariaB", Gender = Gender.FEMALE, Birth = new DateTime().AddMinutes(2), Type = StudentType.ELEMENTARY });
            sut.Create(new Student { Name = "JhonD", Gender = Gender.MALE, Birth = new DateTime().AddMinutes(4) });

            var result = sut.Get(new QueryParameters(
                page: 1,
                size: 3,
                sortingFields: new List<string> { "birth" },
                filteringFields: new List<KeyValuePair<String, String>> 
                { 
                    new KeyValuePair<string, string>("type", StudentType.ELEMENTARY),
                    new KeyValuePair<string, string>("gender", Gender.FEMALE)
                }));

            var actualArray = result.ToArray();
            Assert.Equal("MariaC", actualArray[0].Name);
            Assert.Equal("MariaB", actualArray[1].Name);
            Assert.Equal(2, result.Count());
            Assert.Equal(2, result.Total);
            Assert.Equal(1, result.Page);
        }
    }
}
