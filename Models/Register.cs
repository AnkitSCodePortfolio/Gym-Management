using System.ComponentModel.DataAnnotations;

namespace Gym_Management.Models
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name ="E-mail")]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Required]
        [Display(Name ="Role")]
        public string[] Roles { get; set; }
    }
}
