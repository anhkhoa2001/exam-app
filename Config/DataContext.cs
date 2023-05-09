using ExamApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamApp.Config;

public class DataContext : DbContext
{

    public DbSet<Personal> Personals { get; set; } = null!;
    
    public DbSet<Group> Groups { get; set; } = null!;
    
    public DbSet<Exam> Exams { get; set; } = null!;
    
    public DbSet<Question> Questions { get; set; } = null!;

    public DbSet<RelationshipGroupAndPersonal> Members
    {
        get;
        set;
    } = null!;

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //relationship 1 personal - n group //1 person can own group
        //own
        modelBuilder.Entity<Group>()
            .HasOne(p => p.PersonalCreate)
            .WithMany(b => b.GroupsOwner);
        
        //
        modelBuilder.Entity<RelationshipGroupAndPersonal>()
            .HasOne(p => p.Group)
            .WithMany(b => b.Members);
        
        //
        modelBuilder.Entity<RelationshipGroupAndPersonal>()
            .HasOne(p => p.Personal)
            .WithMany(b => b.MembersPerson);
        
        modelBuilder.Entity<Exam>()
            .HasOne(p => p.PersonalCreateExam)
            .WithMany(b => b.ExamsBy);
        
        modelBuilder.Entity<Question>()
            .HasOne(p => p.Exam)
            .WithMany(b => b.Questions);
        
        modelBuilder.Entity<Exam>()
            .HasOne(p => p.GroupInclude)
            .WithMany(b => b.ExamsIn);
        //relationship n personal - n group //1 person in n group and 1 group include n person
        //join
        
        /*//relationship 1 personal - n group //1 person can own group
        //own
        modelBuilder.Entity<Group>()
            .HasOne(p => p.PersonalCreate)
            .WithMany(b => b.GroupsOwner);
        
        //relationship 1 exam - n question //1 exam include question
        modelBuilder.Entity<Question>()
            .HasOne(p => p.Exam)
            .WithMany(b => b.Questions);
        
        //relationship n personal - n group //1 person in n group and 1 group include n person
        //join
        modelBuilder
            .Entity<Group>()
            .HasMany(p => p.Members)
            .WithMany(p => p.GroupsJoin)
            .UsingEntity(j => j.ToTable("groups_persons"));
        
        //relationship 1 group - n exam //1 group include exam
        modelBuilder.Entity<Exam>()
            .HasOne(p => p.Group)
            .WithMany(b => b.Exams);
        
        //relationship 1 person - n exam //1 person create n exam
        /*modelBuilder.Entity<Exam>()
            .HasOne(p => p.Personal)
            .WithMany(b => b.Exams);#1#*/
    }
}