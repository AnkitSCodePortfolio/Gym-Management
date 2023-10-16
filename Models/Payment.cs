using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Gym_Management.Models
{
    public class Payment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string CustomerName { get; set; }
        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Please enter Amount")]
        [DataType(DataType.Currency)]
        public string Amount { get; set; }
        [Display(Name = "Fitness Goal")]
        [Required(ErrorMessage = "Please select")]
        public int GoalId { get; set; }
        public Goal goal { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please enter your Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } 

        [Display(Name = "Date of Payment Received")]
        [Required(ErrorMessage = "Please select")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        [Display(Name = "Method Of Payment")]
        [Required(ErrorMessage = "Please select")]
        public int PayMethodId { get; set; }
        public PayMethods PaymentMethod { get; set; }

        public string? ReferanceNumber { get; set; }

        [Display(Name = "Membership")]
        [Required(ErrorMessage = "Please select")]
        public int MembershipId { get; set; }
        public Membership Membership { get; set; }

    }
}
