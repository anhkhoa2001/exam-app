using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExamApp.Contants;

namespace ExamApp.Models;

[Table("ex_exam")]
public class Exam
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("exam_id")]
    public int ExamID
    {
        get;
        set;
    }

    [Column("title")]
    public string? Title
    {
        get;
        set;
    }

    [Column("access")]
    public Access Access
    {
        get;
        set;
    }

    [Column("description")]
    public string? Description
    {
        get;
        set;
    }

    [Column("total_question")]
    public int Total
    {
        get;
        set;
    }
    
    public List<Question> Questions
    {
        get;
        set;
    }

    [Column("personal_id_create")]
    [ForeignKey("PersonalCreateExam")]
    public int PersonalIDCreate
    {
        get;
        set;
    }

    [InverseProperty("ExamsBy")]
    public Personal? PersonalCreateExam
    {
        get;
        set;
    }

    [Column("group_id")]
    [ForeignKey("GroupInclude")]
    public int GroupID
    {
        get;
        set;
    }

    [InverseProperty("ExamsIn")]
    public Group? GroupInclude
    {
        get;
        set;
    }
}