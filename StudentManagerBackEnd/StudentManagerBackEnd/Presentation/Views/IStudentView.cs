using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StudentManagerBackEnd.Domain.Students;

namespace StudentManagerBackEnd.Presentation.Views
{
    public interface IStudentView
    {
        List<Student> Students { get; set; }

        void SelectFilter(KeyValuePair<string, string> keyValue);

        void SelectSortingBy(string field);

        Task<int> Search();
    }
}
