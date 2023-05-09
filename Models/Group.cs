using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Models;

[Table("ex_group")]
public class Group
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("group_id")]
    public int GroupID
    {
        get;
        set;
    }

    [Column("name")]
    public string? Name
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

    [Column("personal_id_create")]
    [ForeignKey("PersonalCreate")]
    public int PersonalIDCreate
    {
        get;
        set;
    }

    [InverseProperty("GroupsOwner")]
    public Personal? PersonalCreate
    {
        get;
        set;
    }

    public List<RelationshipGroupAndPersonal>? Members
    {
        get;
        set;
    }

    public List<Exam>? ExamsIn
    {
        get;
        set;
    }
}