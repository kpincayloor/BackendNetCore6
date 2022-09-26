﻿// <auto-generated />
using System;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace testBackend.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20220923060107_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DataAccess.Entity.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApellidoEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Employee Surname");

                    b.Property<DateTimeOffset>("FechaPermiso")
                        .HasColumnType("datetimeoffset")
                        .HasComment("Permission granted on Date");

                    b.Property<string>("NombreEmpleado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Employee Forename");

                    b.Property<int>("TipoPermisoId")
                        .HasColumnType("int")
                        .HasComment("Permission Type");

                    b.HasKey("Id");

                    b.HasIndex("TipoPermisoId");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("DataAccess.Entity.PermissionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasComment("Permission description");

                    b.HasKey("Id");

                    b.ToTable("PermissionType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Descripcion = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Descripcion = "User"
                        });
                });

            modelBuilder.Entity("DataAccess.Entity.Permission", b =>
                {
                    b.HasOne("DataAccess.Entity.PermissionType", "TipoPermiso")
                        .WithMany("Permission")
                        .HasForeignKey("TipoPermisoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("TipoPermiso");
                });

            modelBuilder.Entity("DataAccess.Entity.PermissionType", b =>
                {
                    b.Navigation("Permission");
                });
#pragma warning restore 612, 618
        }
    }
}
