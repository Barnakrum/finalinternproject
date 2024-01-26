using finalinternshipproject.Dtos.Tag;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace finalinternshipproject.Services.Tag
{
    public interface ITagService
    {
        Task<ServiceResponse<List<string>>> GetAllTags();
        Task<ServiceResponse<List<TagForCloudDto>>> GetTagsForCloud();
    }
}
