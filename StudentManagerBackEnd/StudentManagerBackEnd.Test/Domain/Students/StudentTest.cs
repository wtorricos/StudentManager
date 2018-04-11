using System;
using System.Collections.Generic;
using StudentManagerBackEnd.Domain.Students;
using Xunit;

namespace StudentManagerBackEnd.Test.Domain.Students
{
    public class StudentTest
    {
        [Theory]
        [InlineData("Kinder", "Leia", "F", "20151231145934")]
        public void DataConstructorWithCorrectDataInitializesTheFieldsCorrectly(
            string type, 
            string name, 
            string gender, 
            string birth) 
        {
            var data = new List<string> { type, name, gender,  birth };

            var actual = new Student(data);

            Assert.Equal(StudentType.KINDER, actual.Type);
            Assert.Equal(name, actual.Name);
            Assert.Equal(Gender.FEMALE, actual.Gender);
            Assert.Equal(2015, actual.Birth.Year);
            Assert.Equal(12, actual.Birth.Month);
            Assert.Equal(31, actual.Birth.Day);
            Assert.Equal(14, actual.Birth.Hour);
            Assert.Equal(59, actual.Birth.Minute);
            Assert.Equal(34, actual.Birth.Second);
        }

        [Fact]
        public void DataConstructorWithInvalidArgumentsThrowsException()
        {
            var data = new List<string> { "a", "b", "c", "d", "additional" };

            try 
            {
                var actual = new Student(data);
                Assert.False(true, "should throw exception");
            } catch(InvalidOperationException e) {
                Assert.NotNull(e);
            } catch(Exception e) {
                Assert.False(true, "invalid operation exception expected got" + e.Message);
            }

        }

        [Fact]
        public void DataConstructorWithNullValuesThrowsException()
        {
            var data = new List<string> { null, "Leia", "F", "20151231145934" };

            try
            {
                var actual = new Student(data);
                Assert.False(true, "should throw exception");
            }
            catch (InvalidOperationException e)
            {
                Assert.NotNull(e);
            }
            catch (Exception e)
            {
                Assert.False(true, "invalid operation exception expected got" + e.Message);
            }
        }

        [Fact]
        public void DataConstructorWithEmptyValuesThrowsException()
        {
            var data = new List<string> { "", "Leia", "F", "20151231145934" };

            try
            {
                var actual = new Student(data);
                Assert.False(true, "should throw exception");
            }
            catch (InvalidOperationException e)
            {
                Assert.NotNull(e);
            }
            catch (Exception e)
            {
                Assert.False(true, "invalid operation exception expected got" + e.Message);
            }
        }
    }
}
