namespace ToDoers.Api.Entities
{
    public class Todo
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public int TagId { get; set; }
        public Tag? Tag { get; set; }

        public int Priority { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
