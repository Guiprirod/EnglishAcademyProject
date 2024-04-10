using EnglishAcademyProject.Components.Class;
using Havit.Blazor.Components.Web.Bootstrap;

namespace EnglishAcademyProject.Components.Pages
{
    public partial class Home
    {
        private List<OfficialCourses> data = new List<OfficialCourses>();
        private int? courseId;
        private string nameCourse;
        private HxModal? noSelectedCourse;


        protected override async Task OnInitializedAsync()
        {
            // Crear un mock de datos
            var miMock = new Mock.Mock();

            data = miMock.Prueba();


        }
        public async void GoToForm()
        {
            var nullableCourseId = courseId.ToString();

            foreach (var course in data)
            {
                if(course == data.FirstOrDefault( x => x.ID == courseId))
                {
                    nameCourse= course.Name;
                }
            }
            if (nullableCourseId != "")
                navigationManager.NavigateTo($"form/{courseId.ToString()}/{nameCourse}", true);
            //navigationManager.NavigateTo("form");
            else
                await noSelectedCourse.ShowAsync();

        }

    }
}
