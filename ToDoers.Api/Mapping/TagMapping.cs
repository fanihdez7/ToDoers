using ToDoers.Api.Dtos;
using ToDoers.Api.Entities;

namespace ToDoers.Api.Mapping
{
    public static class TagMapping
    {
        public static TagDto ToDto(this Tag tag)
        {
            return new TagDto(tag.Id, tag.Name);
        }

        public static Tag ToEntity(this TagDto tagDto)
        {
            return new Tag
            {
                Id = tagDto.Id,
                Name = tagDto.Name
            };
        }
    }
}
