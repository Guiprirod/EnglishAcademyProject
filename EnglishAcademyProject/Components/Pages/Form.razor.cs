using EnglishAcademyProject.Components.Class;
using Microsoft.AspNetCore.Components;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnglishAcademyProject.Components.Pages
{
    public partial class Form
    {
        [Parameter]
        public string? CourseId { get; set; }

        [Parameter]
        public string? NameCourse { get; set; }

        public FormClass? form = new FormClass();
        private List<OfficialCourses> data = new List<OfficialCourses>();
        private int stepForm = new int();
        private string? selectedCourse;
        private string price;
        private string? mainTitle = "Datos del Curso";

        protected override async Task OnInitializedAsync()
        {
            var miMock = new Mock.Mock();

            data = miMock.Prueba();

            selectedCourse = NameCourse;

            foreach (var currentCourse in data)
            {
                if (currentCourse == data.FirstOrDefault(x => x.ID.ToString() == CourseId))
                {
                    price = currentCourse.Price.ToString() + "€";
                }
            }

        }
        private void ReturnForm(FormClass form, int pass)
        {
           
            if (pass == 1)
            {
                stepForm = 0;
                mainTitle = "Datos del Curso";

            }
            else if (pass == 2)
            {
                stepForm = 1;
                mainTitle = "Datos de Contacto";
            }
            else if (pass == 3)
            {
                stepForm = 2;
                mainTitle = "Datos de Interés";
            }
            else
            {
                stepForm = 0;
            }
            navigationManager.NavigateTo($"form/{CourseId.ToString()}/{NameCourse}");
        }
        private void SendForm(FormClass form, int pass)
        {
            if (pass == 0)
            {
                stepForm = 1;
                mainTitle = "Datos de Contacto";
            }
            else if (pass == 1)
            {
                stepForm = 2;
                mainTitle = "Datos de Interés";

            }
            else if (pass == 2)
            {
                stepForm = 3;
                mainTitle = "Datos Bancarios";
            }
            else if (pass == 3)
            {
                SendEmail();
            }
            else
            {
                stepForm = 0;
            }
            navigationManager.NavigateTo($"form/{CourseId.ToString()}/{NameCourse}");

        }

        private void SendEmail()
        {

        }
    }
}
