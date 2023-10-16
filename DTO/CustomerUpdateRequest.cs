using Gym_Management.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.DTO
{
    public class CustomerUpdateRequest
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

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Timing")]
        public int TimingId { get; set; }
        public List<SelectListItem> Timings { get; set; }

        [Required(ErrorMessage = "Please select")]
        [Display(Name = "Membership Details")]
        public int MembershipId { get; set; }
        [ForeignKey("MembershipId")]
        public List<SelectListItem> Membership { get; set; }
    }
}
