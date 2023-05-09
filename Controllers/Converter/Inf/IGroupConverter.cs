using ExamApp.Controllers.DTO;
using ExamApp.Models;

namespace ExamApp.Controllers.Converter.Inf;

public interface IGroupConverter : IBaseConverter<Group, GroupDTO>
{
    string validateMember(MemberDTO dto);
}