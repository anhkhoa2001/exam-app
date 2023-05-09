using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;

namespace ExamApp.Controllers.Converter;

public class ExamConverter : IExamConverter
{
    private IPersonRepository personRepository;
    private IGroupRepository groupRepository;
    private IGroupConverter groupConverter;

    public ExamConverter(IPersonRepository personRepository, IGroupRepository groupRepository, IGroupConverter groupConverter)
    {
        this.personRepository = personRepository;
        this.groupRepository = groupRepository;
        this.groupConverter = groupConverter;
    }

    public Exam ConvertDTO2Model(ExamDTO source)
    {
        Exam target = new Exam();
        target.Title = source.title;
        target.Description = source.description;
        target.GroupID = source.group_id;
        target.Access = source.access;
        target.Total = source.total;
        target.PersonalIDCreate = source.personal_id_create;

        return target;
    }

    public ExamDTO ConvertModel2DTO(Exam source)
    {
        ExamDTO target = new ExamDTO();

        target.description = source.Description;
        target.title = source.Title;
        target.total = source.Total;
        target.access = source.Access;
        target.exam_id = source.ExamID;
        target.group_id = source.GroupID;
        target.personal_id_create = source.PersonalIDCreate;
        
        Console.WriteLine($"=================+++++ {source.Questions == null}");
        target.questions = source.Questions.Select(e =>
        {
            QuestionDTO dto = new QuestionDTO();
            dto.content = e.Content;
            dto.exam_id = e.ExamID;
            dto.question_id = e.QuestionID;

            return dto;
        }).ToList();

        return target;
    }

    public string validate(ExamDTO dto)
    {
        if (dto == null)
        {
            return "Input invalid";
        }

        Personal personal = personRepository.GetByID(dto.personal_id_create);
        Group group = groupRepository.GetByID(dto.group_id);

        var result = groupConverter.ConvertModel2DTO(group).members
                            .Select(p => p.personal_id == dto.personal_id_create);
        
        if (group == null || personal == null)
        {
            return "Group ID does not exists";
        }
        else if (result == null || (result!= null && result.Count() == 0))
        {
            return "Personal ID invalid";
        }

        return null;
    }
}