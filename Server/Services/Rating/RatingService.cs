using GolfCourseMVC.Server.Data;
using GolfCourseMVC.Server.Models;
using GolfCourseMVC.Shared.Models.Rating;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.RatingService
{
    public class RatingService : IRatingService
    {
        private readonly ApplicationDbContext _context;
        private string _userId;

        public RatingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRatingAsync(RatingCreate model)
        {
            var rating = new Rating
            {
                Score = model.Score,
                Comment = model.Comment,
                CourseId = model.CourseId,
                Date = DateTime.Now,
            };

            _context.Ratings.Add(rating);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteRatingAsync(int id)
        {
            var rating = await _context.Ratings.FindAsync(id);
            _context.Ratings.Remove(rating);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<RatingListItem>> GetAllRatingsAysnc()
        {
            var ratings = _context.Ratings.Select(n => new RatingListItem
            {
                Id = n.Id,
                Score = n.Score,
                Date = n.Date,
                CourseName = n.Course.Name,
            });

            return await ratings.ToListAsync();
        }

        public async Task<RatingDetail> GetRatingByIdAsync(int id)
        {
            var rating = await _context.Ratings
                .Include(nameof(Course))
                .FirstOrDefaultAsync(n => n.Id == id);
            if (rating == null) return null;

            var detail = new RatingDetail
                {
                    Id = rating.Id,
                    Score = rating.Score,
                    Comment = rating.Comment,
                    Date = rating.Date,
                    CourseId = rating.Course.Id,
                    CourseName = rating.Course.Name,
                };

            return detail;
        }

        public async Task<IEnumerable<RatingListItem>> GetRatingsByCourse(int courseId)
        {
            var ratingQuery = _context.Ratings.Where(x => x.CourseId == courseId)
                .Select(x => new RatingListItem
                {
                    Id = x.Id,
                    Score = x.Score,
                    Date = x.Date,
                    CourseName = x.Course.Name,
                });

            return await ratingQuery.ToListAsync();
        }

        public async Task<bool> UpdateRatingAsync(RatingEdit model)
        {
            if (model == null) return false;

            var rating = await _context.Ratings.FindAsync(model.Id);
            if (rating == null) return false;

            rating.Score = model.Score;
            rating.Comment = model.Comment;
            rating.CourseId = model.CourseId;

            return await _context.SaveChangesAsync() == 1;
        }
        public void SetUserId(string userId) => _userId = userId;
    }
}
