
using ExamApp.Config;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.Extensions.Primitives;

namespace ExamApp.Controllers.MiddleWare;

public class AuthenticationMiddle : IMiddleware
{
    private readonly IPersonRepository personRepository;
    private readonly IConfiguration configuration;

    public AuthenticationMiddle(IPersonRepository personRepository, IConfiguration configuration)
    {
        this.personRepository = personRepository;
        this.configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        string path = context.Request.Path.ToString();
        if (path.Contains("login"))
        {
            await next(context);
        }
        else
        {
            Console.WriteLine("check =================================");
            string token = context.Request.Headers.Authorization.ToString();
            if (token == null || token.Length < 100)
            {
                Console.WriteLine("check =================================");
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Add Token To Header");
            }
            else
            {
                string email = JwtConfig.GetUidFromToken(token, configuration);
                Console.WriteLine($"check ================================= {email}");
                if (email == null || email.Length == 0)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token Not Invalid");
                } 
                else if (string.Equals("Token is expired", email))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Token is expired");
                }
                else
                {
                    Personal personal = personRepository.GetByCondition(email, "email");
                    if (personal != null)
                    {
                        await next(context);
                    }
                    else
                    {
                        context.Response.StatusCode = 401;
                        await context.Response.WriteAsync("Token Not Invalid");
                    }
                }
            }
        }

    }
}