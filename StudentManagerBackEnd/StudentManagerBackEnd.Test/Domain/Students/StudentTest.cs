using System;
using System.Collections.Generic;
using StudentManagerBackEnd.Domain.Students;
using Xunit;

namespace StudentManagerBackEnd.Test.Domain.Students
{
    public class StudentTest
    {
        [Theory]
        [InlineData("Kinder", "Leia", "F", "20151231145934", 2015, 12, 31, 14, 59, 34)]
        [InlineData("Kinder", "A0", "F", "20180411190725", 2018,  4, 11, 19,  7, 25)]
        public void DataConstructorWithCorrectDataInitializesTheFieldsCorrectly(
            string type, 
            string name, 
            string gender, 
            string birth,
            int expectedYear,
            int expectedMonth,
            int expectedDay,
            int expectedHour,
            int expectedMinute,
            int expectedSecond) 
        {
            var data = new List<string> { type, name, gender,  birth };

            var actual = new Student(data);

            Assert.Equal(StudentType.KINDER, actual.Type);
            Assert.Equal(name, actual.Name);
            Assert.Equal(Gender.FEMALE, actual.Gender);
            Assert.Equal(expectedYear, actual.Birth.Year);
            Assert.Equal(expectedMonth, actual.Birth.Month);
            Assert.Equal(expectedDay, actual.Birth.Day);
            Assert.Equal(expectedHour, actual.Birth.Hour);
            Assert.Equal(expectedMinute, actual.Birth.Minute);
            Assert.Equal(expectedSecond, actual.Birth.Second);
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
