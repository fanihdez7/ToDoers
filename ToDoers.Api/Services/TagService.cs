using Microsoft.EntityFrameworkCore;
using ToDoers.Api.Data;
using ToDoers.Api.Dtos;
using ToDoers.Api.Mapping;

namespace ToDoers.Api.Services
{
    public class TagService : ITagService
    {

        private readonly TodoContext _dbContext;

        public TagService(TodoContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IEnumerable<TagDto>> GetTagsAsync()
        {
            return await _dbContext.Tags
                   .Select(tag => tag.ToDto())
                   .AsNoTracking()
                   .ToListAsync();
        }
    }
}
