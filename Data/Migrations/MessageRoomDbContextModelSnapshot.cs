﻿// <auto-generated />
using System;
using MessageRoomSolution.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MessageRoomDbContext))]
    partial class MessageRoomDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AboutMe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(1950));

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<int>("DivisionId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Dob")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .IsUnicode(false)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsLocked")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("LastAccess")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(1730));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("StaffId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TokenOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(2182));

                    b.HasKey("Id");

                    b.ToTable("AppUsers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BranchId = 0,
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3509),
                            CreatorId = 0,
                            DepartmentId = 0,
                            DivisionId = 0,
                            Dob = new DateTime(2021, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "admin@halinhit.com",
                            Gender = "Nam",
                            IsAdmin = true,
                            IsDeleted = false,
                            IsLocked = false,
                            LastAccess = new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3080),
                            Name = "Admin",
                            Password = "AQAAAAEAACcQAAAAEAOjluQuDk+kBgfoOOxVu7oEii7+nr3Root5D0Y+hDtwvqkgkM9txLVF2mnkLT8ldQ==",
                            Phone = "0123456789",
                            StaffId = 0,
                            TokenOn = new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3311),
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3700)
                        },
                        new
                        {
                            Id = 2,
                            BranchId = 0,
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7918),
                            CreatorId = 0,
                            DepartmentId = 0,
                            DivisionId = 0,
                            Dob = new DateTime(2002, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "halinhofficial@gmail.com",
                            Gender = "Nam",
                            IsAdmin = false,
                            IsDeleted = false,
                            IsLocked = false,
                            LastAccess = new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7899),
                            Name = "HaLinh",
                            Password = "AQAAAAEAACcQAAAAEE7r2ygrjfz95ARNK0M+gQUvOY4dv5UDgqMsNhPKVPD9TJzU1SNOD6LmcQmms75PFA==",
                            Phone = "0342250348",
                            StaffId = 1,
                            TokenOn = new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7909),
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7928)
                        });
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Attachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 573, DateTimeKind.Local).AddTicks(5895));

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MessageId")
                        .HasColumnType("int");

                    b.Property<string>("ThumbUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 572, DateTimeKind.Local).AddTicks(6103));

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.ToTable("Attachments");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Conversation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ChannelId")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 577, DateTimeKind.Local).AddTicks(3376));

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("Title")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 577, DateTimeKind.Local).AddTicks(3060));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Conversations");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChannelId = "adminChannel",
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(6907),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            Title = "AdminRoom",
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(7141),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:IdentityIncrement", 1)
                        .HasAnnotation("SqlServer:IdentitySeed", 1)
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 578, DateTimeKind.Local).AddTicks(9339));

                    b.Property<DateTime>("DeletedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDeleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<int>("MessageType")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 578, DateTimeKind.Local).AddTicks(8980));

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ConversationId");

                    b.HasIndex("UserId");

                    b.ToTable("Messages");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "This is Admin Room",
                            ConversationId = 1,
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(8697),
                            DeletedAt = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsDeleted = false,
                            MessageType = 0,
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(8900),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Participant", b =>
                {
                    b.Property<int>("ConversationId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 584, DateTimeKind.Local).AddTicks(2936));

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<bool>("IsJoined")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(new DateTime(2022, 4, 9, 22, 20, 32, 584, DateTimeKind.Local).AddTicks(3254));

                    b.HasKey("ConversationId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Participants");

                    b.HasData(
                        new
                        {
                            ConversationId = 1,
                            UserId = 1,
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(92),
                            IsAdmin = true,
                            IsJoined = true,
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(289)
                        },
                        new
                        {
                            ConversationId = 1,
                            UserId = 2,
                            CreatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(529),
                            IsAdmin = false,
                            IsJoined = true,
                            UpdatedAt = new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(537)
                        });
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Attachment", b =>
                {
                    b.HasOne("MessageRoomSolution.Data.Entities.Message", "Messages")
                        .WithMany("Attachments")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Messages");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Conversation", b =>
                {
                    b.HasOne("MessageRoomSolution.Data.Entities.AppUser", "User")
                        .WithMany("Conversations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Message", b =>
                {
                    b.HasOne("MessageRoomSolution.Data.Entities.Conversation", "Conversation")
                        .WithMany("Messages")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MessageRoomSolution.Data.Entities.AppUser", "User")
                        .WithMany("Messages")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Participant", b =>
                {
                    b.HasOne("MessageRoomSolution.Data.Entities.Conversation", "Conversation")
                        .WithMany("Participants")
                        .HasForeignKey("ConversationId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("MessageRoomSolution.Data.Entities.AppUser", "User")
                        .WithMany("Participants")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Conversation");

                    b.Navigation("User");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.AppUser", b =>
                {
                    b.Navigation("Conversations");

                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Conversation", b =>
                {
                    b.Navigation("Messages");

                    b.Navigation("Participants");
                });

            modelBuilder.Entity("MessageRoomSolution.Data.Entities.Message", b =>
                {
                    b.Navigation("Attachments");
                });
#pragma warning restore 612, 618
        }
    }
}
