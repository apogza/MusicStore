using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Data
{
    public class MusicStoreContext : DbContext
    {
        public MusicStoreContext(DbContextOptions<MusicStoreContext> options)
            :base(options)
        {
        }
        public DbSet<Song> Songs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Genre> Genres { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Song>().ToTable("Song");
            builder.Entity<Album>().ToTable("Album");
            builder.Entity<Artist>().ToTable("Artist");
            builder.Entity<Genre>().ToTable("Genre");
        }
    }
}