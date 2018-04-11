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
            this.Type = listData[0];
            this.Name = listData[1];
            this.Gender = listData[2];
            this.Birth = DateTime.ParseExact(
                listData[3], 
                "yyyyMMddHHmmss", 
                System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
