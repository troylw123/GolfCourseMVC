using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Price
{
    public class PriceDetail
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public int AmountPaid { get; set; }
        public TeeTime Time { get; set; }
        public enum TeeTime { weekdayAM, weekdayPM, weekendAM, weekendPM }
        public DateTime Date { get; set; }
    }
}
