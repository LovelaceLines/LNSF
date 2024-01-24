﻿// <auto-generated />
using System;
using LNSF.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("IssuingBody")
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
                            ConcurrencyStamp = "dfcf4dee-2d4f-49fb-97e0-6a721adb06fe",
                            Name = "Desenvolvedor",
                            NormalizedName = "DESENVOLVEDOR"
                        },
                        new
                        {
                            Id = "2",
                            ConcurrencyStamp = "50a6d35f-7765-4a51-8e11-f2ca075df885",
                            Name = "Administrador",
                            NormalizedName = "ADMINISTRADOR"
                        },
                        new
                        {
                            Id = "3",
                            ConcurrencyStamp = "65175c0f-5cfd-4217-b31e-687ccd84a20b",
                            Name = "AssistenteSocial",
                            NormalizedName = "ASSISTENTESOCIAL"
                        },
                        new
                        {
                            Id = "4",
                            ConcurrencyStamp = "13f0dfe6-7bf9-4193-a1e0-81beecbaf62e",
                            Name = "Secretario",
                            NormalizedName = "SECRETARIO"
                        },
                        new
                        {
                            Id = "5",
                            ConcurrencyStamp = "b9488930-7bb9-41aa-acfa-b30b3e4cca99",
                            Name = "Voluntario",
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
                            ConcurrencyStamp = "699053af-3068-4c03-bb62-b7fa4b47be7d",
                            Email = "georgemaiaf@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "GEORGEMAIAF@GMAIL.COM",
                            NormalizedUserName = "GEORGEDEV",
                            PasswordHash = "AQAAAAIAAYagAAAAEAJWhw7eb3ZKjmfSCp5ZGLfmcDYkNACE2zglCQHgzfQUqAHqK3kYFh0zd/ktyQPRyA==",
                            PhoneNumber = "(55) 88 9 9246-5315",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "391f5300-6337-4f93-a108-aa77f867f11e",
                            TwoFactorEnabled = false,
                            UserName = "georgedev"
                        },
                        new
                        {
                            Id = "2",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "47bc1a23-69a7-4b28-b5ab-335cae8910c5",
                            Email = "lnsf@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "LNSF@GMAIL.COM",
                            NormalizedUserName = "LNSF",
                            PasswordHash = "AQAAAAIAAYagAAAAEHvNWSiLtU8RyxRtmatiXINsFNwlXw08TXwFvOepKdy4DZBANTEmvYlRh1/nLY4kWg==",
                            PhoneNumber = "(11) 11 1 1111-1111",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9c9229b9-12e0-46b8-a864-2eaa7de71f3c",
                            TwoFactorEnabled = false,
                            UserName = "lnsf"
                        },
                        new
                        {
                            Id = "3",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2dc9eead-3af0-4cec-bf59-1e99f2440d07",
                            Email = "lnsf2@gmail.com",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedEmail = "LNSF2@GMAIL.COM",
                            NormalizedUserName = "LNSF2",
                            PasswordHash = "AQAAAAIAAYagAAAAEGw0Sm6LU8fiRsu5hSdZzaoPLSVudKXVmxwE0gk1I83kdCBeG0T1LDsIKptZMoKlXg==",
                            PhoneNumber = "(22) 22 2 2222-2222",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e3434693-6603-476f-948c-7a34aa1b28ed",
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
                            UserId = "1",
                            RoleId = "2"
                        },
                        new
                        {
                            UserId = "1",
                            RoleId = "3"
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
