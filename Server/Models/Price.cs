using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GolfCourseMVC.Server.Models
{
    public class Price
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int AmountPaid { get; set; }
        public enum TeeTime { weekdayAM, weekdayPM, weekendAM, weekendPM }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course Course { get; set; }

    }
}
