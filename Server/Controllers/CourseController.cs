using GolfCourseMVC.Server.Data;
using GolfCourseMVC.Server.Services.CourseService;
using GolfCourseMVC.Shared.Models.Course;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;

namespace GolfCourseMVC.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        private readonly ApplicationDbContext _context;

        public CourseController(ICourseService courseService, ApplicationDbContext context)
        {
            _courseService = courseService;
            _context = context;
        }

        private string GetUserId()
        {
            string userIdClaim = User.Claims.First(i => i.Type == ClaimTypes.NameIdentifier).Value;
            if (userIdClaim == null) return null;

            return userIdClaim;
        }

        private bool SetUserIdInService()
        {
            var userId = GetUserId();
            if (userId == null) return false;

            _courseService.SetUserId(userId);
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!SetUserIdInService()) return Unauthorized();
            //var courses = await _context.Courses.FromSqlRaw("SELECT * FROM Courses").ToListAsync();

            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> CourseById(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var course = await _courseService.GetCourseByIdAsync(id);

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> CourseByName(string name)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var course = await _courseService.GetCourseByNameAsync(name);

            if (course == null) return NotFound();

            return Ok(course);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CourseCreate model)
        {
            if (model == null) return BadRequest();
            if (!SetUserIdInService()) return Unauthorized();

            bool wasSuccessful = await _courseService.CreateCourseAsync(model);
            if (wasSuccessful) return Ok();
            else return UnprocessableEntity();
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> Edit(int id, CourseEdit model)
        {
            if (!SetUserIdInService()) return Unauthorized();
            if (model == null || !ModelState.IsValid) return BadRequest();
            if (model.Id != id) return BadRequest();

            bool wasSuccessful = await _courseService.UpdateCourseAsync(model);
            if (!wasSuccessful) return BadRequest();
            else return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (!SetUserIdInService()) return Unauthorized();

            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) return NotFound();

            bool wasSuccessful = await _courseService.DeleteCourseAsync(id);
            if (!wasSuccessful) return BadRequest();
            return Ok();
        }

        [HttpGet("Search/{searchText}")]
        public async Task<ActionResult<List<CourseListItem>>> SearchCourses(string searchText)
        {
            return Ok(await _courseService.SearchCourses(searchText));  
        }
    }
}
