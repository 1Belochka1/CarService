﻿// <auto-generated />
using System;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CarService.Infrastructure.Migrations
{
    [DbContext(typeof(CarServiceDbContext))]
    [Migration("20240607082127_UpdateRelationsUserRecord3")]
    partial class UpdateRelationsUserRecord3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "record_priority", new[] { "low", "normal", "high", "very_high" });
            NpgsqlModelBuilderExtensions.HasPostgresEnum(modelBuilder, "record_status", new[] { "new", "processing", "awaiting", "work", "done" });
            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CarService.Core.Images.Image", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<byte[]>("Data")
                        .HasColumnType("bytea");

                    b.Property<string>("FileName")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("UserInfoId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("CarService.Core.Records.CalendarRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("ServiceId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ServiceId")
                        .IsUnique();

                    b.ToTable("CalendarRecords");
                });

            modelBuilder.Entity("CarService.Core.Records.DayRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CalendarId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("IsWeekend")
                        .HasColumnType("boolean");

                    b.Property<short>("Offset")
                        .HasColumnType("smallint");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CalendarId");

                    b.ToTable("DaysRecords");
                });

            modelBuilder.Entity("CarService.Core.Records.Record", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("CarInfo")
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompleteTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<bool>("IsTransferred")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<Guid?>("TimeRecordId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("VisitTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.ToTable("Records");
                });

            modelBuilder.Entity("CarService.Core.Records.TimeRecord", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ClientId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("DayRecordId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("EndTime")
                        .HasColumnType("time without time zone");

                    b.Property<bool>("IsBusy")
                        .HasColumnType("boolean");

                    b.Property<Guid?>("RecordId")
                        .HasColumnType("uuid");

                    b.Property<TimeOnly>("StartTime")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("DayRecordId");

                    b.HasIndex("RecordId")
                        .IsUnique();

                    b.ToTable("TimesRecords");
                });

            modelBuilder.Entity("CarService.Core.Services.Service", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CalendarId")
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsShowLending")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)");

                    b.HasKey("Id");

                    b.HasIndex("ImageId")
                        .IsUnique();

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Services");
                });

            modelBuilder.Entity("CarService.Core.Services.ServiceType", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("ServiceTypes");
                });

            modelBuilder.Entity("CarService.Core.Users.Role", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Master"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Client"
                        });
                });

            modelBuilder.Entity("CarService.Core.Users.UserAuth", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(1);

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.HasIndex("RoleId");

                    b.ToTable("UserAuths");
                });

            modelBuilder.Entity("CarService.Core.Users.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasMaxLength(300)
                        .HasColumnType("character varying(300)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid?>("ImageId")
                        .HasColumnType("uuid");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Patronymic")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("character varying(12)");

                    b.HasKey("Id");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("MastersServices", b =>
                {
                    b.Property<Guid>("MastersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServicesId")
                        .HasColumnType("uuid");

                    b.HasKey("MastersId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("MastersServices");
                });

            modelBuilder.Entity("RecordsMasters", b =>
                {
                    b.Property<Guid>("MastersId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("WorksId")
                        .HasColumnType("uuid");

                    b.HasKey("MastersId", "WorksId");

                    b.HasIndex("WorksId");

                    b.ToTable("RecordsMasters");
                });

            modelBuilder.Entity("RecordsServices", b =>
                {
                    b.Property<Guid>("RecordsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServicesId")
                        .HasColumnType("uuid");

                    b.HasKey("RecordsId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("RecordsServices");
                });

            modelBuilder.Entity("ServiceTypesServices", b =>
                {
                    b.Property<Guid>("ServiceTypesId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ServicesId")
                        .HasColumnType("uuid");

                    b.HasKey("ServiceTypesId", "ServicesId");

                    b.HasIndex("ServicesId");

                    b.ToTable("ServiceTypesServices");
                });

            modelBuilder.Entity("CarService.Core.Images.Image", b =>
                {
                    b.HasOne("CarService.Core.Users.UserInfo", "UserInfo")
                        .WithOne("Image")
                        .HasForeignKey("CarService.Core.Images.Image", "UserInfoId");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("CarService.Core.Records.CalendarRecord", b =>
                {
                    b.HasOne("CarService.Core.Services.Service", "Service")
                        .WithOne("Calendar")
                        .HasForeignKey("CarService.Core.Records.CalendarRecord", "ServiceId");

                    b.Navigation("Service");
                });

            modelBuilder.Entity("CarService.Core.Records.DayRecord", b =>
                {
                    b.HasOne("CarService.Core.Records.CalendarRecord", "Calendar")
                        .WithMany("DaysRecords")
                        .HasForeignKey("CalendarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("CarService.Core.Records.Record", b =>
                {
                    b.HasOne("CarService.Core.Users.UserInfo", "Client")
                        .WithMany("Records")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CarService.Core.Records.TimeRecord", b =>
                {
                    b.HasOne("CarService.Core.Users.UserInfo", "Client")
                        .WithMany("TimeRecords")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("CarService.Core.Records.DayRecord", "DayRecord")
                        .WithMany("TimeRecords")
                        .HasForeignKey("DayRecordId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Records.Record", "Record")
                        .WithOne("TimeRecord")
                        .HasForeignKey("CarService.Core.Records.TimeRecord", "RecordId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Client");

                    b.Navigation("DayRecord");

                    b.Navigation("Record");
                });

            modelBuilder.Entity("CarService.Core.Services.Service", b =>
                {
                    b.HasOne("CarService.Core.Images.Image", "Image")
                        .WithOne("Service")
                        .HasForeignKey("CarService.Core.Services.Service", "ImageId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Image");
                });

            modelBuilder.Entity("CarService.Core.Users.UserAuth", b =>
                {
                    b.HasOne("CarService.Core.Users.UserInfo", "UserInfo")
                        .WithOne("UserAuth")
                        .HasForeignKey("CarService.Core.Users.UserAuth", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Users.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("MastersServices", b =>
                {
                    b.HasOne("CarService.Core.Users.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("MastersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Services.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecordsMasters", b =>
                {
                    b.HasOne("CarService.Core.Users.UserAuth", null)
                        .WithMany()
                        .HasForeignKey("MastersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Records.Record", null)
                        .WithMany()
                        .HasForeignKey("WorksId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RecordsServices", b =>
                {
                    b.HasOne("CarService.Core.Records.Record", null)
                        .WithMany()
                        .HasForeignKey("RecordsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Services.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceTypesServices", b =>
                {
                    b.HasOne("CarService.Core.Services.ServiceType", null)
                        .WithMany()
                        .HasForeignKey("ServiceTypesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarService.Core.Services.Service", null)
                        .WithMany()
                        .HasForeignKey("ServicesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarService.Core.Images.Image", b =>
                {
                    b.Navigation("Service");
                });

            modelBuilder.Entity("CarService.Core.Records.CalendarRecord", b =>
                {
                    b.Navigation("DaysRecords");
                });

            modelBuilder.Entity("CarService.Core.Records.DayRecord", b =>
                {
                    b.Navigation("TimeRecords");
                });

            modelBuilder.Entity("CarService.Core.Records.Record", b =>
                {
                    b.Navigation("TimeRecord");
                });

            modelBuilder.Entity("CarService.Core.Services.Service", b =>
                {
                    b.Navigation("Calendar");
                });

            modelBuilder.Entity("CarService.Core.Users.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("CarService.Core.Users.UserInfo", b =>
                {
                    b.Navigation("Image");

                    b.Navigation("Records");

                    b.Navigation("TimeRecords");

                    b.Navigation("UserAuth");
                });
#pragma warning restore 612, 618
        }
    }
}
