using Microsoft.EntityFrameworkCore;

namespace MovieApp.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MovieContext>>()))
        {
            if (context.Movie.Any())
            {
                return;
            }

            context.Movie.AddRange(
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    ReleaseDate = DateTime.Parse("1994-10-14"),
                    Genre = "Drama",
                    Rating = "R",
                    Price = 7.99M,
                    ReleaseYear = 1994
                },
                new Movie
                {
                    Title = "Inception",
                    ReleaseDate = DateTime.Parse("2010-07-16"),
                    Genre = "Science Fiction",
                    Rating = "PG-13",
                    Price = 8.99M,
                    ReleaseYear = 2010
                },
                new Movie
                {
                    Title = "The Dark Knight",
                    ReleaseDate = DateTime.Parse("2008-07-18"),
                    Genre = "Action",
                    Rating = "PG-13",
                    Price = 9.99M,
                    ReleaseYear = 2008
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    ReleaseDate = DateTime.Parse("1994-10-14"),
                    Genre = "Crime",
                    Rating = "R",
                    Price = 7.99M,
                    ReleaseYear = 1994
                },
                new Movie
                {
                    Title = "Forrest Gump",
                    ReleaseDate = DateTime.Parse("1994-07-06"),
                    Genre = "Drama",
                    Rating = "PG-13",
                    Price = 6.99M,
                    ReleaseYear = 1994
                }
            );
            context.SaveChanges();
        }
    }
}
