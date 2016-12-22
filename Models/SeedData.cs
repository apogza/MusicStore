using System;
using System.Linq;
using MusicStore.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace MusicStore.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var dbContext = 
                    new MusicStoreContext(serviceProvider.GetRequiredService<DbContextOptions<MusicStoreContext>>()))
            {
                InitGenres(dbContext);
                InitArtists(dbContext);

                dbContext.SaveChanges();
            }
        }

        private static void InitGenres(MusicStoreContext dbContext)
        {
            if(dbContext.Genres.Any())
            {
                return;
            }

            dbContext.AddRange(
                new Genre { Label = "Rock" },
                new Genre { Label = "Hard Rock" },
                new Genre { Label = "Progressive" },
                new Genre { Label = "Jazz" },
                new Genre { Label = "Ethno" }
            );             
        }

        private static void InitArtists(MusicStoreContext dbContext)
        {
            if(dbContext.Artists.Any())
            {
                return;
            }

            dbContext.AddRange(
                new Artist { Name = "Led Zeppelin", Description = "The best hard rock band of all time" },
                new Artist { Name = "Deep Purple", 
                    Description = "Could also be considered the best hard rock band of all time" }
            );
        }
    }
}