﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using geesRecorderClient.Server.Data;

namespace geesRecorderClient.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("AttendanceProjectPerson", b =>
                {
                    b.Property<int>("AttendanceProjectsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PersonsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AttendanceProjectsId", "PersonsId");

                    b.HasIndex("PersonsId");

                    b.ToTable("AttendanceProjectPerson");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AttendanceProjectId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("End")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Start")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AttendanceProjectId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FingerprintIds")
                        .HasColumnType("TEXT");

                    b.Property<int>("FirstName")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.PersonEvent", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EventId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PersonId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeIn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TimeOut")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("PersonId");

                    b.ToTable("PersonEvents");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasDiscriminator<int>("Type").HasValue(0);
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.ServerState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("AccessToken")
                        .HasColumnType("TEXT");

                    b.Property<string>("LockedRoute")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LoggedIn")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Pin")
                        .HasColumnType("TEXT");

                    b.Property<bool>("RouteLockActivated")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("ServerState");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.AttendanceProject", b =>
                {
                    b.HasBaseType("geesRecorderClient.Shared.Models.Project");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.DataCollectionProject", b =>
                {
                    b.HasBaseType("geesRecorderClient.Shared.Models.Project");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("AttendanceProjectPerson", b =>
                {
                    b.HasOne("geesRecorderClient.Shared.Models.AttendanceProject", null)
                        .WithMany()
                        .HasForeignKey("AttendanceProjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("geesRecorderClient.Shared.Models.Person", null)
                        .WithMany()
                        .HasForeignKey("PersonsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.Event", b =>
                {
                    b.HasOne("geesRecorderClient.Shared.Models.AttendanceProject", "AttendanceProject")
                        .WithMany("Events")
                        .HasForeignKey("AttendanceProjectId");

                    b.Navigation("AttendanceProject");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.PersonEvent", b =>
                {
                    b.HasOne("geesRecorderClient.Shared.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("geesRecorderClient.Shared.Models.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId");

                    b.Navigation("Event");

                    b.Navigation("Person");
                });

            modelBuilder.Entity("geesRecorderClient.Shared.Models.AttendanceProject", b =>
                {
                    b.Navigation("Events");
                });
#pragma warning restore 612, 618
        }
    }
}
