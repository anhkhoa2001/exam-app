using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers.Rest;

[Route("question")]
public class QuestionController : ControllerBase
{

    private IQuestionRepository questionRepository;
    private IQuestionConverter questionConverter;

    public QuestionController(IQuestionRepository questionRepository, IQuestionConverter questionConverter)
    {
        this.questionRepository = questionRepository;
        this.questionConverter = questionConverter;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var exams = questionRepository.GetAll()
                            .Select(p => questionConverter.ConvertModel2DTO(p));
            if (exams.Count() == 0)
            {
                return NoContent();
            }
            else
            {
                return Ok(exams);
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
        var question = questionRepository.GetByID(id);
        QuestionDTO dto = questionConverter.ConvertModel2DTO(question);
        if (dto == null)
        {
            return NoContent();
        } else
        {
            return Ok(dto);
        }
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] QuestionDTO dto)
    {
        try
        {
            string message = questionConverter.validate(dto);
            Console.WriteLine($"group =================================== {message}");
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            Question question = questionConverter.ConvertDTO2Model(dto);
            var id = questionRepository.Create(question);

            return Created($"/get-by-id?id={id}", questionConverter.ConvertModel2DTO(question));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
}