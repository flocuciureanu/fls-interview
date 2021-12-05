using System.Threading.Tasks;
using FirstLineSoftwareInterviewTask.Business.Entities.Entities.User.MongoDataModel;

namespace FirstLineSoftwareInterviewTask.Business.Services.Features.Item.Services
{
    public interface IPricingService
    {
        Task<double> GetItemPrice(string itemId, int count);
        Task<double> GetCartTotal(CartDetails userCartDetails);
    }
}