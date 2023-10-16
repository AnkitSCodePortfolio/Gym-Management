using System.ComponentModel.DataAnnotations;

namespace Gym_Management.Models
{
    public class PayMethods
    {
        [Key]
        public int Id { get; set; } 
        public string Name { get; set; }
    }
}
