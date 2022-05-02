using GolfCourseMVC.Shared.Models.Course;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.CourseService
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseListItem>> GetAllCoursesAsync();
        Task<bool> CreateCourseAsync(CourseCreate model);
        Task<CourseDetail> GetCourseByIdAsync(int id);
        Task<CourseDetail> GetCourseByNameAsync(string name);
        Task<bool> UpdateCourseAsync(CourseEdit model);
        Task<bool> DeleteCourseAsync(int id);
        void SetUserId(string userId);
        Task<List<CourseListItem>> SearchCourses(string searchText);
    }
}
