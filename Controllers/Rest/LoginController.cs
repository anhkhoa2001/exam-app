using ExamApp.Config;
using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers.Rest;

[Route("login")]
public class LoginController : ControllerBase
{
    private readonly IConfiguration configuration;
    private readonly IPersonRepository personRepository;
    private readonly IPersonalConverter personalConverter;

    public LoginController(IConfiguration configuration, IPersonRepository personRepository, IPersonalConverter personalConverter)
    {
        this.configuration = configuration;
        this.personalConverter = personalConverter;
        this.personRepository = personRepository;
    }
    
    [HttpPost("verify-id-token")]
    public async Task<IActionResult> Login([FromBody] RequestLoginDTO dto)
    {
        try
        {
            string userId = await FirebaseAuthentication.VerifyIdTokenAsync(dto.IDToken);
            UserRecord user = await FirebaseAuthentication.GetUserRecordAsync(userId);
            
            Personal personal = personRepository.GetByCondition(user.Email, "email");
            if (personal == null)
            {
                personal = personalConverter.ConvertDTO2Model(user);
                var id = personRepository.Create(personal);
            }

            string token = JwtConfig.generationToken(personal.Email, configuration);
            
            return Ok(token);
        }
        catch (FirebaseException ex)
        {
            return BadRequest(ex.Message);
        }
    }
    
    [HttpPost("get-by-user-id")]
    public async Task<IActionResult> GetByID([FromQuery] string userID)
    {
        try
        {
            UserRecord user = await FirebaseAuthentication.GetUserRecordAsync(userID);
            Personal personal = personRepository.GetByCondition(user.Email, "email");
            if (personal == null)
            {
                personal = personalConverter.ConvertDTO2Model(user);
                var id = personRepository.Create(personal);
            }

            string token = JwtConfig.generationToken(personal.Email, configuration);
            return Ok(token);
        }
        catch (FirebaseException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}