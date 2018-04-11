using System;
namespace StudentManagerBackEnd.Domain.Students
{
    public class Student
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public short Age { get; set; }

        public string Gender { get; set; }

        public string StudentType { get; set; }

        public DateTime Birth;
    }
}
