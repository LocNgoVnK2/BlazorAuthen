

/*
using Serilog;
using System.Reflection;
using System.Security.Claims;

namespace BlazorAppMSAuth.Endpoints;


public static class Api
{
    public static void ConfigurePingApi(this WebApplication app)
    {
        // 1a. Ping
        var groupAuth = app.MapGroup("/api");
        var groupNoAuth = app.MapGroup("/api/noauth");

        groupAuth.MapGet("ping", BasicPing)
           .RequireAuthorization(policy => policy.RequireRole("Admin"));

        // ping with minimal auth (no roles, etc)
        groupAuth.MapGet("pang", DifferentPing)
            .RequireAuthorization();

        groupNoAuth.MapGet("pong", NoAuthPing)
            .AllowAnonymous();

    }

    private static IResult DifferentPing(HttpContext context)
    {
        var message = "Different Ping with Auth";
        Log.Information(message);
        return Results.Ok(message);
    }

    private static async Task<IResult> BasicPing(IConfiguration _config, HttpContext context)
    {
        try
        {
            Log.Information("Starting Basic Ping with Auth");
            var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            List<string> result = new List<string>();
            Assembly assembly = Assembly.GetEntryAssembly();
            result.Add("Ping: With Added Auth");
            result.Add("=====================");
            result.Add($"User: {userId}");
            result.Add($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}");
            result.Add($"Machine Name: {Environment.MachineName}");
            result.Add($"OS Version: {Environment.OSVersion.ToString() ?? ""}");
            result.Add($"Name: {assembly.GetName().Name ?? ""}");
            result.Add($"Version: {assembly.GetName().Version.ToString() ?? ""}");
            result.Add($".Net Framework: {AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName ?? ""}");
            

            return Results.Ok(result);

        }
        catch (Exception ex)
        {
            Log.Error("Failed Basic Ping with Auth {ex}", ex);
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> NoAuthPing()
    {
        try
        {
            Log.Information("Starting NoAuth Ping");
            List<string> result = new List<string>();
            result.Add("Ping: No Auth");
            result.Add("=============");
            result.Add($"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}");
            result.Add($"Machine Name: {Environment.MachineName}");
            result.Add($"OS Version: {Environment.OSVersion.ToString() ?? ""}");

            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            Log.Error("Failed NoAuth Ping {ex}", ex);
            return Results.BadRequest(ex.Message);
        }
    }
}
*/
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Security.Claims;
using Serilog;
using Microsoft.AspNetCore.Authorization;

namespace BlazorAppMSAuth.Endpoints
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        // GET: api/ping/basic
        [HttpGet("basic")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BasicPing()
        {
            try
            {
                Log.Information("Starting Basic Ping with Auth");
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var assembly = Assembly.GetEntryAssembly();
                var result = new List<string>
                {
                    "Ping: With Added Auth",
                    "=====================",
                    $"User: {userId}",
                    $"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}",
                    $"Machine Name: {Environment.MachineName}",
                    $"OS Version: {Environment.OSVersion}",
                    $"Name: {assembly?.GetName().Name ?? ""}",
                    $"Version: {assembly?.GetName().Version}",
                    $".NET Framework: {AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName ?? ""}"
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Failed Basic Ping with Auth {ex}", ex);
                return BadRequest(ex.Message);
            }
        }

        // GET: api/ping/different
        [HttpGet("different")]
        [Authorize]
        public IActionResult DifferentPing()
        {
            var message = "Different Ping with Auth";
            Log.Information(message);
            return Ok(message);
        }

        // GET: api/ping/noauth
        [HttpGet("noauth")]
        [AllowAnonymous]
        public IActionResult NoAuthPing()
        {
            try
            {
                Log.Information("Starting NoAuth Ping");
                var result = new List<string>
                {
                    "Ping: No Auth",
                    "=============",
                    $"Environment: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? ""}",
                    $"Machine Name: {Environment.MachineName}",
                    $"OS Version: {Environment.OSVersion}"
                };

                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error("Failed NoAuth Ping {ex}", ex);
                return BadRequest(ex.Message);
            }
        }
    }
}
