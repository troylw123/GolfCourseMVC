using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GolfCourseMVC.Server.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public int Length { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public virtual List<Price> Prices { get; set; }

    }
}
