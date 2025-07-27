using ToDoers.Api.Data;
using ToDoers.Api.Endpoints;
using ToDoers.Api.Services;

var builder = WebApplication.CreateBuilder(args);

var dbPwd = builder.Configuration["db:pwd"];

var connString = "Server=localhost;Database=ToDoers;User Id=sa;Password="+ dbPwd + ";TrustServerCertificate=true;";

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddSqlServer<TodoContext>(connString);

var app = builder.Build();

app.MapTodosEndpoints();
app.MapTagsEndpoints();

await app.MigrateDbAsync();

app.Run();
