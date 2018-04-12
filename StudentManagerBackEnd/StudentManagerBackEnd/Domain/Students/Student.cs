using System;
using System.Collections.Generic;
using System.Linq;

namespace StudentManagerBackEnd.Domain.Students
{
    public class Student
    {
        public Student() {}

        public Student(IEnumerable<string> data) {
            var listData = data.ToList();
            this.ValidateData(listData);
            this.Initialize(listData);
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public short Age { get; set; }

        public string Gender { get; set; }

        public string Type { get; set; }

        public DateTime Birth;

        public override string ToString()
        {
            return string.Format(
                "Name: {0}, Type: {1}, Gender: {2}, Birth: {3}",
                this.Name,
                this.Type,
                this.Gender,
                this.Birth);
        } 

        private void ValidateData(List<string> listData)
        {
            if(listData.Count() != 4) 
            {
                var msg = "Students needs 4 fields: Type, Name, Gender, Birth, in this specific order";
                throw new InvalidOperationException(msg);
            }

            if(listData.Any(item => string.IsNullOrEmpty(item))) 
            {
                var msg = "Students needs 4 fields: Type, Name, Gender, Birth and none of them can be null or empty";
                throw new InvalidOperationException(msg);
            }
        }

        private void Initialize(List<string> listData)
        {
            this.Type = listData[0].ToLower();
            this.Name = listData[1];
            if(listData[2].Equals("F")) {
                this.Gender = Students.Gender.FEMALE;
            } else {
                this.Gender = Students.Gender.MALE;
            }

            this.Birth = DateTime.ParseExact(
                listData[3], 
                "yyyyMMddHHmmss", 
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
