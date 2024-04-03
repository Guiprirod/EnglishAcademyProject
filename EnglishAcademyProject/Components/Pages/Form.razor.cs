using EnglishAcademyProject.Components.Class;

namespace EnglishAcademyProject.Components.Pages
{
    public partial class Form
    {
        public FormClass? form = new FormClass();
        private DateTime? value = new DateTime();

        protected override async Task OnInitializedAsync() 
        {
        }
    }
}
