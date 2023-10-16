using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Xml.Linq;

namespace Gym_Management.Models
{
    public class CustomerDTO
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter your Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage ="Please enter your E-mail")]
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
        public List<SelectListItem> Timings { get; set; }
        public string Timingname { get; set; }

        [Required(ErrorMessage = "Please enter Age")]
        [Display(Name = "Age")]
        [Range(1,100)]
        public int Age { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Gender")]
        public int GenderId { get; set; }
        [ForeignKey("GenderId")]

        public List<SelectListItem> Gender { get; set; }
        public string Gendername { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Membership Details")]
        public int MembershipId { get; set; }
        [ForeignKey("MembershipId")]
        public List<SelectListItem> Membership { get; set; }
        public string Membershipname { get; set; }

        [Required(ErrorMessage = "Please select")]
        public int TrainerId { get; set; }
        
        public List<SelectListItem> Trainer { get; set; }
        public string Trainername { get; set; }
       


    }
}
