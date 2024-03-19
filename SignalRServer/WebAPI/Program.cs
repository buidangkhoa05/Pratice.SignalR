
using Microsoft.AspNetCore.Http.Connections;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.Services.AddCors(cfs => cfs.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy()
        {
            IsOriginAllowed = _ => true,
            SupportsCredentials = false,
        }));

        builder.Services.AddSignalR(opts =>
        {
            opts.EnableDetailedErrors = true;
        })
        .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null);

        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)); // allow any origin

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.MapHub<RealTimeHub>("/hub", opts =>
        {
            opts.Transports = HttpTransportType.WebSockets;
        });

        app.Run();
    }
}
