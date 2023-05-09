using ExamApp.Controllers.Converter.Inf;
using ExamApp.Controllers.DTO;
using ExamApp.Models;
using ExamApp.Repository.Inf;
using FirebaseAdmin.Auth;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Controllers.Converter;

public class PersonalConverter : IPersonalConverter
{
    private IPersonRepository personRepository;

    public PersonalConverter(IPersonRepository personRepository)
    {
        this.personRepository = personRepository;
    }

    public string? validate(PersonalDTO dto)
    {
        if (dto.Equals(null))
        {
            return "Account invalid";
        }
        else
        {
            Personal personByEmail = personRepository.GetByCondition(dto.email, "username");

            if (personByEmail != null)
            {
                return "Username of account exist";
            } 
        }

        return null;
    }
    public Personal ConvertDTO2Model(PersonalDTO source)
    {
        Personal target = new Personal();
        target.Email = source.email;
        target.Phone = source.phone;
        target.Image = source.image;
        target.CreateDate = source.create_date;
        target.UID = source.uid;
        
        return target;
    }
    
    public Personal ConvertDTO2Model(UserRecord source)
    {
        Personal target = new Personal();
        target.Email = source.Email;
        target.Phone = source.PhoneNumber;
        target.Image = source.PhotoUrl;
        target.CreateDate = source.UserMetaData.CreationTimestamp;
        target.UID = source.Uid;
        
        return target;
    }

    public PersonalDTO ConvertModel2DTO(Personal source)
    {
        if (source == null)
        {
            return null;
        }
        else
        {
            PersonalDTO target = new PersonalDTO();
            target.personal_id = source.PersonalID;
            target.email = source.Email;
            target.phone = source.Phone;
            target.image = source.Image;
            target.create_date = source.CreateDate;
            target.uid = source.UID;

            List<GroupDTO> groupsOwner = 
                                personRepository.GetByPersonalIDCreate(target.personal_id, Type.CREATED);

            List<GroupDTO> groupsJoined = personRepository.GetByPersonalID(target.personal_id, Type.JOINED);

            target.groups_owner = groupsOwner;
            target.groups_joined = groupsJoined;
            
            return target;
        }
    }
}