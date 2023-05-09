using ExamApp.Config;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Repository.Base;

public class PersonRepository : BaseRepository<Personal>, IPersonRepository
{
    public PersonRepository(DataContext dataContext) : base(dataContext)
    {
    }
    
    public override Personal GetByCondition(string arg, string type)
    {
        return base.dataContext.Set<Personal>().FirstOrDefault(p => p.Email == arg);
    }

    public List<GroupDTO> GetByGroupID(int group_id, Type type)
    {
        var result =  dataContext.Set<Group>()
            .Join(dataContext.Set<RelationshipGroupAndPersonal>(),
                g => g.GroupID,
                p => p.group_id,
                (g, p) => new { Group = g, Relate = p })
            .Where(g => g.Relate.Type == type && g.Group.GroupID == group_id);

        return result.Select(p => 
                new GroupDTO(p.Group.GroupID, p.Group.Name, p.Group.PersonalIDCreate))
                .ToList();
    }
    
    public List<GroupDTO> GetByPersonalIDCreate(int person_id, Type type)
    {
        /*return base.dataContext.Set<RelationshipGroupAndPersonal>().Where(p => 
                                        p.personal_id == group_id && p.Type == type);*/

        var result =  dataContext.Set<Group>()
                        .Join(dataContext.Set<RelationshipGroupAndPersonal>(),
                            g => g.GroupID,
                            p => p.group_id,
                            (g, p) => new { Group = g, Relate = p })
                        .Where(g => g.Relate.Type == type && g.Group.PersonalIDCreate == person_id);

        return result.Select(p => 
                        new GroupDTO(p.Group.GroupID, p.Group.Name, p.Group.PersonalIDCreate))
                        .ToList();
    }

    public List<GroupDTO> GetByPersonalID(int personal_id, Type type)
    {
        var result =  dataContext.Set<Group>()
            .Join(dataContext.Set<RelationshipGroupAndPersonal>(),
                g => g.GroupID,
                p => p.group_id,
                (g, p) => new { Group = g, Relate = p })
            .Where(g => g.Relate.Type == type && g.Relate.personal_id == personal_id);

        return result.Select(p => 
                new GroupDTO(p.Group.GroupID, p.Group.Name, p.Group.PersonalIDCreate))
            .ToList();
    }
}