namespace ExamApp.Controllers.DTO;

public class GroupDTO
{

    public int group_id
    {
        get;
        set;
    }
    public string? name
    {
        get;
        set;
    }
    
    public string? image
    {
        get;
        set;
    }

    public int personal_id_create
    {
        get;
        set;
    }

    public List<PersonalDTO>? members
    {
        get;
        set;
    }

    public GroupDTO(int GroupID, string Name, int personalIdCreate)
    {
        this.group_id = GroupID;
        this.name = Name;
        this.personal_id_create = personalIdCreate;
    }
    
    public GroupDTO()
    {
        
    }
}