using ExamApp.Config;
using ExamApp.Models;
using ExamApp.Repository.Inf;

namespace ExamApp.Repository.Base;

public class QuestionRepository : BaseRepository<Question>, IQuestionRepository
{
    public QuestionRepository(DataContext dataContext) : base(dataContext)
    {
    }

    public List<Question> GetByExamID(int exam_id)
    {
        return dataContext.Set<Question>().Where(e => e.ExamID == exam_id).ToList();
    }
}