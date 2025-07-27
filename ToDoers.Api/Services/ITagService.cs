using ToDoers.Api.Dtos;

namespace ToDoers.Api.Services
{
    public interface ITagService
    {        
        Task<IEnumerable<TagDto>> GetTagsAsync();
    }
}
