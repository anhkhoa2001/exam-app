using ExamApp.Controllers.DTO;
using ExamApp.Models;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Repository.Inf;

public interface IPersonRepository : IBaseRepository<Personal>
{
    List<GroupDTO> GetByGroupID(int group_id, Type type);
    List<GroupDTO> GetByPersonalIDCreate(int personal_id_create, Type type);
    
    List<GroupDTO> GetByPersonalID(int personal_id, Type type);
}