using GolfCourseMVC.Server.Data;
using GolfCourseMVC.Server.Models;
using GolfCourseMVC.Shared.Models.Price;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.PriceService
{
    public class PriceService : IPriceService

    {
        private readonly ApplicationDbContext _context;
        private string _userId;

        public PriceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePriceAsync(PriceCreate model)
        {
            var price = new Price
            {
                AmountPaid = model.AmountPaid,
                Time = (Price.TeeTime)model.Time,
                Date = DateTime.Now,
                CourseId = model.CourseId,
            };
            _context.Prices.Add(price);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePriceAsync(int id)
        {
            var price = await _context.Prices.FindAsync(id);

            _context.Prices.Remove(price);

            return await _context.SaveChangesAsync() == 1;
        }


        public async Task<IEnumerable<PriceListItem>> GetAllPricesAysnc()
        {
            var priceQuery = _context.Prices.Select(x => new PriceListItem
            {
                Id = x.Id,
                CourseName = x.Course.Name,
                AmountPaid = x.AmountPaid,
                Time = (PriceListItem.TeeTime)x.Time,
            });
            return await priceQuery.ToListAsync();
        }

        public async Task<PriceDetail> GetPriceByIdAsync(int id)
        {
            var price = await _context.Prices
                .Include(nameof(Course))
                .FirstOrDefaultAsync(x => x.Id == id);
            if (price == null) return null;

            var detail = new PriceDetail
            {
                Id = price.Id,
                CourseName = price.Course.Name,
                AmountPaid = price.AmountPaid,
                Time = (PriceDetail.TeeTime)price.Time,
                Date = price.Date,
            };

            return detail;
        }

        public async Task<IEnumerable<PriceListItem>> GetPricesByCourse(int courseId)
        {
            var priceQuery = _context.Prices.Where(x => x.CourseId == courseId)
                .Select(x => new PriceListItem
            {
                Id = x.Id,
                CourseName = x.Course.Name,
                AmountPaid = x.AmountPaid,
                Time = (PriceListItem.TeeTime)x.Time,
            });
            return await priceQuery.ToListAsync();
        }

        public async Task<bool> UpdatePriceAsync(PriceEdit model)
        {
            if (model == null) return false;

            var price = await _context.Prices.FindAsync(model.Id);

            price.AmountPaid = model.AmountPaid;
            price.Time = (Price.TeeTime)model.Time;
            price.CourseId = model.CourseId;
            
            return await _context.SaveChangesAsync() == 1;
        }

        public void SetUserId(string userId) => _userId = userId;

    }
}
