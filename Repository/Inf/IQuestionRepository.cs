using ExamApp.Models;

namespace ExamApp.Repository.Inf;

public interface IQuestionRepository : IBaseRepository<Question>
{
    List<Question> GetByExamID(int exam_id);
}