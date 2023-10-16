using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.Models
{
    [Table("EmployeeStatus")]
    public class EmpStatus
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
   
}
