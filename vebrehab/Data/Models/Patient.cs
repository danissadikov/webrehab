using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("patients")]
[Index(nameof(UserId), IsUnique = true, Name = "ux_patients_user_id")]
[Index(nameof(FullName), Name = "ix_patients_full_name")]
public class Patient : BaseEntity
{
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Required, Column("full_name"), MaxLength(255)]
    public string FullName { get; set; }

    [Column("birth_date")]
    public DateOnly? BirthDate { get; set; }

    [Column("sex"), StringLength(20)]
    public string? Sex { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("tags_json"), Comment("JSON: custom tags/metadata")]
    public string? TagsJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public virtual ICollection<PatientAssignment> Assignments { get; set; } = new List<PatientAssignment>();
    public virtual ICollection<VisitNote> VisitNotes { get; set; } = new List<VisitNote>();
    public virtual ICollection<PatientCondition> Conditions { get; set; } = new List<PatientCondition>();
    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
    public virtual ICollection<WorkoutSession> WorkoutSessions { get; set; } = new List<WorkoutSession>();
    public virtual ICollection<ExerciseLog> ExerciseLogs { get; set; } = new List<ExerciseLog>();
    public virtual ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    public virtual ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
}