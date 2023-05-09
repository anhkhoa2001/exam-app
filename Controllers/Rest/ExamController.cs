using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.AspNetCore.Mvc;

namespace ExamApp.Controllers.Rest;

[Route("exam")]
public class ExamController : ControllerBase
{
    private IExamRepository examRepository;
    private IExamConverter examConverter;
    private IQuestionRepository questionRepository;

    public ExamController(IExamRepository examRepository, IExamConverter examConverter, IQuestionRepository questionRepository)
    {
        this.examRepository = examRepository;
        this.examConverter = examConverter;
        this.questionRepository = questionRepository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        try
        {
            var exams = examRepository.GetAll();
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
        try
        {
            var exam = examRepository.GetByID(id);
            var questions = questionRepository.GetByExamID(id);
            exam.Questions = questions;
            ExamDTO dto = examConverter.ConvertModel2DTO(exam);

            return Ok(dto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        return BadRequest();
    }
    
    [HttpPost]
    public IActionResult Add([FromBody] ExamDTO dto)
    {
        try
        {
            string message = examConverter.validate(dto);
            Console.WriteLine($"group =================================== {message}");
            if (!String.IsNullOrEmpty(message))
            {
                return BadRequest(message);
            }

            Exam exam = examConverter.ConvertDTO2Model(dto);
            var id = examRepository.Create(exam);

            return Created($"/get-by-id?id={id}", examConverter.ConvertModel2DTO(exam));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return NotFound();
        }
    }
    
}