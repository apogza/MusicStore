using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MusicStore.Data;

namespace MusicStore.Migrations
{
    [DbContext(typeof(MusicStoreContext))]
    [Migration("20161222200308_InitDatabase")]
    partial class InitDatabase
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("MusicStore.Models.Album", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArtistID");

                    b.Property<int>("GenreID");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("YearRelease");

                    b.HasKey("ID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("GenreID");

                    b.ToTable("Album");
                });

            modelBuilder.Entity("MusicStore.Models.Artist", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Artist");
                });

            modelBuilder.Entity("MusicStore.Models.Genre", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label")
                        .IsRequired();

                    b.HasKey("ID");

                    b.ToTable("Genre");
                });

            modelBuilder.Entity("MusicStore.Models.Song", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AlbumId");

                    b.Property<int>("Minutes");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("Seconds");

                    b.Property<int>("TrackNumber");

                    b.HasKey("ID");

                    b.HasIndex("AlbumId");

                    b.ToTable("Song");
                });

            modelBuilder.Entity("MusicStore.Models.Album", b =>
                {
                    b.HasOne("MusicStore.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MusicStore.Models.Genre", "Genre")
                        .WithMany("Albums")
                        .HasForeignKey("GenreID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MusicStore.Models.Song", b =>
                {
                    b.HasOne("MusicStore.Models.Album", "Album")
                        .WithMany("Songs")
                        .HasForeignKey("AlbumId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
