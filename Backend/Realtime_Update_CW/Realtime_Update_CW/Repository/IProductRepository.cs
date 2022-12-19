using Realtime_Update_CW.Models;

namespace Realtime_Update_CW.Repository
{
    public interface IProductRepository
    {
        List<Product> GetAllProducts();

    }
}
