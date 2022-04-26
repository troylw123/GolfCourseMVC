using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Price
{
    public class PriceEdit
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int AmountPaid { get; set; }
        public TeeTime Time { get; set; }
        public enum TeeTime { weekdayAM, weekdayPM, weekendAM, weekendPM }
        public int CourseId { get; set; }
        
    }
}
