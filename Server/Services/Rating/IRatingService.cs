using GolfCourseMVC.Shared.Models.Rating;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.RatingService
{
    public interface IRatingService
    {
        Task<IEnumerable<RatingListItem>> GetAllRatingsAysnc();
        Task<IEnumerable<RatingListItem>> GetRatingsByCourse(int courseId);
        Task<bool> CreateRatingAsync(RatingCreate model);
        Task<RatingDetail> GetRatingByIdAsync(int id);
        Task<bool> UpdateRatingAsync(RatingEdit model);
        Task<bool> DeleteRatingAsync(int id);
        void SetUserId(string userId);
    }
}
