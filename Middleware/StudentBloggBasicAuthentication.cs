using StudentBloggAPI.Services.Interfaces;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace StudentBloggAPI.Middleware;

public class StudentBloggBasicAuthentication : IMiddleware
{
    private readonly IUserService _userService;
    private readonly ILogger<StudentBloggBasicAuthentication> _logger;
    private readonly List<Regex> _excludePatterns = new();

    public StudentBloggBasicAuthentication(IUserService userService, ILogger<StudentBloggBasicAuthentication> logger)
    {
        _userService = userService;
        _logger = logger;
        _excludePatterns.Add(new Regex("/api/.*/[Uu]sers/Register"));
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_excludePatterns.Any(reg => reg.IsMatch(context.Request.Path.Value!)))
        {
            await next(context);
            return;
        }

        try
        {
            if (!context.Request.Headers.ContainsKey("Authorization"))
                throw new UnauthorizedAccessException("'Authorization' mangler i HTTP-header");

            var authHeader = context.Request.Headers.Authorization;

            string base64string = authHeader.ToString().Split(" ")[1];
            string user_password = DecodeBase64String(base64string);

            var arr = user_password.Split(":");

            if (arr.Length != 2)
            {
                throw new UnauthorizedAccessException("Passord eller Brukernavn mangler");
            }

            string userName = arr[0];
            string password = arr[1];

            int userId = await _userService.IsUserAuthorizedAsync(userName, password);
            if (userId == 0)
            {
                throw new UnauthorizedAccessException("Ingen tilgang til API");
            }

            context.Items["UserId"] = userId;
            context.Items["UserName"] = userName;

            await next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await Results.Problem(
                title: "Unauthorized: Ikke lov til å bruke API",
                statusCode: StatusCodes.Status401Unauthorized,
                detail: ex.Message,
                extensions: new Dictionary<string, object?>
                {
                    { "traceId", Activity.Current?.Id }
                }).ExecuteAsync(context);
        }
    }

    private string DecodeBase64String(string base64string)
    {
        byte[] base64bytes = Convert.FromBase64String(base64string);
        string usrname_and_password = System.Text.Encoding.UTF8.GetString(base64bytes);
        return usrname_and_password;
    }
}
