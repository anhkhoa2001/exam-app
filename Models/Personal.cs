using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Models;

[Table("ex_personal")]
public class Personal
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("personal_id")]
    public int PersonalID
    {
        get;
        set;
    }

    [Column("email")]
    public string? Email
    {
        get;
        set;
    }
    
    [Column("phone")]
    public string? Phone
    {
        get;
        set;
    }

    [Column("image")]
    public string? Image
    {
        get;
        set;
    }
    
    [Column("create_date")]
    public DateTime? CreateDate
    {
        get;
        set;
    }
    
    [Column("uid")]
    public string? UID
    {
        get;
        set;
    }

    public List<Group>? GroupsOwner
    {
        get;
        set;
    }

    //person joined n groups
    public List<RelationshipGroupAndPersonal> MembersPerson
    {
        get;
        set;
    }

    public List<Exam>? ExamsBy
    {
        get;
        set;
    }

}