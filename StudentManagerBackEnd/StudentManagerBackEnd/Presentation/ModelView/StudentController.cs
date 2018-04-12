using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> Search(QueryParameters queryParameters) 
        {
            Task<int> task = Task.Run(() => 
            {
                var result = this.studentFacade.Search(queryParameters);
                this.studentView.Students = result.ToList();
                return this.studentView.Students.Count();
            });

            return await task;
        }
    }
}
