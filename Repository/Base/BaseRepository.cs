using ExamApp.Config;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Microsoft.EntityFrameworkCore;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Repository;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
{
    protected DataContext dataContext;

    public BaseRepository(DataContext dataContext)
    {
        this.dataContext = dataContext;
    }
    
    public List<T> GetAll()
    {
        return dataContext.Set<T>().ToList();
    }

    public virtual int Create(T t)
    {
        dataContext.Set<T>().Add(t);
        return dataContext.SaveChanges();
    }

    public T GetByID(int id)
    {
        return dataContext.Set<T>().Find(id);
    }

    public int Update(T newT)
    {
        dataContext.Set<T>().Update(newT);
        return dataContext.SaveChanges();
    }

    public void Delete(T t)
    {
        dataContext.Set<T>().Remove(t);
        dataContext.SaveChanges();
    }

    public virtual T GetByCondition(string arg, string type)
    {
        return null;
    }

    public virtual int Create(T t, Type type)
    {
        return 0;
    }
}