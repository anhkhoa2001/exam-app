using ExamApp.Controllers.DTO;
using ExamApp.Models;

namespace ExamApp.Repository.Inf;

public interface IGroupRepository : IBaseRepository<Group>
{
    int CreateMember(MemberDTO dto);
    List<PersonalDTO> GetMemberByGroupID(int group_id);
}