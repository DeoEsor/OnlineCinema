﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineCinema.DAL;

#nullable disable

namespace OnlineCinema.DAL.Migrations.Films
{
    [DbContext(typeof(FilmsContext))]
    partial class FilmsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnlineCinema.Domain.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<int?>("FilmId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int?>("SerialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("FilmId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("SerialId");

                    b.ToTable("Actors");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Director", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<int?>("FilmId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int?>("SerialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("FilmId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("SerialId");

                    b.ToTable("Directors");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Episode", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EpisodeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EpisodeNumber")
                        .HasColumnType("int");

                    b.Property<float>("IMDbRaiting")
                        .HasColumnType("real");

                    b.Property<string>("MagnetLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseData")
                        .HasColumnType("datetime2");

                    b.Property<float>("RottenTomatoesRaiting")
                        .HasColumnType("real");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("int");

                    b.Property<string>("SerialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SeasonId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Film", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FilmName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genres")
                        .HasColumnType("int");

                    b.Property<float>("IMDbRaiting")
                        .HasColumnType("real");

                    b.Property<string>("MagnetLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseData")
                        .HasColumnType("datetime2");

                    b.Property<float>("RottenTomatoesRaiting")
                        .HasColumnType("real");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Films");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Season", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("IMDbRaiting")
                        .HasColumnType("real");

                    b.Property<string>("MagnetLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseData")
                        .HasColumnType("datetime2");

                    b.Property<float>("RottenTomatoesRaiting")
                        .HasColumnType("real");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SeasonName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SeasonNumber")
                        .HasColumnType("int");

                    b.Property<int?>("SerialId")
                        .HasColumnType("int");

                    b.Property<string>("SerialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("SerialId");

                    b.ToTable("Seasons");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Serial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AverageRuntime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Genres")
                        .HasColumnType("int");

                    b.Property<float>("IMDbRaiting")
                        .HasColumnType("real");

                    b.Property<string>("MagnetLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PosterSource")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ReleaseData")
                        .HasColumnType("datetime2");

                    b.Property<float>("RottenTomatoesRaiting")
                        .HasColumnType("real");

                    b.Property<string>("SerialName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Serials");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Writer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<int?>("EpisodeId")
                        .HasColumnType("int");

                    b.Property<int?>("FilmId")
                        .HasColumnType("int");

                    b.Property<string>("PersonalName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SeasonId")
                        .HasColumnType("int");

                    b.Property<int?>("SerialId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("FilmId");

                    b.HasIndex("SeasonId");

                    b.HasIndex("SerialId");

                    b.ToTable("Writers");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Actor", b =>
                {
                    b.HasOne("OnlineCinema.Domain.Episode", null)
                        .WithMany("Cast")
                        .HasForeignKey("EpisodeId");

                    b.HasOne("OnlineCinema.Domain.Film", null)
                        .WithMany("Cast")
                        .HasForeignKey("FilmId");

                    b.HasOne("OnlineCinema.Domain.Season", null)
                        .WithMany("Cast")
                        .HasForeignKey("SeasonId");

                    b.HasOne("OnlineCinema.Domain.Serial", null)
                        .WithMany("Cast")
                        .HasForeignKey("SerialId");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Director", b =>
                {
                    b.HasOne("OnlineCinema.Domain.Episode", null)
                        .WithMany("Directors")
                        .HasForeignKey("EpisodeId");

                    b.HasOne("OnlineCinema.Domain.Film", null)
                        .WithMany("Directors")
                        .HasForeignKey("FilmId");

                    b.HasOne("OnlineCinema.Domain.Season", null)
                        .WithMany("Directors")
                        .HasForeignKey("SeasonId");

                    b.HasOne("OnlineCinema.Domain.Serial", null)
                        .WithMany("Directors")
                        .HasForeignKey("SerialId");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Episode", b =>
                {
                    b.HasOne("OnlineCinema.Domain.Season", null)
                        .WithMany("Episodes")
                        .HasForeignKey("SeasonId");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Season", b =>
                {
                    b.HasOne("OnlineCinema.Domain.Serial", null)
                        .WithMany("Seasons")
                        .HasForeignKey("SerialId");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Writer", b =>
                {
                    b.HasOne("OnlineCinema.Domain.Episode", null)
                        .WithMany("Writers")
                        .HasForeignKey("EpisodeId");

                    b.HasOne("OnlineCinema.Domain.Film", null)
                        .WithMany("Writers")
                        .HasForeignKey("FilmId");

                    b.HasOne("OnlineCinema.Domain.Season", null)
                        .WithMany("Writers")
                        .HasForeignKey("SeasonId");

                    b.HasOne("OnlineCinema.Domain.Serial", null)
                        .WithMany("Writers")
                        .HasForeignKey("SerialId");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Episode", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("Directors");

                    b.Navigation("Writers");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Film", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("Directors");

                    b.Navigation("Writers");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Season", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("Directors");

                    b.Navigation("Episodes");

                    b.Navigation("Writers");
                });

            modelBuilder.Entity("OnlineCinema.Domain.Serial", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("Directors");

                    b.Navigation("Seasons");

                    b.Navigation("Writers");
                });
#pragma warning restore 612, 618
        }
    }
}