using System;
namespace StudentManagerBackEnd.Domain.Students
{
    //TODO: Consider creating an Enumerator base class
    public sealed class StudentType
    {
        public static readonly string KINDER = "kinder";
        public static readonly string ELEMENTARY = "elementary";
        public static readonly string HIGH = "high";
        public static readonly string UNIVERSITY = "university";
    }
}
