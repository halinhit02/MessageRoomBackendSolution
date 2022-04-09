using MessageRoomSolution.Data.Entities;
using MessageRoomSolution.Data.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MessageRoomSolution.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<AppUser>();

            modelBuilder.Entity<AppUser>().HasData(
                new AppUser()
                {
                    Id = 1,
                    Email = "admin@halinhit.com",
                    Password = hasher.HashPassword(null, "Admin@123"),
                    Name = "Admin",
                    Dob = new DateTime(2021, 10, 20),
                    Gender = "Nam",
                    StaffId = 0,
                    Phone = "0123456789",
                    IsAdmin = true,
                    IsLocked = false,
                    IsDeleted = false,
                    DepartmentId = 0,
                    DivisionId = 0,
                    CreatorId = 0,
                    BranchId = 0,
                    LastAccess = DateTime.Now,
                    TokenOn = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },

                new AppUser()
                {
                    Id = 2,
                    Email = "halinhofficial@gmail.com",
                    Password = hasher.HashPassword(null, "123456"),
                    Name = "HaLinh",
                    Dob = new DateTime(2002, 10, 25),
                    Gender = "Nam",
                    StaffId = 1,
                    Phone = "0342250348",
                    IsAdmin = false,
                    IsLocked = false,
                    IsDeleted = false,
                    DepartmentId = 0,
                    DivisionId = 0,
                    CreatorId = 0,
                    BranchId = 0,
                    LastAccess = DateTime.Now,
                    TokenOn = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            modelBuilder.Entity<Conversation>().HasData(
                new Conversation()
                {
                    Id = 1,
                    Title = "AdminRoom",
                    UserId = 1,
                    ChannelId = "adminChannel",
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            modelBuilder.Entity<Message>().HasData(
                new Message()
                {
                    Id = 1,
                    ConversationId = 1,
                    UserId = 1,
                    MessageType = 0,
                    Content = "This is Admin Room",
                    IsDeleted = false,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
            modelBuilder.Entity<Participant>().HasData(
                new Participant()
                {
                    ConversationId = 1,
                    UserId = 1,
                    IsAdmin = true,
                    IsJoined = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                },
                new Participant()
                {
                    ConversationId = 1,
                    UserId = 2,
                    IsAdmin = false,
                    IsJoined = true,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                });
        }
    }
}