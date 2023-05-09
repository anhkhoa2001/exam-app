using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;

namespace ExamApp.Controllers.Converter;

public class QuestionConverter : IQuestionConverter
{
    private IExamRepository examRepository;

    public QuestionConverter(IExamRepository examRepository)
    {
        this.examRepository = examRepository;
    }

    public Question ConvertDTO2Model(QuestionDTO source)
    {
        if (source == null)
        {
            return null;
        }
        Question target = new Question();
        target.Content = source.content;
        target.ExamID = source.exam_id;


        return target;
    }

    public QuestionDTO ConvertModel2DTO(Question source)
    {
        if (source == null)
        {
            return null;
        }

        QuestionDTO target = new QuestionDTO();
        target.content = source.Content;
        target.exam_id = source.ExamID;
        target.question_id = source.QuestionID;

        return target;
    }

    public string validate(QuestionDTO dto)
    {
        if (dto == null)
        {
            return "Input invalid";
        }

        Exam exam = examRepository.GetByID(dto.exam_id);
        if (exam == null)
        {
            return "Exam does not exists";
        }
        
        return null;
    }
}