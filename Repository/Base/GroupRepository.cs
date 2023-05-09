using ExamApp.Config;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Repository.Base;

public class GroupRepository : BaseRepository<Group>, IGroupRepository
{
    public GroupRepository(DataContext dataContext) : base(dataContext)
    {
    }
    
    public override Group GetByCondition(string arg, string type)
    {
        return base.dataContext.Set<Group>().FirstOrDefault(p => p.Name == arg);
    }
    
    public override int Create(Group group, Type type)
    {
        if (group.GroupID == null || group.GroupID == 0)
        {
            Console.WriteLine("repo =========================== check");
            dataContext.Set<Group>().Add(group);
            dataContext.SaveChanges();
        }
        Console.WriteLine("repo =========================== " + group.GroupID);
        RelationshipGroupAndPersonal relationship = new RelationshipGroupAndPersonal();
        relationship.group_id = group.GroupID;
        relationship.personal_id = group.PersonalIDCreate;
        relationship.Type = type;

        dataContext.Set<RelationshipGroupAndPersonal>().Add(relationship);
        dataContext.SaveChanges();
        return group.GroupID;
    }

    public int CreateMember(MemberDTO dto)
    {
        RelationshipGroupAndPersonal relationship = new RelationshipGroupAndPersonal();
        relationship.group_id = dto.group_id;
        relationship.personal_id = dto.personal_id;
        relationship.Type = Type.JOINED;
        
        dataContext.Set<RelationshipGroupAndPersonal>().Add(relationship);
        dataContext.SaveChanges();
        return dto.group_id;
    }

    public List<PersonalDTO> GetMemberByGroupID(int group_id)
    {
         var result =  dataContext.Set<Personal>()
                    .Join(dataContext.Set<RelationshipGroupAndPersonal>(),
                        p => p.PersonalID,
                        r => r.personal_id,
                        (p, r) => new { Personal = p, Relate = r })
                    .Where(r => r.Relate.Type == Type.JOINED && r.Relate.group_id == group_id);
        
                return result.Select(p => 
                        new PersonalDTO(p.Personal))
                        .ToList();
    }
}