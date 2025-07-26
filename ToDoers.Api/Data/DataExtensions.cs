﻿using Microsoft.EntityFrameworkCore;

namespace ToDoers.Api.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app) 
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TodoContext>();
            dbContext.Database.Migrate();
        }

    }
}
