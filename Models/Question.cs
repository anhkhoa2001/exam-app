using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamApp.Models;

[Table("ex_question")]
public class Question
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("question_id")]
    public int QuestionID
    {
        get;
        set;
    }

    [Column("content")]
    public string? Content
    {
        get;
        set;
    }

    [Column("exam_id")]
    [ForeignKey("Exam")]
    public int ExamID
    {
        get;
        set;
    }

    [InverseProperty("Questions")]
    public Exam? Exam
    {
        get;
        set;
    }
}