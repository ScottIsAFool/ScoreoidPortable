using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using ScoreoidPortable.Entities;

namespace ScoreoidPortable
{
    public interface IScoreoidClient
    {
        HttpClient HttpClient{ get; }

        string ApiKey { get; set; }

        string GameId { get; set; }

        /// <summary>
        /// Signs the in async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password. [Optional]</param>
        /// <returns>True if credentials were correct</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        Task<bool> SignInAsync(string username, string password = null);

        /// <summary>
        /// Gets the player count.
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The number of players based on the criteria given</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<int> GetPlayerCountAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0);

        /// <summary>
        /// Gets the player async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Returns the given player's information</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        Task<Player> GetPlayerAsync(string username);

        /// <summary>
        /// Deletes the player.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>True if the player has been deleted</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        Task<bool> DeletePlayerAsync(string username);

        /// <summary>
        /// Creates the player.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if the player was created successfully</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">
        /// player;Player cannot be null
        /// or
        /// player;Username cannot be null or empty
        /// </exception>
        Task<bool> CreatePlayerAsync(Player player);

        /// <summary>
        /// Edits the player async.
        /// </summary>
        /// <param name="player">The player.</param>
        /// <returns>True if the player was successfully edited</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">
        /// player;Player cannot be null
        /// or
        /// player;Username cannot be null or empty
        /// </exception>
        Task<bool> EditPlayerAsync(Player player);

        /// <summary>
        /// Gets the player rank async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>
        /// The specified player's rank
        /// </returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        Task<int> GetPlayerRankAsync(string username, DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0);

        /// <summary>
        /// Gets the player scores async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="sortBy">The criteria on which to sort by. [Optional]</param>
        /// <param name="orderBy">The criteria on which to order by (based on the sortBy property). [Optional]</param>
        /// <param name="startingAt">The number at which items will start at. This value is ignored if numberToRetrieve is not also set [Optional]</param>
        /// <param name="numberToRetrieve">The number to retrieve. [Optional]</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>
        /// A list of scores for the given player and criteria
        /// </returns>
        /// <exception cref="System.NullReferenceException">API Key cannot be null or empty
        /// or
        /// Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">Username cannot be null or empty</exception>
        Task<List<Score>> GetPlayerScoresAsync(string username,
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0);

        /// <summary>
        /// Creates the score async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="score">The score.</param>
        /// <returns>True if the score published successfully</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">
        /// Score cannot be null
        /// or
        /// Username cannot be null or empty
        /// </exception>
        Task<bool> CreateScoreAsync(string username, Score score);

        /// <summary>
        /// Counts the scores 
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The count of scores for your game</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> CountScoresAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0);

        /// <summary>
        /// Counts the best scores 
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The count of best scores for your game</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> CountBestScoresAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0);

        /// <summary>
        /// Gets the best scores around a given player.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="startingAt">The starting at.</param>
        /// <param name="numberToRetrieve">The number to retrieve.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="platform">The platform.</param>
        /// <param name="difficulty">The difficulty.</param>
        /// <returns>
        /// The list of score items
        /// </returns>
        Task<List<ScoreItem>> GetBestScoresAroundPlayerAsync(string username, 
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0);

        /// <summary>
        /// Gets the best scores around a given score.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <param name="startingAt">The starting at.</param>
        /// <param name="numberToRetrieve">The number to retrieve.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="platform">The platform.</param>
        /// <param name="difficulty">The difficulty.</param>
        /// <returns>
        /// The list of score items
        /// </returns>
        Task<List<ScoreItem>> GetBestScoresAroundScoreAsync(int score,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0);

        /// <summary>
        /// Gets the scores.
        /// </summary>
        /// <param name="usernames">The username.</param>
        /// <param name="sortBy">The criteria on which to sort by. [Optional]</param>
        /// <param name="orderBy">The criteria on which to order by (based on the sortBy property). [Optional]</param>
        /// <param name="startingAt">The number at which items will start at. This value is ignored if numberToRetrieve is not also set [Optional]</param>
        /// <param name="numberToRetrieve">The number to retrieve. [Optional]</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>
        /// A list of scores for the given player and criteria
        /// </returns>
        /// <exception cref="System.NullReferenceException">API Key cannot be null or empty
        /// or
        /// Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">Username cannot be null or empty</exception>
        Task<List<ScoreItem>> GetScoresAsync(SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0,
            string usernames = null);

        /// <summary>
        /// Gets the best player scores.
        /// </summary>
        /// <param name="usernames">The username.</param>
        /// <param name="sortBy">The criteria on which to sort by. [Optional]</param>
        /// <param name="orderBy">The criteria on which to order by (based on the sortBy property). [Optional]</param>
        /// <param name="startingAt">The number at which items will start at. This value is ignored if numberToRetrieve is not also set [Optional]</param>
        /// <param name="numberToRetrieve">The number to retrieve. [Optional]</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>
        /// A list of scores for the given player and criteria
        /// </returns>
        /// <exception cref="System.NullReferenceException">API Key cannot be null or empty
        /// or
        /// Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">Username cannot be null or empty</exception>
        Task<List<ScoreItem>> GetBestScoresAsync(SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0,
            string usernames = null);

        /// <summary>
        /// Gets the average score.
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>Returns the average score based on the given criteria</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> GetAverageScoreAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0);

        /// <summary>
        /// Gets the game details.
        /// </summary>
        /// <returns>The game details for the game id being used.</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<Game> GetGameAsync();

        /// <summary>
        /// Gets the game players.
        /// </summary>
        /// <param name="sortBy">The criteria on which to sort by. [Optional]</param>
        /// <param name="orderBy">The criteria on which to order by (based on the sortBy property). [Optional]</param>
        /// <param name="startingAt">The number at which items will start at. This value is ignored if numberToRetrieve is not also set [Optional]</param>
        /// <param name="numberToRetrieve">The number to retrieve. [Optional]</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The list of players for this game</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<List<Player>> GetGamePlayersAsync(SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null);

        /// <summary>
        /// Gets the game average.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's average score for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> GetGameAverageAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null);

        /// <summary>
        /// Gets the game top score.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's top score for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> GetGameTopAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null);

        /// <summary>
        /// Gets the game lowest.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's lowest value for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> GetGameLowestAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null);

        /// <summary>
        /// Gets the game total.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's total value for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        Task<double> GetGameTotalAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null);
    }
}