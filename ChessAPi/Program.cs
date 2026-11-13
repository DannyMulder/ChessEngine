using Microsoft.AspNetCore.SignalR;

namespace ChessAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Services registreren
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:63342")
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseRouting();
            app.UseCors("AllowFrontend");

            app.MapHub<ChessHub>("/chessHub");

            app.Run("http://localhost:5000");
        }
    }
    public class UserQueue(string roomName, string playerID, string connectionID)
    {
        public string roomName = roomName;
        public string playerID = playerID;
        public string ConnectionID = connectionID;
    }

    public class Game(string roomName, string player1ID, string player2ID, string board)
    {
        public string RoomName { get; set; } = roomName;
        public string[] Players { get; set; } = { player1ID, player2ID };
        public string Board { get; set; } = board;
    }
    public class ChessHub : Hub
    {
        public static List<UserQueue> queue = [];
        public static List<Game> activeGames = [];

        private static Game? GetGame(string playerID)
        {
            return activeGames.FirstOrDefault(
                game => game.Players.Contains(playerID)
            );
        }

        public async Task JoinQueue(string playerID)
        {
            try
            {
                if (queue.Count == 0)
                    queue.Add(new UserQueue(Guid.NewGuid().ToString(), playerID, Context.ConnectionId));
                else
                {
                    UserQueue waiter = queue[0];
                    string joiner = Context.ConnectionId;
                    try
                    {
                        await Groups.AddToGroupAsync(joiner, waiter.roomName);
                        await Groups.AddToGroupAsync(waiter.ConnectionID, waiter.roomName);

                        await Clients.Group(waiter.roomName).SendAsync("Connected", waiter.roomName);

                        queue.RemoveAt(0);
                        activeGames.Add(new Game(waiter.roomName, waiter.playerID, playerID, "rnbqkbnrpppppppp................................PPPPPPPPRNBQKBNR"));
                    }
                    catch (Exception error)
                    {
                        Console.WriteLine(error);
                    }
                }
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }

        public async Task LeaveQueue(string playerID)
        {
            queue.RemoveAll(user => user.playerID == playerID);
        }

        public Task<Game?> LoadGame(string playerID)
        {
            // No try netting
            return Task.FromResult(GetGame(playerID));
        }

        public async Task RejoinGroup(string playerID)
        {
            // No try netting
            string sender = Context.ConnectionId;
            Game? game = activeGames.FirstOrDefault(
                game => game.Players.Contains(playerID)
            );

            if (game != null)
                await Groups.AddToGroupAsync(sender, game.RoomName);
        }

        public async Task SendMove(string board, string gameID, string playerID)
        {
            try
            {
                Game? game = GetGame(playerID);

                if (game != null)
                    game.Board = board;

                await Clients.OthersInGroup(gameID).SendAsync("GetMoves", board);
            }
            catch (Exception error)
            {
                Console.WriteLine(error);
            }
        }
    }
}
