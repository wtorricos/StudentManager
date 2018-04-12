using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StudentManagerBackEnd.DataAccess;
using StudentManagerBackEnd.Domain.Students;
using StudentManagerBackEnd.Presentation.ModelView;

namespace StudentManagerBackEnd.Presentation.Views
{
    public class ConsoleStudentView : IStudentView
    {
        private List<KeyValuePair<string, string>> selectedFilters = new List<KeyValuePair<string, string>>();

        private string sortingField;

        public List<Student> Students { get; set; }

        public StudentController Controller { get; set; }

        public void ProcessArgs(StudentController controller, string[] args) 
        {
            this.Controller = controller;
            var argList = args.ToList();
            if(argList.Count < 1) 
            {
                throw new InvalidOperationException("At least the path to the csv file is required");
            }

            this.Controller.Initialize(argList[0]);
            var filteringArguments = argList
                .Skip(1)
                .Select(arg =>
                {
                    var keyValueArg = arg.Split("=");
                    return new KeyValuePair<string, string>(keyValueArg[0], keyValueArg[1]);
                })
                .ToList();

            filteringArguments.ForEach(fa => this.SelectFilter(fa));

            if(filteringArguments.Any(fa => fa.Key == "name")) 
            {
                this.SelectSortingBy("name");
            } 
            else if(filteringArguments.Any(fa => fa.Key == "type"))
            {
                this.SelectSortingBy("birth");
            }

            var searchResult = this.Search().Result;
        }

        public void SelectFilter(KeyValuePair<string, string> keyValue)
        {
            this.selectedFilters.Add(keyValue);
        }

        public void SelectSortingBy(string field)
        {
            this.sortingField = field;
        }

        public async Task<int> Search() 
        {
            //we are hardcoding some of the parameters as the console view won't offer a way to set them for the moment
            int defaultPage = 1;
            int defaultSize = int.MaxValue;
            var query = new QueryParameters(
                defaultPage,
                defaultSize,
                new List<string> { this.sortingField },
                this.selectedFilters);

            return await this.Controller.Search(query);
        }

        public void DisplayStudents() 
        {
            Console.WriteLine($"Results found ({ this.Students.Count().ToString() }):");
            this.Students.ForEach(s => Console.WriteLine(s));    
        }
    }
}
