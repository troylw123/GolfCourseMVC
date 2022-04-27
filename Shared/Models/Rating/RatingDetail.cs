using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Rating
{
    public class RatingDetail
    {
        public int Id { get; set; }
        public int Score { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
        public int CourseId { get; set; }
        public string CourseName { get; set; }
    }
}
