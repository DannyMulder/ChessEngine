using ChessEngine;

namespace ChessAPi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureHttpJsonOptions(options =>
            {
                options.SerializerOptions.IncludeFields = true;
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("PhpStorm", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:63342")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            var app = builder.Build();

            app.UseCors("PhpStorm");

            app.MapPost("/test", () =>
            {
                return "Succes";
            });

            app.Run("http://localhost:5000");
        }
    }
}
