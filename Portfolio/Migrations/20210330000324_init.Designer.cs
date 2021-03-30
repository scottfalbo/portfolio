﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Portfolio.Data;

namespace Portfolio.Migrations
{
    [DbContext(typeof(PortfolioDbContext))]
    [Migration("20210330000324_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Portfolio.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeployedLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RepoLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SourceURL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Projects");

                    b.HasData(
                        new
                        {
                            Id = -1,
                            Description = "SmallBoi is a two player coop platform puzzle game built in Unity.  It has both local and network multiple player options using Photon.",
                            RepoLink = "https://github.com/AmeiliaAndTheSmallBois/SmallBoi/tree/main",
                            SourceURL = "images/smallboi.png",
                            Title = "SmallBoi, The Game"
                        },
                        new
                        {
                            Id = 2,
                            Description = "LiteBerry Pi allows users to create and send designs to a RaspBerry Pi with a matrix of led lights attached.  The app uses an api to create and save designs.  The api also contains a route to send designs to the Pi using a SignalR server.",
                            RepoLink = "https://github.com/Lite-Berry-pi/Lite-Berry-Pi",
                            SourceURL = "images/liteberrypi.png",
                            Title = "LiteBerry Pi"
                        },
                        new
                        {
                            Id = 3,
                            DeployedLink = "https://scottfalbo.github.io/react-minesweeper-v2/",
                            Description = "A re-creation of the Window's classic Minesweeper.  I built this to practice components and state within a React App.",
                            RepoLink = "https://github.com/scottfalbo/react-minesweeper-v2",
                            SourceURL = "images/minesweeper.png",
                            Title = "React Minesweeper"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
