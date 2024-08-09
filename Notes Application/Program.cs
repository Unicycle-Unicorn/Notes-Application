
using AuthProvider.Authentication;
using AuthProvider.CamInterface;
using AuthProvider.Exceptions;
using AuthProvider.RuntimePrecheck;
using AuthProvider.Swagger;
using AuthProvider.Utils;
using Microsoft.AspNetCore.HttpOverrides;

namespace Notes_Application;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        _ = builder.Services.AddSwaggerGen(config => config.OperationFilter<SwaggerAuth>());

        _ = builder.Services.AddAuthentication(NullAuthenticationHandler.RegisterWithBuilder);

        _ = builder.Services.AddExceptionHandler<UuidExceptionHandler>();
        _ = builder.Services.AddProblemDetails();

        const string CorsAllowAll = "CorsAllowAll";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(CorsAllowAll, builder =>
            {
                builder.WithOrigins("https://ui.unicycleunicorn.net").AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders(HeaderUtils.XExceptionCode);
            });
        });

        ICamInterface camService = new RemoteCamInterface("notes", "https://api.unicycleunicorn.net/cam");
        //ICamInterface camService = new RemoteCamInterface("notes", "http://localhost:5048");
        _ = builder.Services.AddSingleton(typeof(ICamInterface), camService);

        var app = builder.Build();

        RuntimePrechecker.RunPrecheck(app);

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        // We don't need https because our services are secured via nginx https through a proxy pass over http to this service
        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(CorsAllowAll);

        _ = app.UseExceptionHandler();
        app.MapControllers();

        await camService.Initialize();

        app.Run();
    }
}
