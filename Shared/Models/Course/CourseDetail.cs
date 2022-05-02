using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Course
{
    public class CourseDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int Length { get; set; }
        public double Score { get; set; }
        public double Cost { get; set; }
        public double Value { get; set; }
    }
}
