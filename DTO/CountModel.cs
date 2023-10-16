using System.ComponentModel.DataAnnotations.Schema;

namespace Gym_Management.DTO
{
    public class CountModel
    {
       public int Department { get; set; }
        public int Employee { get; set; }
        public int PresentCount { get; set; }
        public int AbsentCount { get; set; }
    }
}
