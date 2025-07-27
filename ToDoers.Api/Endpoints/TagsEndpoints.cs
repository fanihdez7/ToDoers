using Microsoft.EntityFrameworkCore;
using ToDoers.Api.Data;
using ToDoers.Api.Mapping;
using ToDoers.Api.Services;

namespace ToDoers.Api.Endpoints
{
    public static class TagsEndpoints
    {
        public static RouteGroupBuilder MapTagsEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("tags");

            group.MapGet("/", async (ITagService tagService) => {
                var result = await tagService.GetTagsAsync();
                return Results.Ok(result);
            });

            return group;
        }
    }
}
