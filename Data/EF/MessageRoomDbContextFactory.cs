using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

using System.Text;

namespace MessageRoomSolution.Data.EF
{
    internal class MessageRoomDbContextFactory : IDesignTimeDbContextFactory<MessageRoomDbContext>
    {
        public MessageRoomDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("MessageRoom");
            var optionsBuilder = new DbContextOptionsBuilder<MessageRoomDbContext>();
            optionsBuilder.UseSqlServer(connectionString);
            return new MessageRoomDbContext(optionsBuilder.Options);
        }
    }
}