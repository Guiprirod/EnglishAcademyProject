using EnglishAcademyProject.Components.Class;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Mail;
using System.Net;
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
        private HxModal dataProcessingModal;
        private HxModal additionalClausesModal;
        private HxModal emailSuccessfully;
        private string? method;


        protected override async Task OnInitializedAsync()
        {

            form.additionalInformation = "";
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
                mainTitle = "Datos de interés y condiciones legales";
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
                    mainTitle = "Datos de interés y condiciones legales";
                }
                else
                {
                    await emptyForm.ShowAsync();
                }

            }
            else if(pass == 2)
            {
                if (form.authorizationAdditionalClauses && form.authorizationDataProcessing)
                {
                    stepForm = 3;
                    mainTitle = "Datos Bancarios";
                }
                else
                {
                    await emptyForm.ShowAsync();
                }
            }
        
            else if(pass == 3)
            {
                SendEmail("guilleprieto08@gmail.com", "Bienvenido a Academia Sevilla Este", $"Bienvenido {form.nameparent} a Academias Sevilla este, el registro de su hijo/a {form.sonsName} al curso de {selectedCourse} ha sido completado correctamente");
            }
            else
            {
                stepForm = 0;
            }
            navigationManager.NavigateTo($"form/{CourseId.ToString()}/{NameCourse}");

        }

        public void SendEmail(string toAddress, string subject, string body)
        {
            // Configuración del servidor SMTP
            string smtpHost = "smtp.gmail.com"; // Cambia esto por el host de tu servidor SMTP
            int smtpPort = 587; // Puerto del servidor SMTP (generalmente 587 o 25)
            string smtpUsername = "guilleprieto08@gmail.com"; // Usuario SMTP
            string smtpPassword = "mttg xkur roqb sxbj"; // Contraseña SMTP

            // Dirección de correo electrónico del remitente
            string fromAddress = "guilleprieto08@gmail.com";

            // Creación del mensaje de correo
            MailMessage message = new MailMessage(fromAddress, toAddress);
            message.Subject = subject;
            message.Body = body;

            // Configuración del cliente SMTP
            SmtpClient client = new SmtpClient(smtpHost, smtpPort);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true; // Habilitar SSL si el servidor SMTP lo requiere


            try
            {
                // Envío del correo
                client.Send(message);
                client.UseDefaultCredentials = false;

                emailSuccessfully.ShowAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error al enviar el correo: " + ex.Message);
            }
            finally
            {
                // Liberar recursos
                message.Dispose();
                client.Dispose();
            }
        }
    }
}
