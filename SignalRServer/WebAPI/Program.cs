
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using WebAPI.Middleware;

namespace WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        // Add services to the container.
        builder.Services.AddAuthorization();

        builder.WebHost.UseKestrel(opts =>
        {
            opts.Limits.MinRequestBodyDataRate = new MinDataRate(bytesPerSecond: 100, gracePeriod: TimeSpan.FromSeconds(60));
        });

        builder.Services.AddCors(cfs => cfs.AddPolicy("AllowAll", new Microsoft.AspNetCore.Cors.Infrastructure.CorsPolicy()
        {
            IsOriginAllowed = _ => true,
            SupportsCredentials = false,
        }));

        //config signalR
        builder.Services.AddSignalR(opts =>
        {
            opts.EnableDetailedErrors = true;
        })
        .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null);

        builder.Services.AddScoped<RequestMiddleware>();
        builder.Services.AddScoped<AuthensMidlleware>();


        builder.Services.AddControllers();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        //app.UseMiddleware<RequestMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<AuthensMidlleware>();

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
