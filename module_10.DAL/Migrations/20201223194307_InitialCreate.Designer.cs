﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using module_10.DAL.DataAccess;

namespace module_10.DAL.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201223194307_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("module_10.DAL.Entities.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<bool>("HomeworkPresence")
                        .HasColumnType("bit");

                    b.Property<int?>("LectionId")
                        .HasColumnType("int");

                    b.Property<int>("Mark")
                        .HasColumnType("int");

                    b.Property<int?>("StudentId")
                        .HasColumnType("int");

                    b.Property<bool>("StudentPresence")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LectionId");

                    b.HasIndex("StudentId");

                    b.ToTable("Homeworks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 1,
                            Mark = 5,
                            StudentId = 1,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = false,
                            LectionId = 1,
                            Mark = 0,
                            StudentId = 2,
                            StudentPresence = false
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 1,
                            Mark = 4,
                            StudentId = 3,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 4,
                            Date = new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 1,
                            Mark = 5,
                            StudentId = 4,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 5,
                            Date = new DateTime(2020, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 1,
                            Mark = 5,
                            StudentId = 5,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 6,
                            Date = new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 2,
                            Mark = 5,
                            StudentId = 1,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 7,
                            Date = new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = false,
                            LectionId = 2,
                            Mark = 0,
                            StudentId = 2,
                            StudentPresence = false
                        },
                        new
                        {
                            Id = 8,
                            Date = new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 2,
                            Mark = 4,
                            StudentId = 3,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 9,
                            Date = new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = false,
                            LectionId = 2,
                            Mark = 0,
                            StudentId = 4,
                            StudentPresence = false
                        },
                        new
                        {
                            Id = 10,
                            Date = new DateTime(2020, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 2,
                            Mark = 5,
                            StudentId = 5,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 11,
                            Date = new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 3,
                            Mark = 4,
                            StudentId = 1,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 12,
                            Date = new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = false,
                            LectionId = 3,
                            Mark = 0,
                            StudentId = 2,
                            StudentPresence = false
                        },
                        new
                        {
                            Id = 13,
                            Date = new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 3,
                            Mark = 4,
                            StudentId = 3,
                            StudentPresence = true
                        },
                        new
                        {
                            Id = 14,
                            Date = new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = false,
                            LectionId = 3,
                            Mark = 0,
                            StudentId = 4,
                            StudentPresence = false
                        },
                        new
                        {
                            Id = 15,
                            Date = new DateTime(2020, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HomeworkPresence = true,
                            LectionId = 3,
                            Mark = 1,
                            StudentId = 5,
                            StudentPresence = true
                        });
                });

            modelBuilder.Entity("module_10.DAL.Entities.Lection", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("LecturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.ToTable("Lections");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            LecturerId = 1,
                            Name = "Maths"
                        },
                        new
                        {
                            Id = 2,
                            LecturerId = 1,
                            Name = "Physics"
                        },
                        new
                        {
                            Id = 3,
                            LecturerId = 3,
                            Name = "English"
                        });
                });

            modelBuilder.Entity("module_10.DAL.Entities.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Anastasia",
                            LastName = "Yarovikova"
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Vlad",
                            LastName = "Sinotov"
                        },
                        new
                        {
                            Id = 3,
                            FirstName = "Ilya",
                            LastName = "Maddyson"
                        });
                });

            modelBuilder.Entity("module_10.DAL.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<float>("AverageMark")
                        .HasColumnType("real");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("MissedLections")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AverageMark = 4.3f,
                            FirstName = "Eugene",
                            LastName = "Buchenkov",
                            MissedLections = 0
                        },
                        new
                        {
                            Id = 2,
                            AverageMark = 0f,
                            FirstName = "Kirill",
                            LastName = "Makarov",
                            MissedLections = 3
                        },
                        new
                        {
                            Id = 3,
                            AverageMark = 4.2f,
                            FirstName = "Mikhail",
                            LastName = "Eremin",
                            MissedLections = 0
                        },
                        new
                        {
                            Id = 4,
                            AverageMark = 1.5f,
                            FirstName = "Alexander",
                            LastName = "Nikitin",
                            MissedLections = 2
                        },
                        new
                        {
                            Id = 5,
                            AverageMark = 3.9f,
                            FirstName = "Ivan",
                            LastName = "Shkikavy",
                            MissedLections = 0
                        });
                });

            modelBuilder.Entity("module_10.DAL.Entities.Homework", b =>
                {
                    b.HasOne("module_10.DAL.Entities.Lection", "Lection")
                        .WithMany("LectionHomework")
                        .HasForeignKey("LectionId");

                    b.HasOne("module_10.DAL.Entities.Student", "Student")
                        .WithMany("StudentHomework")
                        .HasForeignKey("StudentId");

                    b.Navigation("Lection");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("module_10.DAL.Entities.Lection", b =>
                {
                    b.HasOne("module_10.DAL.Entities.Lecturer", "Lecturer")
                        .WithMany("Lections")
                        .HasForeignKey("LecturerId");

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("module_10.DAL.Entities.Lection", b =>
                {
                    b.Navigation("LectionHomework");
                });

            modelBuilder.Entity("module_10.DAL.Entities.Lecturer", b =>
                {
                    b.Navigation("Lections");
                });

            modelBuilder.Entity("module_10.DAL.Entities.Student", b =>
                {
                    b.Navigation("StudentHomework");
                });
#pragma warning restore 612, 618
        }
    }
}
