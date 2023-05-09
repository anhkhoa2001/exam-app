using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers.Rest;

[Route("person")]
public class PersonalController : ControllerBase
{
    private IPersonRepository personRepository;
    private IPersonalConverter personalConverter;

    public PersonalController(IPersonRepository personRepository, IPersonalConverter personalConverter)
    {
        this.personRepository = personRepository;
        this.personalConverter = personalConverter;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
            
        try
        {
            List<PersonalDTO> personals = personRepository.GetAll()
                                .Select(p => personalConverter.ConvertModel2DTO(p)).ToList();
            if (personals.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(personals);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
    
    [HttpGet]
    [Route("get-by-id")]
    public IActionResult GetById([FromQuery] int id)
    {
        var personal = personRepository.GetByID(id);
        PersonalDTO dto = personalConverter.ConvertModel2DTO(personal);
        if (dto == null)
        {
            return NoContent();
        } else
        {
            return Ok(dto);
        }
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] PersonalDTO dto)
    {
        try
        {
            string message = personalConverter.validate(dto);
            Console.WriteLine("================================ " + String.IsNullOrEmpty(message));
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }
            
            Personal personal = personalConverter.ConvertDTO2Model(dto);
            var id = personRepository.Create(personal);
            
            return Created($"/get-by-id?id={id}", personalConverter.ConvertModel2DTO(personal));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound("An error occurred");
        }
    }
}