using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services
{
    public interface IPricingService
    {
        Task<double> GetItemPriceAsync(string itemId, int count);
        Task<double> GetCartTotalAsync(CartDetails userCartDetails);
    }
}