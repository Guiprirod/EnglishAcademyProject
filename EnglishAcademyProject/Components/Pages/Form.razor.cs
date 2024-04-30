using EnglishAcademyProject.Components.Class;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using System;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnglishAcademyProject.Components.Pages
{
    public partial class Form
    {
        [Parameter]
        public string? CourseId { get; set; }

        [Parameter]
        public string? NameCourse { get; set; }

        public FormClass form = new FormClass();
        private List<HowMeetUs> dataHowMeetus = new List<HowMeetUs>();

        private List<OfficialCourses> data = new List<OfficialCourses>();
        private int stepForm = new int();
        private string? selectedCourse;
        private string price;
        private string? mainTitle = "Datos del Curso";
        private string? _labelParentName;
        private HxModal emptyForm;
        private string? method;


        protected override async Task OnInitializedAsync()
        {
            _labelParentName = "Nombre y apellidos de madre/padre/tutor legal *";

            var miMock = new Mock.Mock();

            data = miMock.Courses();

            selectedCourse = NameCourse;

            foreach(var currentCourse in data)
            {
                if(currentCourse == data.FirstOrDefault(x => x.ID.ToString() == CourseId))
                {
                    price = currentCourse.Price.ToString() + "€";
                }
            }
            dataHowMeetus = miMock.Vias();

        }
        private void ReturnForm(FormClass form, int pass)
        {

            if(pass == 1)
            {
                stepForm = 0;
                mainTitle = "Datos del Curso";

            }
            else if(pass == 2)
            {
                stepForm = 1;
                mainTitle = "Datos de Contacto";
            }
            else if(pass == 3)
            {
                stepForm = 2;
                mainTitle = "Aceptación condiciones legales";
            }
            else if(pass == 4)
            {
                stepForm = 3;
                mainTitle = "Datos Bancarios";
            }
            else
            {
                stepForm = 0;
            }
            navigationManager.NavigateTo($"form/{CourseId.ToString()}/{NameCourse}");
        }
        private async void SendForm(FormClass form, int pass)
        {
            if(pass == 0)
            {
                if(!System.String.IsNullOrEmpty(form.sonsName) && !System.String.IsNullOrEmpty(form.preferenceDay) && !System.String.IsNullOrEmpty(form.preferenceSchedule) && !System.String.IsNullOrEmpty(form.sonsBirthday.ToShortDateString()))
                {
                    stepForm = 1;
                    mainTitle = "Datos de Contacto";
                }
                else
                    await emptyForm.ShowAsync();

            }
            else if(pass == 1)
            {
                if(!System.String.IsNullOrEmpty(form.nameparent) && !System.String.IsNullOrEmpty(form.nationalId) && !System.String.IsNullOrEmpty(form.phoneNumber) && !System.String.IsNullOrEmpty(form.email) && !System.String.IsNullOrEmpty(form.address) && !System.String.IsNullOrEmpty(form.city) && !System.String.IsNullOrEmpty(form.province) && !System.String.IsNullOrEmpty(form.cp))
                {
                    stepForm = 2;
                    mainTitle = "Datos de Interés";
                }
                else
                {
                    await emptyForm.ShowAsync();
                }

            }
            else if(pass == 2)
            {
                stepForm = 3;
                mainTitle = "Aceptación condiciones legales";
            }
            else if(pass == 3)
            {
                if(form.authorizationAdditionalClauses && form.authorizationDataProcessing)
                {
                    stepForm = 4;
                    mainTitle = "Datos Bancarios";
                }
                else
                {
                    await emptyForm.ShowAsync();
                }


            }
            else if(pass == 4)
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
