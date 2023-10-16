using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gym_Management.Models
{
    public class LoginDTO
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
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string ConPassword { get; set; }

        [Required(ErrorMessage = "Please select")]
        public ProfilesDTO Selected { get; set; }
    }
    public enum ProfilesDTO
    {
        Admin,
        Receptionist
    }
}
