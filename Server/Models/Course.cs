using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

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
        
        public int Length { get; set; }
        public double Score
        {
            get { return Ratings.Count > 0 ? 
                    (double)Ratings.Select(r => r.Score).Sum() / Ratings.Count : 0; }
        }
        public double Cost
        {
            get { return Prices.Count > 0 ?
                    (double)Prices.Select(p => p.AmountPaid).Sum() / Prices.Count : 0; }
        }
        public double Value
        {
            get { return Score / Cost * 100; }
        }
        
        public virtual List<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual List<Price> Prices { get; set; } = new List<Price>();

    }
}
