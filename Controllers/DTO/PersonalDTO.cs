using ExamApp.Models;

namespace ExamApp.Controllers.DTO;

public class PersonalDTO
{
    public int personal_id
    {
        get;
        set;
    }
    
    public string? email
    {
        get;
        set;
    }


    public string? image
    {
        get;
        set;
    }
    
    public string? phone
    {
        get;
        set;
    }

    public string? uid
    {
        get;
        set;
    }

    public DateTime? create_date
    {
        get;
        set;
    }
    
    public List<GroupDTO>? groups_owner
    {
        get;
        set;
    }
    
    public List<GroupDTO>? groups_joined
    {
        get;
        set;
    }
    
    public PersonalDTO() {}

    public PersonalDTO(Personal source)
    {
        personal_id = source.PersonalID;
        email = source.Email;
        uid = source.UID;
        image = source.Image;
        create_date = source.CreateDate;
        phone = source.Phone;
    }
}