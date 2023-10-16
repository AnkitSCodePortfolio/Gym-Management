using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.Models
{
    [Table("Trainer")]
    public class TrainerDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required(ErrorMessage = "Please enter your Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "Please enter your E-mail")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Please enter a valid email address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your Phone Number")]

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public int GenderId { get; set; }
         public List<SelectListItem> Gender { get; set; }
        public string GenderName { get; set; }

        [Required(ErrorMessage = "Please enter your Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dateofjoin { get; set; }

        [Required(ErrorMessage = "Please enter Age")]
        [Display(Name = "Age")]
        [Range(1, 100)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please enter Experiance")]
        [Display(Name = "Experiance")]
        [Range(1, 100)]
        public int Experiance { get; set; }
        
    }
}
