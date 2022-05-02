using GolfCourseMVC.Server.Data;
using GolfCourseMVC.Server.Models;
using GolfCourseMVC.Shared.Models.Course;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.CourseService
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private string _userId;

        public CourseService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateCourseAsync(CourseCreate model)
        {
            var course = new Course
            {
                Name = model.Name,
                Address = model.Address,
                Length = model.Length,
            };

            _context.Courses.Add(course);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCourseAsync(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            _context.Courses.Remove(course);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<IEnumerable<CourseListItem>> GetAllCoursesAsync()
        {
            var courses = _context.Courses.Include(x => x.Ratings)
                .Include(x => x.Prices).Select(x => new CourseListItem
            {
                Id = x.Id,
                Name = x.Name,
                Address = x.Address,
                Length = x.Length,
                Score = x.Score,
                Cost = x.Cost,
                Value = x.Value,
            });

            return await courses.ToListAsync();
        }

        public async Task<CourseDetail> GetCourseByIdAsync(int id)
        {
            var course = await _context.Courses.Include(x => x.Ratings)
                .Include(x => x.Prices).FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return null;

            var detail = new CourseDetail
            {
                Id = course.Id,
                Name = course.Name,
                Address = course.Address,
                Length = course.Length,
                Score = course.Score,
                Cost = course.Cost,
                Value = course.Value,
            };

            return detail;
        }

        public async Task<CourseDetail> GetCourseByNameAsync(string name)
        {
            var course = await _context.Courses.Where(x => x.Name.Contains(name)).FirstOrDefaultAsync();
            if (course == null) return null;

            var detail = new CourseDetail
            {
                Id = course.Id,
                Name = course.Name,
                Address = course.Address,
                Length = course.Length,
            };

            return detail;
        }

        public async Task<bool> UpdateCourseAsync(CourseEdit model)
        {
            if (model == null) return false;

            var course = await _context.Courses.FindAsync(model.Id);

            course.Name = model.Name;
            course.Address = model.Address;
            course.Length = model.Length;

            return await _context.SaveChangesAsync() == 1;

        }

        public void SetUserId(string userId) => _userId = userId;

        public async Task<List<CourseListItem>> SearchCourses(string searchText)
        {
            var courses = _context.Courses.Where(x => x.Name.Contains(searchText))
                .Include(x => x.Ratings)
                .Include(x => x.Prices).Select(x => new CourseListItem
                {
                    Id = x.Id,
                    Name = x.Name,
                    Address = x.Address,
                    Length = x.Length,
                    Score = x.Score,
                    Cost = x.Cost,
                    Value = x.Value,
                });

            return await courses.ToListAsync();
        }
    }
}
