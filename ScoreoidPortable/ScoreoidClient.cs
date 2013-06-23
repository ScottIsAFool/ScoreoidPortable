using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ScoreoidPortable.Entities;
using System.Linq;

namespace ScoreoidPortable
{
    public class ScoreoidClient
    {
        private const string ScoreoidEndpoint = "https://www.scoreoid.com/api/";

        #region Public properties

        public HttpClient HttpClient
        {
            get;
            private set;
        }

        public string ApiKey
        {
            get;
            set;
        }

        public string GameId
        {
            get;
            set;
        }

        #endregion

        #region Constructors

        public ScoreoidClient(string apiKey, string gameId, HttpMessageHandler handler)
        {
            HttpClient = new HttpClient(handler);
            ApiKey = apiKey;
            GameId = gameId;
        }

        public ScoreoidClient(string apiKey, string gameId)
        {
            HttpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip
            });
            ApiKey = apiKey;
            GameId = gameId;
        }

        public ScoreoidClient()
            : this(string.Empty, string.Empty)
        {

        }

        #endregion

        #region Public methods

        #region Other methods

        /// <summary>
        /// Signs the in async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password. [Optional]</param>
        /// <returns>True if credentials were correct</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        public async Task<bool> SignInAsync(string username, string password = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "Username cannot be null or empty");
            }

            var users = await GetPlayerFromQueryAsync(username, password, null, true);

            return users != null && users.Any();
        }

        #endregion

        #region Player methods

        /// <summary>
        /// Gets the player count.
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The number of players based on the criteria given</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<int> GetPlayerCountAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var postData = CreatePostData();
            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            var response = await PostData<PlayerCountResponse>(postData, "countPlayers");

            return response.PlayerCount;
        }

        /// <summary>
        /// Gets the player async.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Returns the given player's information</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        public async Task<Player> GetPlayerAsync(string username)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "Username cannot be null or empty");
            }

            var users = await GetPlayerFromQueryAsync(username, null, null);

            return users != null && users.Any() ? users[0].Player : null;
        }

        /// <summary>
        /// Deletes the player.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>True if the player has been deleted</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        public async Task<bool> DeletePlayerAsync(string username)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "Username cannot be null or empty");
            }

            var postData = CreatePostData();
            postData["username"] = username;

            var response = await PostData<SuccessResponse>(postData, "deletePlayer");

            return response != null;
        }

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
        public async Task<bool> CreatePlayerAsync(Player player)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (player == null)
            {
                throw new ArgumentNullException("player", "Player cannot be null");
            }

            if (string.IsNullOrEmpty(player.Username))
            {
                throw new ArgumentNullException("player", "Username cannot be null or empty");
            }

            var postData = CreatePostData();
            postData.InsertItemToDictionary(player);

            var response = await PostData<SuccessResponse>(postData, "createPlayer");

            return response != null;
        }

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
        public async Task<bool> EditPlayerAsync(Player player)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (player == null)
            {
                throw new ArgumentNullException("player", "Player cannot be null");
            }

            if (string.IsNullOrEmpty(player.Username))
            {
                throw new ArgumentNullException("player", "Username cannot be null or empty");
            }

            var postData = CreatePostData();
            postData.InsertItemToDictionary(player);

            var response = await PostData<SuccessResponse>(postData, "editPlayer");

            return response != null;
        }

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
        public async Task<int> GetPlayerRankAsync(string username, DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "Username cannot be null or empty");
            }

            var postData = CreatePostData();
            postData["username"] = username;

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            var response = await PostData<PlayerRankResponse>(postData, "getPlayerRank");

            return response.Rank;
        }

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
        public async Task<List<Score>> GetPlayerScoresAsync(string username,
                                                            SortBy? sortBy = null,
                                                            OrderBy? orderBy = null,
                                                            int? startingAt = null,
                                                            int? numberToRetrieve = null,
                                                            DateTime? startDate = null,
                                                            DateTime? endDate = null,
                                                            string platform = null,
                                                            int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException("username", "Username cannot be null or empty");
            }

            var response = await GetItemsFromQueryAsync<ScoreResponse[]>("username", "getPlayerScores", sortBy, orderBy, startingAt, numberToRetrieve, startDate, endDate, platform, difficulty, username);

            return response.ToList().Select(x => x.Scores).ToList();
        }

        #endregion

        #region Score methods

        /// <summary>
        /// Creates the score async.
        /// </summary>
        /// <param name="score">The score.</param>
        /// <returns>True if the score published ok</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        /// <exception cref="System.ArgumentNullException">score;Score cannot be null</exception>
        public async Task<bool> CreateScoreAsync(Score score)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            if (score == null)
            {
                throw new ArgumentNullException("score", "Score cannot be null");
            }

            var postData = CreatePostData();
            postData.InsertItemToDictionary(score);

            var response = await PostData<SuccessResponse>(postData, "createScore");

            return response != null;
        }

        /// <summary>
        /// Counts the scores 
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The count of scores for your game</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> CountScoresAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var postData = CreatePostData();

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            var response = await PostData<ScoreCountResponse>(postData, "countScores");

            return response.ScoreCount;
        }

        /// <summary>
        /// Counts the best scores 
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>The count of best scores for your game</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> CountBestScoresAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var postData = CreatePostData();

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            var response = await PostData<ScoreCountResponse>(postData, "countBestScores");

            return response.ScoreCount;
        }

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
        public async Task<List<Score>> GetScoresAsync(SortBy? sortBy = null,
                                                    OrderBy? orderBy = null,
                                                    int? startingAt = null,
                                                    int? numberToRetrieve = null,
                                                    DateTime? startDate = null,
                                                    DateTime? endDate = null,
                                                    string platform = null,
                                                    int difficulty = 0,
                                                    string usernames = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var response = await GetItemsFromQueryAsync<ScoreResponse[]>("usernames", "getScores", sortBy, orderBy, startingAt, numberToRetrieve, startDate, endDate, platform, difficulty, usernames);

            return response.ToList().Select(x => x.Scores).ToList();
        }

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
        public async Task<List<Score>> GetBestScoresAsync(SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0,
            string usernames = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var response = await GetItemsFromQueryAsync<ScoreResponse[]>("usernames", "getBestScores", sortBy, orderBy, startingAt, numberToRetrieve, startDate, endDate, platform, difficulty, usernames);

            return response.ToList().Select(x => x.Scores).ToList();
        }

        /// <summary>
        /// Gets the average score.
        /// </summary>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <param name="difficulty">The difficulty. [Optional]</param>
        /// <returns>Returns the average score based on the given criteria</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> GetAverageScoreAsync(DateTime? startDate = null, DateTime? endDate = null, string platform = null, int difficulty = 0)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var postData = CreatePostData();

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            var response = await PostData<AverageScoreResponse>(postData, "getAverageScore");

            return response.AverageScore;
        }
        #endregion

        #region Game methods

        /// <summary>
        /// Gets the game details.
        /// </summary>
        /// <returns>The game details for the game id being used.</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<Game> GetGameAsync()
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var postData = CreatePostData();

            var response = await PostData<GameResponse>(postData, "getGame");

            return response.Game;
        }

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
        public async Task<List<Player>> GetGamePlayersAsync(SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            var response = await GetItemsFromQueryAsync<PlayerResponse[]>("", "getPlayers", sortBy, orderBy, startingAt, numberToRetrieve, startDate, endDate, platform);

            return response.ToList().Select(x => x.Player).ToList();
        }

        /// <summary>
        /// Gets the game average.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's average score for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> GetGameAverageAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            return await GetGameFieldValueAsync("getGameAverage", gameField, startDate, endDate, platform);
        }

        /// <summary>
        /// Gets the game top score.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's top score for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> GetGameTopAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            return await GetGameFieldValueAsync("getGameTop", gameField, startDate, endDate, platform);
        }

        /// <summary>
        /// Gets the game lowest.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's lowest value for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> GetGameLowestAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            return await GetGameFieldValueAsync("getGameLowest", gameField, startDate, endDate, platform);
        }

        /// <summary>
        /// Gets the game total.
        /// </summary>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date. [Optional]</param>
        /// <param name="endDate">The end date. [Optional]</param>
        /// <param name="platform">The platform. [Optional]</param>
        /// <returns>The game's total value for that field</returns>
        /// <exception cref="System.NullReferenceException">API Key or Game ID cannot be null or empty</exception>
        public async Task<double> GetGameTotalAsync(GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null)
        {
            if (string.IsNullOrEmpty(ApiKey) || string.IsNullOrEmpty(GameId))
            {
                throw new NullReferenceException("API Key or Game ID cannot be null or empty");
            }

            return await GetGameFieldValueAsync("getGameTotal", gameField, startDate, endDate, platform);
        }

        #endregion
        #endregion

        #region Private/Internal methods

        /// <summary>
        /// Gets the number from json.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <returns>The number from within the JSON</returns>
        internal double GetNumberFromJson(string response)
        {
            if (response.StartsWith("{\"error\""))
            {
                return 0;
            }

            var colonPoint = response.IndexOf(":", StringComparison.Ordinal);
            var value = response.Substring(colonPoint + 1, response.Length - colonPoint - 2);
            double returnValue;

            return double.TryParse(value, out returnValue) ? returnValue : 0;
        }

        /// <summary>
        /// Gets the game field value async.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="gameField">The game field.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="platform">The platform.</param>
        /// <returns>The value for that field</returns>
        private async Task<double> GetGameFieldValueAsync(string methodName, GameField gameField, DateTime? startDate = null, DateTime? endDate = null, string platform = null)
        {
            var postData = CreatePostData();
            postData["field"] = gameField.GetDescription();

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            var response = await PostTheDataAsync(postData, methodName);

            var value = GetNumberFromJson(response);

            return value;
        }
        
        /// <summary>
        /// Gets the player from query.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="isLogin">if set to <c>true</c> [is login].</param>
        /// <returns>An array of player responses</returns>
        private async Task<PlayerResponse[]> GetPlayerFromQueryAsync(string username, string password, string emailAddress, bool isLogin = false)
        {
            var postData = CreatePostData();
            if (!string.IsNullOrEmpty(username))
            {
                postData["username"] = username;
            }

            if (!string.IsNullOrEmpty(password))
            {
                postData["password"] = password;
            }

            if (!string.IsNullOrEmpty(emailAddress))
            {
                postData["email"] = emailAddress;
            }

            return await PostData<PlayerResponse[]>(postData, "getPlayer", isLogin);
        }

        /// <summary>
        /// Gets the items from query async.
        /// </summary>
        /// <typeparam name="T">The return type</typeparam>
        /// <param name="usernameParameter">The username parameter.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="sortBy">The sort by.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="startingAt">The starting at.</param>
        /// <param name="numberToRetrieve">The number to retrieve.</param>
        /// <param name="startDate">The start date.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="platform">The platform.</param>
        /// <param name="difficulty">The difficulty.</param>
        /// <param name="usernames">The usernames.</param>
        /// <returns>The type of object requested</returns>
        private async Task<T> GetItemsFromQueryAsync<T>(string usernameParameter, 
            string methodName, 
            SortBy? sortBy = null,
            OrderBy? orderBy = null,
            int? startingAt = null,
            int? numberToRetrieve = null,
            DateTime? startDate = null,
            DateTime? endDate = null,
            string platform = null,
            int difficulty = 0,
            string usernames = null)
        {
            var postData = CreatePostData();

            if (sortBy.HasValue)
            {
                postData["order_by"] = sortBy.Value.GetDescription();
            }

            if (orderBy.HasValue)
            {
                postData["order"] = orderBy.Value.GetDescription();
            }

            if (startingAt.HasValue && numberToRetrieve.HasValue)
            {
                var limit = string.Format("{0},{1}", startingAt, numberToRetrieve);
                postData["limit"] = limit;
            }
            else
            {
                if (numberToRetrieve.HasValue)
                {
                    postData["limit"] = numberToRetrieve.Value.ToString();
                }
            }

            if (startDate.HasValue)
            {
                postData["start_date"] = startDate.Value.ToString("YYYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYYY-MM-DD");
            }

            if (!string.IsNullOrEmpty(platform))
            {
                postData["platform"] = platform;
            }

            if (difficulty > 0)
            {
                postData["difficulty"] = difficulty.ToString();
            }

            if (!string.IsNullOrEmpty(usernames))
            {
                postData[usernameParameter] = usernames;
            }

            var response = await PostData<T>(postData, methodName);

            return response;
        }

        /// <summary>
        /// Gets the scoreoid URI.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns>The scoreoid API URI</returns>
        private static string GetScoreoidUri(string method)
        {
            return string.Format("{0}{1}", ScoreoidEndpoint, method);
        }

        /// <summary>
        /// Posts the data.
        /// </summary>
        /// <typeparam name="T">The requested Type</typeparam>
        /// <param name="postData">The post data.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="suppressException">if set to <c>true</c> [suppress exception].</param>
        /// <returns>The request deserialized item</returns>
        /// <exception cref="ScoreoidException"></exception>
        private async Task<T> PostData<T>(Dictionary<string, string> postData, string methodName, bool suppressException = false)
        {
            var responseString = await PostTheDataAsync(postData, methodName);

            if (responseString.StartsWith("{\"error\""))
            {
                var error = JsonConvert.DeserializeObject<ErrorResponse>(responseString);
                if (suppressException)
                {
                    return default(T);
                }

                throw new ScoreoidException(error.Error);
            }

            var returnObject = JsonConvert.DeserializeObject<T>(responseString);

            return returnObject;
        }

        private async Task<string> PostTheDataAsync(Dictionary<string, string> postData, string methodName)
        {
            var url = GetScoreoidUri(methodName);

            var response = await HttpClient.PostAsync(url, new FormUrlEncodedContent(postData));

            var responseString = await response.Content.ReadAsStringAsync();
            
            return responseString;
        }

        /// <summary>
        /// Creates the post data.
        /// </summary>
        /// <returns>The default post data information that's in every API call</returns>
        private Dictionary<string, string> CreatePostData()
        {
            var postData = new Dictionary<string, string>();
            postData["api_key"] = ApiKey;
            postData["game_id"] = GameId;
            postData["response"] = "JSON";
            return postData;
        }
        
        #endregion
    }
}