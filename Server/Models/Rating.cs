using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCourseMVC.Server.Models
{
    public class Rating
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Score { get; set; }
        [Required]
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }
    }
}
