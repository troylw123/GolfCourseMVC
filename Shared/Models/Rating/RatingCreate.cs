using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfCourseMVC.Shared.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        [Range(1, 10, ErrorMessage = "{0} must be a whole number between {1} and {2}.")]
        public int Score { get; set; }
        [Required]
        [MinLength(10, ErrorMessage ="{0} needs at least {1} characters.")]
        [MaxLength(300, ErrorMessage ="{0} cannot exceed {1} characters.")]
        public string Comment { get; set; }
        [Required]
        public int CourseId { get; set; }
    }
}
