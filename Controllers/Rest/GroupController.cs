using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.AspNetCore.Mvc;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Controllers.Rest;
[Route("group")]
public class GroupController : ControllerBase
{
    private IGroupRepository groupRepository;
    private IGroupConverter groupConverter;
    
    public GroupController(IGroupRepository groupRepository, IGroupConverter groupConverter)
    {
        this.groupRepository = groupRepository;
        this.groupConverter = groupConverter;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var groups = groupRepository.GetAll()
                            .Select(g => groupConverter.ConvertModel2DTO(g));
            if (groups.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(groups);
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
        var group = groupRepository.GetByID(id);
        GroupDTO result = groupConverter.ConvertModel2DTO(group);
        if (result == null)
        {
            return NoContent();
        } else
        {
            return Ok(result);
        }
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] GroupDTO dto)
    {
        try
        {
            string message = groupConverter.validate(dto);
            Console.WriteLine($"group =================================== {message}");
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            Group group = groupConverter.ConvertDTO2Model(dto);
            Console.WriteLine($"group =================================== {groupRepository == null}");
            var id = groupRepository.Create(group, Type.CREATED);

            return Created($"/get-by-id?id={id}", groupConverter.ConvertModel2DTO(group));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
    
    [HttpPost]
    [Route("member")]
    public IActionResult AddMember([FromBody] MemberDTO dto)
    {
        try
        {
            string message = groupConverter.validateMember(dto);
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            var id = groupRepository.CreateMember(dto);
            Group group = groupRepository.GetByID(id);
            return Created($"/get-by-id?id={id}", groupConverter.ConvertModel2DTO(group));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}