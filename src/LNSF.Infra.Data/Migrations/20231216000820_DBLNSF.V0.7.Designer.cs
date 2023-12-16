﻿// <auto-generated />
using System;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20231216000820_DBLNSF.V0.7")]
    partial class DBLNSFV07
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("LNSF.Domain.Entities.EmergencyContact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PeopleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PeopleId", "Phone")
                        .IsUnique();

                    b.ToTable("EmergencyContacts");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Escort", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeopleId")
                        .IsUnicode(true)
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PeopleId")
                        .IsUnique();

                    b.ToTable("Escorts");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Acronym")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Hospitals");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Hosting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CheckIn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CheckOut")
                        .HasColumnType("TEXT");

                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("Hostings");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.HostingEscort", b =>
                {
                    b.Property<int>("HostingId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("EscortId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HostingId", "EscortId");

                    b.HasIndex("EscortId");

                    b.ToTable("HostingsEscorts");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Patient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("HospitalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeopleId")
                        .IsUnicode(true)
                        .HasColumnType("INTEGER");

                    b.Property<bool>("SocioeconomicRecord")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Term")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("PeopleId")
                        .IsUnique();

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.PatientTreatment", b =>
                {
                    b.Property<int>("PatientId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TreatmentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PatientId", "TreatmentId");

                    b.HasIndex("TreatmentId");

                    b.ToTable("PatientsTreatments");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.People", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("CPF")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("HouseNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Neighborhood")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RG")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CPF")
                        .IsUnique();

                    b.HasIndex("RG")
                        .IsUnique();

                    b.ToTable("Peoples");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.PeopleRoom", b =>
                {
                    b.Property<int>("RoomId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PeopleId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HostingId")
                        .HasColumnType("INTEGER");

                    b.HasKey("RoomId", "PeopleId", "HostingId");

                    b.HasIndex("HostingId");

                    b.HasIndex("PeopleId");

                    b.ToTable("PeoplesRooms");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Available")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Bathroom")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Beds")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Storey")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Number")
                        .IsUnique();

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Tour", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Input")
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Output")
                        .HasColumnType("TEXT");

                    b.Property<int>("PeopleId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PeopleId");

                    b.ToTable("Tours");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Treatment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Name", "Type")
                        .IsUnique();

                    b.ToTable("Treatments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            ConcurrencyStamp = "586b22b9-bf85-4d1f-b7ae-11eac16cc9a3",
                            Name = "Desenvolvedor",
                            NormalizedName = "DESENVOLVEDOR"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "d185422f-b039-4701-b306-be491c9ca978",
                            Name = "Administrador",
                            NormalizedName = "ADMINISTRADOR"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "0a5ab808-d8fc-4d56-b03e-8ede5177105d",
                            Name = "Assistente Social",
                            NormalizedName = "ASSISTENTESOCIAL"
                        },
                        new
                        {
                            Id = "4",
                            ConcurrencyStamp = "df691bc0-0525-4221-8c59-3a1497cccd13",
                            Name = "Secretário",
                            NormalizedName = "SECRETARIO"
                        },
                        new
                        {
                            Id = "5",
                            ConcurrencyStamp = "e408633f-9a91-4c2e-a5e2-203695b376d7",
                            Name = "Voluntário",
                            NormalizedName = "VOLUNTARIO"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "16c404ef-64db-41f9-b246-48bcf5552969",
                            Email = "georgemaiaf@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GEORGEMAIAF@GMAIL.COM",
                            NormalizedUserName = "GEORGEDEV",
                            PasswordHash = "AQAAAAIAAYagAAAAEEthRfYKzlvxcXVURojClgyAb6KHFtNG5zqiDM4bhiMAtU6b8V3OViRWGaXSfsIYcw==",
                            PhoneNumber = "(55) 88 9 9246-5315",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "93a6584d-07f1-400e-a959-0fe0e6cb2a00",
                            TwoFactorEnabled = false,
                            UserName = "georgedev"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "25546e0a-bba0-4aa5-a3ee-3ce00c7c7f13",
                            Email = "lnsf@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "LNSF@GMAIL.COM",
                            NormalizedUserName = "LNSF",
                            PasswordHash = "AQAAAAIAAYagAAAAEHlZ2mwH2IOnqY3pM5lpgVQnU7PHvM6EJaCmXYuvcATycC6s5fwjx+NZIxwn3yWn9A==",
                            PhoneNumber = "(11) 11 1 1111-1111",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "478c7c0f-1342-4a65-ba47-44a0d4e253ac",
                            TwoFactorEnabled = false,
                            UserName = "lnsf"
                        },
                        new
                        {
                            Id = "3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "95cc53b0-8ca0-4bf8-971e-512b23a7b7e0",
                            Email = "lnsf2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "LNSF2@GMAIL.COM",
                            NormalizedUserName = "LNSF2",
                            PasswordHash = "AQAAAAIAAYagAAAAEIVZ9qq1KANd6Q/gx83GQ1l2VYKb/j4SjbeG9IAQUuZzVP56ZCGYnfKHYOhmB3eECg==",
                            PhoneNumber = "(22) 22 2 2222-2222",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e1ee8b88-4383-4dbd-a2c2-590023535004",
                            TwoFactorEnabled = false,
                            UserName = "lnsf2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "1"
                        },
                        new
                        {
                            UserId = "2",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "3",
                            RoleId = "5"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("LNSF.Domain.Entities.EmergencyContact", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Escort", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.People", "People")
                        .WithOne()
                        .HasForeignKey("LNSF.Domain.Entities.Escort", "PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Hosting", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.HostingEscort", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.Escort", "Escort")
                        .WithMany()
                        .HasForeignKey("EscortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LNSF.Domain.Entities.Hosting", "Hosting")
                        .WithMany()
                        .HasForeignKey("HostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Escort");

                    b.Navigation("Hosting");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Patient", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.Hospital", "Hospital")
                        .WithMany()
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LNSF.Domain.Entities.People", "People")
                        .WithOne()
                        .HasForeignKey("LNSF.Domain.Entities.Patient", "PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");

                    b.Navigation("People");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.PatientTreatment", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LNSF.Domain.Entities.Treatment", "Treatment")
                        .WithMany()
                        .HasForeignKey("TreatmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");

                    b.Navigation("Treatment");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.PeopleRoom", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.Hosting", "Hosting")
                        .WithMany()
                        .HasForeignKey("HostingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LNSF.Domain.Entities.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LNSF.Domain.Entities.Room", "Room")
                        .WithMany()
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hosting");

                    b.Navigation("People");

                    b.Navigation("Room");
                });

            modelBuilder.Entity("LNSF.Domain.Entities.Tour", b =>
                {
                    b.HasOne("LNSF.Domain.Entities.People", "People")
                        .WithMany()
                        .HasForeignKey("PeopleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("People");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
