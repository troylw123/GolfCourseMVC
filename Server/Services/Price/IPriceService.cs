using GolfCourseMVC.Shared.Models.Price;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GolfCourseMVC.Server.Services.PriceService
{
    public interface IPriceService
    {
        Task<IEnumerable<PriceListItem>> GetAllPricesAysnc();
        Task<IEnumerable<PriceListItem>> GetPricesByCourse(int courseId);
        Task<bool> CreatePriceAsync(PriceCreate model);
        Task<PriceDetail> GetPriceByIdAsync(int id);
        Task<bool> UpdatePriceAsync(PriceEdit model);
        Task<bool> DeletePriceAsync(int id);
        void SetUserId(string userId);
    }
}
