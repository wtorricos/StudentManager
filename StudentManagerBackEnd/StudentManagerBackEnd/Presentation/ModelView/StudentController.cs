using System;
using System.Linq;
using StudentManagerBackEnd.Application.Students;
using StudentManagerBackEnd.DataAccess;
using StudentManagerBackEnd.Presentation.Views;

namespace StudentManagerBackEnd.Presentation.ModelView
{
    public class StudentController
    {
        private readonly IStudentView studentView;
        private readonly StudentFacade studentFacade;

        public StudentController(IStudentView studentView, StudentFacade studentFacade)
        {
            this.studentView = studentView;
            this.studentFacade = studentFacade;
        }

        public void Initialize(string path) 
        {
            this.studentFacade.LoadData(path);
        }

        public void Search(QueryParameters queryParameters) 
        {
            var result = this.studentFacade.Search(queryParameters);
            this.studentView.Students = result.ToList();
        }
    }
}
