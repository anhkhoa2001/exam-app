using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;

namespace ExamApp.Controllers.Converter;

public class GroupConverter : IGroupConverter
{
    private IPersonRepository personRepository;
    private IGroupRepository groupRepository;

    public GroupConverter(IPersonRepository personRepository, IGroupRepository groupRepository)
    {
        this.personRepository = personRepository;
        this.groupRepository = groupRepository;
    }
    public Group ConvertDTO2Model(GroupDTO source)
    {
        Group target = new Group();
        target.Name = source.name;
        target.PersonalIDCreate = source.personal_id_create;
        target.Image = source.image;

        return target;
    }

    public GroupDTO ConvertModel2DTO(Group source)
    {
        if (source == null)
        {
            return null;
        }
        GroupDTO target = new GroupDTO();
        target.name = source.Name;
        target.group_id = source.GroupID;
        target.personal_id_create = source.PersonalIDCreate;
        target.image = source.Image;

        List<PersonalDTO> members = groupRepository.GetMemberByGroupID(source.GroupID);
        target.members = members;

        return target;
    }

    public string validate(GroupDTO dto)
    {
        if (dto == null)
        {
            return "Group invalid";
        }

        Personal personByID = personRepository.GetByID(dto.personal_id_create);
        Group groupByName = groupRepository.GetByCondition(dto.name, "");
        
        if (personByID == null)
        {
            return "ID of account invalid";
        } else if (groupByName != null)
        {
            return "Name of Group already exists";
        }

        return null;
    }

    public string validateMember(MemberDTO dto)
    {
        if (dto == null)
        {
            return "Input invalid";
        }
        else
        {
            Group group = groupRepository.GetByID(dto.group_id);
            Personal personal = personRepository.GetByID(dto.personal_id);

            if (group == null || personal == null)
            {
                return "ID input does not exists";
            } else if (group.PersonalIDCreate == dto.personal_id)
            {
                return "Person to add is the owning group";
            }
        }

        return null;
    }
}
