using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.Models
{
    [Table("Attendance")]
    public class Attendance
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Please select an Employee type")]
        [Display(Name = "Employee Type")]
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
        [Display(Name = "Timing")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm}", ApplyFormatInEditMode = true)]
        public DateTime Timing { get; set; }

        [Required(ErrorMessage = "Please enter")]
        [Display(Name = "Status")]
        public int EmpStatusId { get; set; }
        public EmpStatus Status { get; set; }
        public enum EmployeeType
        {
            Customer,
            Trainer
        }
    }
}
