using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DivisionId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TokenOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastAccess = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(1730)),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(1950)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 587, DateTimeKind.Local).AddTicks(2182))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ChannelId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Background = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 577, DateTimeKind.Local).AddTicks(3376)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 577, DateTimeKind.Local).AddTicks(3060)),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conversations_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MessageType = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 578, DateTimeKind.Local).AddTicks(9339)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 578, DateTimeKind.Local).AddTicks(8980)),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ConversationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsJoined = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 584, DateTimeKind.Local).AddTicks(2936)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 584, DateTimeKind.Local).AddTicks(3254))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => new { x.ConversationId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Participants_AppUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Participants_Conversations_ConversationId",
                        column: x => x.ConversationId,
                        principalTable: "Conversations",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Attachments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ThumbUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 573, DateTimeKind.Local).AddTicks(5895)),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValue: new DateTime(2022, 4, 9, 22, 20, 32, 572, DateTimeKind.Local).AddTicks(6103)),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attachments_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AboutMe", "Avatar", "BranchId", "CreatedAt", "CreatorId", "DepartmentId", "DivisionId", "Dob", "Email", "Gender", "IsAdmin", "LastAccess", "Name", "Password", "Phone", "StaffId", "Token", "TokenOn", "UpdatedAt" },
                values: new object[] { 1, null, null, 0, new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3509), 0, 0, 0, new DateTime(2021, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@halinhit.com", "Nam", true, new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3080), "Admin", "AQAAAAEAACcQAAAAEAOjluQuDk+kBgfoOOxVu7oEii7+nr3Root5D0Y+hDtwvqkgkM9txLVF2mnkLT8ldQ==", "0123456789", 0, null, new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3311), new DateTime(2022, 4, 9, 22, 20, 32, 668, DateTimeKind.Local).AddTicks(3700) });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AboutMe", "Avatar", "BranchId", "CreatedAt", "CreatorId", "DepartmentId", "DivisionId", "Dob", "Email", "Gender", "LastAccess", "Name", "Password", "Phone", "StaffId", "Token", "TokenOn", "UpdatedAt" },
                values: new object[] { 2, null, null, 0, new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7918), 0, 0, 0, new DateTime(2002, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "halinhofficial@gmail.com", "Nam", new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7899), "HaLinh", "AQAAAAEAACcQAAAAEE7r2ygrjfz95ARNK0M+gQUvOY4dv5UDgqMsNhPKVPD9TJzU1SNOD6LmcQmms75PFA==", "0342250348", 1, null, new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7909), new DateTime(2022, 4, 9, 22, 20, 32, 673, DateTimeKind.Local).AddTicks(7928) });

            migrationBuilder.InsertData(
                table: "Conversations",
                columns: new[] { "Id", "Avatar", "Background", "ChannelId", "CreatedAt", "DeletedAt", "Description", "Title", "UpdatedAt", "UserId" },
                values: new object[] { 1, null, null, "adminChannel", new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(6907), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AdminRoom", new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(7141), 1 });

            migrationBuilder.InsertData(
                table: "Messages",
                columns: new[] { "Id", "Content", "ConversationId", "CreatedAt", "DeletedAt", "MessageType", "UpdatedAt", "UserId" },
                values: new object[] { 1, "This is Admin Room", 1, new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(8697), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, new DateTime(2022, 4, 9, 22, 20, 32, 674, DateTimeKind.Local).AddTicks(8900), 1 });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ConversationId", "UserId", "CreatedAt", "IsAdmin", "IsJoined", "NickName", "UpdatedAt" },
                values: new object[] { 1, 1, new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(92), true, true, null, new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(289) });

            migrationBuilder.InsertData(
                table: "Participants",
                columns: new[] { "ConversationId", "UserId", "CreatedAt", "IsJoined", "NickName", "UpdatedAt" },
                values: new object[] { 1, 2, new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(529), true, null, new DateTime(2022, 4, 9, 22, 20, 32, 675, DateTimeKind.Local).AddTicks(537) });

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_MessageId",
                table: "Attachments",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_UserId",
                table: "Conversations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ConversationId",
                table: "Messages",
                column: "ConversationId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_UserId",
                table: "Messages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_UserId",
                table: "Participants",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attachments");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Conversations");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
