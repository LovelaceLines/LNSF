using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LNSF.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Acronym = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Peoples",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    MaritalStatus = table.Column<int>(type: "INTEGER", nullable: false),
                    RaceColor = table.Column<int>(type: "INTEGER", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    RG = table.Column<string>(type: "TEXT", nullable: false),
                    IssuingBody = table.Column<string>(type: "TEXT", nullable: false),
                    CPF = table.Column<string>(type: "TEXT", nullable: false),
                    Street = table.Column<string>(type: "TEXT", nullable: false),
                    HouseNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Neighborhood = table.Column<string>(type: "TEXT", nullable: false),
                    City = table.Column<string>(type: "TEXT", nullable: false),
                    State = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Peoples", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NormalizedName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Bathroom = table.Column<bool>(type: "INTEGER", nullable: false),
                    Beds = table.Column<int>(type: "INTEGER", nullable: false),
                    Storey = table.Column<int>(type: "INTEGER", nullable: false),
                    Available = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Treatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Treatments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "TEXT", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: true),
                    SecurityStamp = table.Column<string>(type: "TEXT", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "INTEGER", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "TEXT", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    PeopleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Escorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PeopleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Escorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Escorts_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SocioeconomicRecord = table.Column<bool>(type: "INTEGER", nullable: false),
                    Term = table.Column<bool>(type: "INTEGER", nullable: false),
                    PeopleId = table.Column<int>(type: "INTEGER", nullable: false),
                    HospitalId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Hospitals_HospitalId",
                        column: x => x.HospitalId,
                        principalTable: "Hospitals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Output = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Input = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Note = table.Column<string>(type: "TEXT", nullable: false),
                    PeopleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tours_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    ClaimType = table.Column<string>(type: "TEXT", nullable: true),
                    ClaimValue = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderKey = table.Column<string>(type: "TEXT", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    LoginProvider = table.Column<string>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Value = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LogEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    EntityName = table.Column<string>(type: "TEXT", nullable: false),
                    EntityId = table.Column<int>(type: "INTEGER", nullable: false),
                    Action = table.Column<string>(type: "TEXT", nullable: false),
                    ValuesChanges = table.Column<string>(type: "TEXT", nullable: false),
                    LogDateTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogEntries_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyGroupProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Kinship = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Profession = table.Column<string>(type: "TEXT", nullable: false),
                    Income = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyGroupProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyGroupProfiles_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hostings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CheckIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CheckOut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Hostings_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientsTreatments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    TreatmentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientsTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientsTreatments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientsTreatments_Treatments_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "Treatments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    SocialProgram = table.Column<string>(type: "TEXT", nullable: false),
                    SocialProgramNote = table.Column<string>(type: "TEXT", nullable: false),
                    DomicileType = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialExternalWallsDomicile = table.Column<string>(type: "TEXT", nullable: false),
                    AccessElectricalEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    HasWaterSupply = table.Column<bool>(type: "INTEGER", nullable: false),
                    WayWaterSupply = table.Column<string>(type: "TEXT", nullable: false),
                    SanitaryDrainage = table.Column<int>(type: "INTEGER", nullable: false),
                    GarbageCollection = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberBedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberPeoplePerBedroom = table.Column<int>(type: "INTEGER", nullable: false),
                    DomicileHasAccessibility = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLocatedInRiskArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocatedInDifficultAccessArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocatedInConflictViolenceArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessToUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    AccessToUnitNote = table.Column<string>(type: "TEXT", nullable: false),
                    FirstAttendanceReason = table.Column<string>(type: "TEXT", nullable: false),
                    DemandPresented = table.Column<string>(type: "TEXT", nullable: false),
                    Referrals = table.Column<string>(type: "TEXT", nullable: false),
                    Observations = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HostingsEscorts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HostingId = table.Column<int>(type: "INTEGER", nullable: false),
                    EscortId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostingsEscorts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostingsEscorts_Escorts_EscortId",
                        column: x => x.EscortId,
                        principalTable: "Escorts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HostingsEscorts_Hostings_HostingId",
                        column: x => x.HostingId,
                        principalTable: "Hostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeoplesRoomsHostings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HostingId = table.Column<int>(type: "INTEGER", nullable: false),
                    PeopleId = table.Column<int>(type: "INTEGER", nullable: false),
                    RoomId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeoplesRoomsHostings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeoplesRoomsHostings_Hostings_HostingId",
                        column: x => x.HostingId,
                        principalTable: "Hostings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeoplesRoomsHostings_Peoples_PeopleId",
                        column: x => x.PeopleId,
                        principalTable: "Peoples",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeoplesRoomsHostings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedAt", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { 1, "50f9fdfe-576e-4bdb-a969-6ba4ce856f93", new DateTime(2024, 11, 4, 20, 59, 25, 196, DateTimeKind.Local).AddTicks(1983), "Desenvolvedor", "DESENVOLVEDOR" },
                    { 2, "75a5cb8d-eae0-422d-a406-09e12227cd71", new DateTime(2024, 11, 4, 20, 59, 25, 196, DateTimeKind.Local).AddTicks(2008), "Administrador", "ADMINISTRADOR" },
                    { 3, "365f8d0c-2f20-4dbb-9eaa-ee1456f0ce10", new DateTime(2024, 11, 4, 20, 59, 25, 196, DateTimeKind.Local).AddTicks(2014), "Assistente Social", "ASSISTENTESOCIAL" },
                    { 4, "2e54c106-ec32-4df4-bd43-184a058c0921", new DateTime(2024, 11, 4, 20, 59, 25, 196, DateTimeKind.Local).AddTicks(2034), "Secretário", "SECRETARIO" },
                    { 5, "7061de7c-30fd-427f-8372-bdee668a8a2f", new DateTime(2024, 11, 4, 20, 59, 25, 196, DateTimeKind.Local).AddTicks(2049), "Voluntário", "VOLUNTARIO" }
                });

            migrationBuilder.InsertData(
                table: "Treatments",
                columns: new[] { "Id", "Name", "Type" },
                values: new object[,]
                {
                    { 1, "Câncer", 0 },
                    { 2, "Pré-transplante", 1 },
                    { 3, "Pós-transplante", 2 },
                    { 4, "Outro", 3 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { 1, 0, "b3dfdbde-6f78-4b56-85a2-e785b81949c3", new DateTime(2024, 11, 4, 20, 59, 25, 276, DateTimeKind.Local).AddTicks(1823), "georgemaiaf@gmail.com", false, false, null, "George Maia", "GEORGEMAIAF@GMAIL.COM", "GEORGEDEV", "AQAAAAIAAYagAAAAEB/ozYGr3I/7kdNNlv7XN/kR2FbU9DyofuFIQjtBDEeH5nsxWKVgjV/MExGTPPatFw==", "(55) 88 9 9246-5315", false, "ac56f750-c53a-4130-9746-568236c5c0ab", false, "georgedev" },
                    { 2, 0, "2910f4e6-f889-421b-8a48-315357460a87", new DateTime(2024, 11, 4, 20, 59, 25, 360, DateTimeKind.Local).AddTicks(5525), "lnsf@gmail.com", false, false, null, "Lar Nossa Senhora de Fátima", "LNSF@GMAIL.COM", "LNSF", "AQAAAAIAAYagAAAAEBEAfMel/385e2vQLsxn2C3mezZb4zDR6fR5a36Ar340PEVi2QmDuxXj2/X9zRCvXA==", "(11) 11 1 1111-1111", false, "b087e6af-9ada-40ec-b06f-ef0246e59268", false, "lnsf" },
                    { 3, 0, "e93cbb23-8c83-4324-b51a-0cba744e1ba7", new DateTime(2024, 11, 4, 20, 59, 25, 463, DateTimeKind.Local).AddTicks(4775), "lnsf2@gmail.com", false, false, null, "Lar Nossa Senhora de Fátima 2", "LNSF2@GMAIL.COM", "LNSF2", "AQAAAAIAAYagAAAAEBBI8XkhqPlZMc73JK/iP1gmIGfvMoHvI9u6nBNes+T7x7VVRSeZh46pI633edLc3Q==", "(22) 22 2 2222-2222", false, "f9b7efbd-0978-4024-b1d1-1e9bda089c27", false, "lnsf2" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "Id", "RoleId", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 2, 2, 1 },
                    { 3, 3, 1 },
                    { 4, 2, 2 },
                    { 5, 5, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_PeopleId_Phone",
                table: "EmergencyContacts",
                columns: new[] { "PeopleId", "Phone" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Escorts_PeopleId",
                table: "Escorts",
                column: "PeopleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyGroupProfiles_PatientId",
                table: "FamilyGroupProfiles",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Hostings_PatientId",
                table: "Hostings",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_HostingsEscorts_EscortId",
                table: "HostingsEscorts",
                column: "EscortId");

            migrationBuilder.CreateIndex(
                name: "IX_HostingsEscorts_HostingId_EscortId",
                table: "HostingsEscorts",
                columns: new[] { "HostingId", "EscortId" });

            migrationBuilder.CreateIndex(
                name: "IX_LogEntries_UserId",
                table: "LogEntries",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_HospitalId",
                table: "Patients",
                column: "HospitalId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PeopleId",
                table: "Patients",
                column: "PeopleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientsTreatments_PatientId_TreatmentId",
                table: "PatientsTreatments",
                columns: new[] { "PatientId", "TreatmentId" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientsTreatments_TreatmentId",
                table: "PatientsTreatments",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_CPF",
                table: "Peoples",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Peoples_RG",
                table: "Peoples",
                column: "RG",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeoplesRoomsHostings_HostingId",
                table: "PeoplesRoomsHostings",
                column: "HostingId");

            migrationBuilder.CreateIndex(
                name: "IX_PeoplesRoomsHostings_PeopleId",
                table: "PeoplesRoomsHostings",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_PeoplesRoomsHostings_RoomId_PeopleId_HostingId",
                table: "PeoplesRoomsHostings",
                columns: new[] { "RoomId", "PeopleId", "HostingId" });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_Number",
                table: "Rooms",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_PatientId",
                table: "ServiceRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tours_PeopleId",
                table: "Tours",
                column: "PeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_Treatments_Name_Type",
                table: "Treatments",
                columns: new[] { "Name", "Type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId_RoleId",
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" });

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "FamilyGroupProfiles");

            migrationBuilder.DropTable(
                name: "HostingsEscorts");

            migrationBuilder.DropTable(
                name: "LogEntries");

            migrationBuilder.DropTable(
                name: "PatientsTreatments");

            migrationBuilder.DropTable(
                name: "PeoplesRoomsHostings");

            migrationBuilder.DropTable(
                name: "ServiceRecords");

            migrationBuilder.DropTable(
                name: "Tours");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Escorts");

            migrationBuilder.DropTable(
                name: "Treatments");

            migrationBuilder.DropTable(
                name: "Hostings");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropTable(
                name: "Peoples");
        }
    }
}
