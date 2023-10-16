using System.ComponentModel.DataAnnotations;

namespace Gym_Management.DTO
{
    public class TrainerUpdateRequest
    {
        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please enter your E-mail")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Please enter your Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dateofjoin { get; set; }

        [Required(ErrorMessage = "Please enter Experiance")]
        [Display(Name = "Experiance")]
        [Range(1, 100)]
        public int Experiance { get; set; }
    }
}
