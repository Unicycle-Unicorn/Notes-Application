
using AuthProvider.Authentication;
using AuthProvider.CamInterface;
using AuthProvider.Exceptions;
using AuthProvider.RuntimePrecheck;
using AuthProvider.Swagger;
using AuthProvider.Utils;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Notes_Application;

public class Program
{
    public static async Task Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplicationUtils.Initialize(args);

        WebApplicationUtils.AddRemoteCam(builder, "notes");

        await WebApplicationUtils.Start(builder);
    }
}
