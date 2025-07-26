using Microsoft.EntityFrameworkCore;
using ToDoers.Api.Data;
using ToDoers.Api.Mapping;

namespace ToDoers.Api.Endpoints
{
    public static class TagsEndpoints
    {
        public static RouteGroupBuilder MapTagsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("tags");

            group.MapGet("/", async (TodoContext dbContext) =>
                await dbContext.Tags
                    .Select(tag => tag.ToDto())
                    .AsNoTracking()
                    .ToListAsync()
            );

            return group;
        }
    }
}
