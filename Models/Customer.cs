using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gym_Management.Models
{
    [Table("Customer")]
    public class Customer
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
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


        [Required(ErrorMessage = "Please enter your Address")]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Date Of Joining")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Dateofjoin { get; set; }


        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Timing")]
        public int TimingsId { get; set; }
        [NotMapped]
        public Timings Timings { get; set; }

        [Required(ErrorMessage = "Please enter Age")]
        [Display(Name = "Age")]
        [Range(1, 100)]
        public int Age { get; set; }

        [Display(Name = "Gender")]
        [Required]
        public int GenderId { get; set; }
        [ForeignKey("GenderId")]
        [NotMapped]
        public Genders Gender { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Membership Details")]
        public int MembershipId { get; set; }
        [NotMapped]
        public Membership Membership { get; set; }

        [Required(ErrorMessage = "Please select")]
        public int TrainerId { get; set; }
        [NotMapped]
        public Trainer Trainer { get; set; }

    }

}

