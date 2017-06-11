using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinderProyectoOOP
{
    public class Persona
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Username { get; set; }
        public int age { get; set; }
        public string Password { get; set; }
        public string email { get; set; }

        public string About { get; set; }
        public string Work { get; set; }
        public string School { get; set; }
        public string Anthem { get; set; }
        public int Location { get; set; }
        public string Gender { get; set; }
        public int AgeRange { get; set; }
        public bool emailvalidation { get; set; }

        public Persona(string Name, DateTime Birthdate)
        {
            this.Username = Username;
            this.Password = Password;
            this.email = email;
            this.About = About;
            this.Work = Work;
            this.School = School;
            this.Anthem = Anthem;
        }

        public Persona()
        {

        }

        public int Age()
        {
            TimeSpan intervalo = DateTime.Now.Subtract(BirthDate);
            int age = (int)(intervalo.TotalDays / 365.25);
            return age;
        }

        public override string ToString()
        {
            return (Name + " , " + age.ToString());
        }
       
    }
}
