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
        public HttpClient HttpClient { get; private set; }

        public string ApiKey { get; set; }

        public string GameId { get; set; }
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
                postData["start_date"] = startDate.Value.ToString("YYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYY-MM-DD");
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
                postData["start_date"] = startDate.Value.ToString("YYY-MM-DD");
            }

            if (endDate.HasValue)
            {
                postData["end_date"] = endDate.Value.ToString("YYY-MM-DD");
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
        /// <returns></returns>
        /// <exception cref="System.NullReferenceException">
        /// API Key cannot be null or empty
        /// or
        /// Game ID cannot be null or empty
        /// </exception>
        /// <exception cref="System.ArgumentNullException">username;Username cannot be null or empty</exception>
        public async Task<List<Score>> GetPlayerScoresAsync(string username)
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

            var response = await PostData<ScoreArray[]>(postData, "getPlayerScores");

            return response.ToList().Select(x => x.Scores).ToList();
        }
        #endregion
        #endregion

        #region Private methods

        /// <summary>
        /// Gets the player from query async.
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
            var url = GetScoreoidUri(methodName);

            var response = await HttpClient.PostAsync(url, new FormUrlEncodedContent(postData));

            var responseString = await response.Content.ReadAsStringAsync();

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
