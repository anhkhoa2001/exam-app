using ExamApp.Controllers.Converter;
using ExamApp.Controllers.Converter.Inf;
using ExamApp.Repository.Base;
using ExamApp.Repository.Inf;

namespace ExamApp.Config;

public static class RepositoryExtension
{
    
    public static void ConfigureRepository(this IServiceCollection services)
    {
        services.AddScoped<IPersonRepository, PersonRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IExamRepository, ExamRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IPersonalConverter, PersonalConverter>();
        services.AddScoped<IGroupConverter, GroupConverter>();
        services.AddScoped<IExamConverter, ExamConverter>();
        services.AddScoped<IQuestionConverter, QuestionConverter>();
    }
}