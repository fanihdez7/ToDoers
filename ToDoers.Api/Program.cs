using ToDoers.Api.Data;
using ToDoers.Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

var dbPwd = builder.Configuration["db:pwd"];

var connString = "Server=localhost;Database=ToDoers;User Id=sa;Password="+ dbPwd + ";TrustServerCertificate=true;";

builder.Services.AddSqlServer<TodoContext>(connString);

var app = builder.Build();

app.MapTodosEndpoints();

app.MigrateDb();

app.Run();
