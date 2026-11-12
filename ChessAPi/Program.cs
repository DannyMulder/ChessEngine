using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Linq;

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

    public class ChessHub : Hub
    {
        public static Dictionary<string, string> queue = [];

        public async Task JoinQueue()
        {
            if (queue.Count == 0)
                queue.Add(Context.ConnectionId, Guid.NewGuid().ToString());
            else
            {
                KeyValuePair<string, string> waiter = queue.First();
                try
                {
                    string Joiner = Context.ConnectionId;

                    await Groups.AddToGroupAsync(Joiner, waiter.Value);
                    await Groups.AddToGroupAsync(waiter.Key, waiter.Value);

                    await Clients.Group(waiter.Value).SendAsync("Connected", null);
                }
                finally
                {
                    queue.Remove(waiter.Key);
                }
            }
        }
    }
}
