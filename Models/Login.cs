using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gym_Management.Models
{
    public class Login
    {
       
     
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please enter your E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Please enter your Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string ConPassword { get; set; }

        [Required(ErrorMessage = "Please select")]
        public Profiles Selected { get; set; }
    }
    public enum Profiles
    {
        Admin,
        Receptionist
    }
}
