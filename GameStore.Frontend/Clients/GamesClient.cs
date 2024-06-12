using GameStore.Frontend.Models;

namespace GameStore.Frontend.Clients
{
    public class GamesClient
    {
        private readonly List<GameSummary> games =
        [
            new()
            {
                Id = 1,
                Name = "Street Figher II",
                Genre = "Fighting",
                Price = 19.99M,
                ReleaseDate = new DateOnly(1992, 7, 15)
            },

            new()
            {
                Id = 2,
                Name = "Final Fantasy XIV",
                Genre = "RolePlaying",
                Price = 59.99M,
                ReleaseDate = new DateOnly(2010, 9, 30)
            },

            new()
            {
                Id = 3,
                Name = "Fifa 23",
                Genre = "Sports",
                Price = 69.99M,
                ReleaseDate = new DateOnly(2022, 9, 27)
            },
        ];

        private readonly Genre[] genres = new GenresClient().Genres();

        public GameSummary[] Games() => [.. games];

        public void AddGame(GameDetails game)
        {
            Genre genre = GetGenreById(game.GenreId);

            var gameSummary = new GameSummary
            {
                Id = games.Count + 1,
                Name = game.Name,
                Genre = genre.Name,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            };

            games.Add(gameSummary);
        }

        public GameDetails GetGame(int id)
        {
            GameSummary game = GetGameSummaryById(id);

            var genre = genres.Single(g => string.Equals(
                g.Name,
                game.Genre,
                StringComparison.OrdinalIgnoreCase));

            return new GameDetails
            {
                Id = game.Id,
                Name = game.Name,
                GenreId = genre.Id.ToString(),
                ReleaseDate = game.ReleaseDate,
                Price = game.Price
            };
        }

        public void UpdateGame(GameDetails updatedGame)
        {
            var genre = GetGenreById(updatedGame.GenreId);
            var existingGame = GetGameSummaryById(updatedGame.Id);

            existingGame.Name = updatedGame.Name;
            existingGame.Price = updatedGame.Price;
            existingGame.Genre = genre.Name;
            existingGame.ReleaseDate = updatedGame.ReleaseDate;
        }

        public void DeleteGame(int id)
        {
            var game = GetGameSummaryById(id);
            games.Remove(game);
        }

        private GameSummary GetGameSummaryById(int id)
        {
            GameSummary? game = games.Find(g => g.Id == id);
            ArgumentNullException.ThrowIfNull(game);
            return game;
        }

        private Genre GetGenreById(string? id)
        {
            ArgumentException.ThrowIfNullOrEmpty(id);
            return genres.Single(g => g.Id == int.Parse(id));
        }
    }
}
