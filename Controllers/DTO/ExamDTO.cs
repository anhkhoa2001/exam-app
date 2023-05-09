using ExamApp.Contants;

namespace ExamApp.Controllers.DTO;

public class ExamDTO
{
    public int exam_id
    {
        get;
        set;
    }

    public string? title
    {
        get;
        set;
    }

    public Access access
    {
        get;
        set;
    }

    public string? description
    {
        get;
        set;
    }

    public int total
    {
        get;
        set;
    }
    
    public List<QuestionDTO> questions
    {
        get;
        set;
    }
    
    public int personal_id_create
    {
        get;
        set;
    }
    
    public int group_id
    {
        get;
        set;
    }
}