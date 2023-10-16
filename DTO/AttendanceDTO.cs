using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Gym_Management.Models.Attendance;

namespace Gym_Management.Models
{
    public class AttendanceDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public EmployeeType DisplayType { get; set; }

        [Required(ErrorMessage = "Please enter your Name")]
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "Please enter a valid name")]
        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Status")]
        public int EmpStatusId { get; set; }
        public List<SelectListItem> Status { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Timing")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Timing { get; set; }
        public enum EmployeeType
        {
            AllEmployee,
            Customer,
            Trainer
        }
    }
}
