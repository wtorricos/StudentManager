using System;
namespace StudentManagerBackEnd.Domain.Students
{
    public class Student
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public short Age { get; set; }
    }
}
