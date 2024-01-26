using finalinternshipproject.Dtos.Collection;

namespace finalinternshipproject.Services.Collection
{
    public interface ICollectionService
    {
        Task<ServiceResponse<string>> PostCollection(CollectionPostDto request);
        Task<ServiceResponse<int>> EditCollection(CollectionEditDto request,int id);

        Task<ServiceResponse<List<CollectionGetDto>>> GetAllCollections();
        Task<ServiceResponse<CollectionGetDto>> GetCollectionById(int id);
        Task<ServiceResponse<List<CollectionGetDto>>> GetCollectionsByUserId(int id);

        Task<ServiceResponse<bool>> DeleteCollection(int id);

        Task<ServiceResponse<CollectionAddItemDto>> GetCollectionFields(int id);
        Task<ServiceResponse<CollectionAddItemDto>> GetCollectionFieldsAnonymous(int id);


        Task<ServiceResponse<bool>> CheckIfUserCanManageCollection(int id);


    }
}
