
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Type = ExamApp.Contants.Type;

namespace ExamApp.Models;

[Table("groups_personals")]
public class RelationshipGroupAndPersonal
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("member_id")]
    public int MemberID
    {
        get;
        set;
    }
    
    [Column("group_id")]
    [ForeignKey("Group")]
    public int group_id
    {
        get;
        set;
    }

    [Column("type")]
    public Type? Type
    {
        get;
        set;
    }

    [InverseProperty("Members")]
    public Group? Group
    {
        get;
        set;
    }

    [Column("personal_id")]
    [ForeignKey("Personal")]
    public int personal_id
    {
        get;
        set;
    }

    [InverseProperty("MembersPerson")]
    public Personal? Personal
    {
        get;
        set;
    }
}