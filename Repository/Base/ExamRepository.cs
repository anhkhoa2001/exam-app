using ExamApp.Config;
using ExamApp.Models;
using ExamApp.Repository.Inf;

namespace ExamApp.Repository.Base;

public class ExamRepository : BaseRepository<Exam>, IExamRepository
{
    public ExamRepository(DataContext dataContext) : base(dataContext)
    {
    }
    
    
}