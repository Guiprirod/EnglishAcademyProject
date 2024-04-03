using EnglishAcademyProject.Components.Class;

namespace EnglishAcademyProject.Components.Pages
{
    public partial class Home
    {
        private List<OfficialCourses> data = new List<OfficialCourses>();
        private int? courseId;

        protected override async Task OnInitializedAsync()
        {
            // Crear un mock de datos
            data = new List<OfficialCourses>
        {
            new OfficialCourses { ID = 1, Name = "Alphaboat" },
            new OfficialCourses{ ID = 2, Name = "Nat and Friends" },
            new OfficialCourses { ID = 3, Name = "It's a Baby Dragon" }
            // Agrega más elementos según sea necesario
        };


        }
        public void GoToForm()
        {

            navigationManager.NavigateTo("form");
        }

    }
}
