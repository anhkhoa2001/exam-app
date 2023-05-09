using Type = ExamApp.Contants.Type;

namespace ExamApp.Repository.Inf;

public interface IBaseRepository<T>
{
    List<T> GetAll();
    int Create(T t);
    T GetByID(int id);
    int Update(T newT);
    void Delete(T t);
    T GetByCondition(string arg, string type);
    int Create(T t, Type type);
}