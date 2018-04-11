using System;
namespace StudentManagerBackEnd.Domain.Students
{
    //TODO: Consider creating an Enumerator base class
    public sealed class StudentType
    {
        public static readonly string KINDER = "Kinder";
        public static readonly string ELEMENTARY = "Elementary";
        public static readonly string HIGH = "High";
        public static readonly string UNIVERSITY = "University";
    }
}
