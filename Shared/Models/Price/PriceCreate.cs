using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Price
{
    public class PriceCreate
    {
        [Required]
        public int AmountPaid { get; set; }
        public TeeTime Time { get; set; }
        public enum TeeTime { weekdayAM, weekdayPM, weekendAM, weekendPM }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        
    }
}
