using EnglishAcademyProject.Components.Class;
using EnglishAcademyProject.Components.Pages;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnglishAcademyProject.Components.Mock
{
    public class Mock
    {

        public List<OfficialCourses> Prueba()
        {

            var data = new List<OfficialCourses>
        {
            new OfficialCourses { ID = 1, Name = "Alphaboat" , Price= 30},
            new OfficialCourses{ ID = 2, Name = "Nat and Friends", Price= 30 },
            new OfficialCourses { ID = 3, Name = "It's a Baby Dragon", Price = 30 }
            // Agrega más elementos según sea necesario
        };
            return data;
        }
    }
}
