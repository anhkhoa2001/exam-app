namespace ExamApp.Controllers.DTO;

public class QuestionDTO
{
    public int question_id
    {
        get;
        set;
    }

    public int exam_id
    {
        get;
        set;
    }

    public string? content
    {
        get;
        set;
    }
}