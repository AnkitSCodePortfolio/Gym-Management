using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.Models
{
   
    public class Goal
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } 
    }
}
