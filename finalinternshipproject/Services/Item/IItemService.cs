using finalinternshipproject.Dtos.Item;

namespace finalinternshipproject.Services.Item
{
    public interface IItemService
    {
        Task<ServiceResponse<int>> PostItem(ItemPostDto request, int id);
        Task<ServiceResponse<ItemGetDto>> GetItemById(int id);
        Task<ServiceResponse<List<ItemGetDto>>> GetItemByCollectionId(int id);

        Task<ServiceResponse<List<ItemGetDto>>> SearchForItem(string searchQuery);

        Task<ServiceResponse<List<int>>> CollectionItems(int id);

        Task<ServiceResponse<List<ItemLatestDto>>> GetLatest(int ammount);

        Task<ServiceResponse<bool>> CanUserManageItem(int id);
        Task<ServiceResponse<int>> DeleteItem(int id);

        Task<ServiceResponse<int>> EditItem(ItemEditDto request,int id);


    }
}
