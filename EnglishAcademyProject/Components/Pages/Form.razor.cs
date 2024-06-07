using EnglishAcademyProject.Components.Class;
using Havit.Blazor.Components.Web.Bootstrap;
using Microsoft.AspNetCore.Components;
using System;
using System.Net.Mail;
using System.Net;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;

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
        public bool enableAnnual = true;
        public bool enableMonth = true;
        public bool enableQuarter = true;


        protected override async Task OnInitializedAsync()
        {

            form.additionalInformation = "";
            _labelParentName = "Nombre y apellidos de tutor legal *";

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
                if(form.authorizationAdditionalClauses && form.authorizationDataProcessing)
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


        private void ReviewChecksMonthPayment()
        {
            if(form.monthlyPayment)
            {
                enableMonth = true;
                enableQuarter = false;
                enableAnnual = false;

            }

        }
        private void ReviewChecksQuarterPayment()
        {

            if(form.quarterlyPayment)
            {
                enableAnnual = false;
                enableMonth = false;
                enableQuarter = true;
            }


        }
        private void ReviewChecksAnnualPayment()
        {

            if(form.annualPayment)
            {
                enableAnnual = true;
                enableMonth = false;
                enableQuarter = false;
            }
        }
        private void ReviewChecksPayment()
        {


            enableAnnual = true;
            enableMonth = true;
            enableQuarter = true;

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

            string cuerpo = $@"
        <html>
        <head>
            <style>
                body {{ font-family: Arial, sans-serif; }}
                h1 {{ color: #333333; }}
                p {{ font-size: 14px; color: #666666; }}
                .button {{
                    background-color: #4CAF50; /* Verde */
                    border: none;
                    color: white;
                    padding: 10px 20px;
                    text-align: center;
                    text-decoration: none;
                    display: inline-block;
                    font-size: 16px;
                    margin: 4px 2px;
                    cursor: pointer;
                    border-radius: 12px;
                }}
                .colum {{ color: #3333; }}
                .align {{ text-align: center; padding:1rem; }}

            </style>
        </head>
        <body>
            <h1>¡Hola, {form.nameparent}!</h1>
            <br>
            <h3>¡Bienvenido a Academia Sevilla Este!</h3>
            <br>
            <p>Este es un correo electrónico de confirmación, donde puedes ver un <strong>resumen del formulario rellenado</strong>, si detectara algún error, por favor póngase en contacto con nosotros lo antes posible.</p>
            <br>
            <p>¡Gracias por confiar en nosotros!</p>

<TABLE BORDER>
	<TR>
		<TH COLSPAN=6><strong>Datos del Curso</strong></TH>
	</TR>
	<TR>
		<TH>Nombre hijo/a</TH> <TH>Fecha Nacimiento</TH> <TH>Curso Escolar</TH> <TH>Día</TH> <TH>Hora</TH><TH>¿Pagar matrícula?</TH> 
	</TR>
	<TR>
		<TD class='align'>{form.sonsName}</TD> <TD class='align'>{form.sonsBirthday.ToShortDateString()}</TD> <TD  class='align'>{form.schoolYear}</TD><TD class='align'>{form.preferenceDay}</TD><TD class='align'>{form.preferenceSchedule}</TD><TD class='align'>{TrueToYes(form.paid.ToString())}</TD> 
	</TR>
</TABLE>
            <br>


<TABLE BORDER>
	<TR>
		<TH COLSPAN=6><strong>Datos de Contacto</strong></TH>
	</TR>
	<TR>
		<TH>Nombre y apellidos del tutor legal</TH> <TH>NIF/NIE</TH> <TH>Teléfono tutor legal</TH> <TH>Correo electrónico</TH> <TH>Dirección</TH><TH>Ciudad</TH> <TH>Provincia</TH> <TH>Código Postal</TH> 
	</TR>
	<TR>
		<TD class='align'>{form.nameparent}</TD> <TD class='align'>{form.nationalId}</TD> <TD class='align'>{form.phoneNumber}</TD><TD class='align'>{form.email}</TD><TD class='align'>{form.address}</TD><TD class='align'>{form.city}</TD> <TD class='align'>{form.province}</TD> <TD class='align'>{form.cp}</TD> 
	</TR>
</TABLE>
            <br>

<TABLE BORDER>
	<TR>
		<TH COLSPAN=7><strong>Datos de Interés y condiciones legales</strong></TH>
	</TR>
	<TR>
		<TH>¿Puede ir solo a casa?</TH> <TH>¿Alergias?</TH> <TH>¿Uso de Whatsapp?</TH> <TH>¿Publicación de fotos?</TH> <TH>¿Envío de comunicaciones comerciales?</TH><TH>Tratamiento de datos</TH> <TH>Cláusulas adicionales</TH>
	</TR>
	<TR>
		<TD class='align'>{TrueToYes(form.goHome.ToString())}</TD> <TD class='align'>{TrueToYes(form.alergias.ToString())}</TD> <TD class='align'>{TrueToYes(form.whatsapp.ToString())}</TD><TD class='align'>{TrueToYes(form.photos.ToString())}</TD><TD class='align'>{TrueToYes(form.commercialCommunications.ToString())}</TD><TD class='align'>{TrueToYes(form.authorizationDataProcessing.ToString())}</TD> <TD class='align'>{TrueToYes(form.authorizationAdditionalClauses.ToString())}</TD>
	</TR>
</TABLE>
            <br>

<TABLE BORDER>
	<TR>
		<TH COLSPAN=7><strong>Datos Bancarios</strong></TH>
	</TR>
	<TR>
		<TH>Información a tener en cuenta</TH> <TH>¿Otras persona para recoger al niño/a?</TH> <TH>Método de pago</TH> <TH>Forma de pago</TH> <TH> {SelectTitleIban(form.bank)} </TH> <TH>¿Otro titular de la cuenta??</TH><TH>Aviso legal y política de privacidad</TH>
	</TR>
	<TR>
		<TD class='align'>{TrueToYes(form.additionalInformation)}</TD> <TD class='align'>{form.morePeople}</TD> <TD class='align'>{SelectPaymentAnswer(form.monthlyPayment, form.quarterlyPayment, form.annualPayment)}</TD><TD class='align'>{SelectMethodPayment(form.bank, form.other)}</TD><TD>{SelectIban(form.bank)}</TD><TD class='align'>{SelectOtherHolder(form.anotherHolder)}</TD> <TD class='align'>{TrueToYes(form.privacyPolicy.ToString())}</TD>
	</TR>
</TABLE>

<br>
            <a href='{"urlBoton"}' class='button'>Visitar Sitio</a>
        </body>
        </html>";

            message.Body = cuerpo;
            message.IsBodyHtml = true;
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

        private string TrueToYes(string value)
        {
            if(value == "True")
            {
                value = "Sí";
            }
            else
            {        
                value = "No";
            }
            return value;
        }
        private string SelectPaymentAnswer(bool monthlyPayment, bool quarterlyPayment, bool annualPayment)
        {
            string answer = "";
            if(monthlyPayment)
            {
                answer = "Mensual";
            }
            else if(quarterlyPayment)
            {
                answer = "Trimestral";
            }
            else if(annualPayment)
            {
                answer = "Anual";
            }
            return answer;
        }
        private string SelectMethodPayment(bool bank, bool other)
        {
            string answer = "";
            if(bank)
            {
                answer = "Domiciliación Bancaria";
            }
            else if(other)
            {
                answer = "Otro";
            }
            return answer;
        }
        private string SelectIban(bool bank)
        {
            string answer = "";
            if(bank)
            {
                answer = form.iban;
            }
            else
            {
                answer = "No se ha seleccionado domiciliación bancaria";
            }
            return answer;
        }   
        private string SelectTitleIban(bool bank)
        {
            string answer = "";
            if(bank)
            {
                answer = "IBAN";
            }
            else
            {
                answer = "No se ha seleccionado domiciliación bancaria";
            }
            return answer;
        }
        private string SelectOtherHolder(string anotherHolder)
        {
            string answer = "";
            if(System.String.IsNullOrEmpty(form.anotherHolder))
            {
                answer = "No";
            }
            else
            {
                answer = form.anotherHolder;
            }
            return answer;
        }
    }
}
