using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Data.Identity;

[Table("users")]
[Index(nameof(Email), IsUnique = true, Name = "ux_users_email")]
[Index(nameof(Role), Name = "ix_users_role")]
[Index(nameof(Status), Name = "ix_users_status")]
public class User
{
    /// <summary>
    /// Идентификатор (Guid)
    /// </summary>
    [Key, Column("id")]
    public Guid Id { get; set; }

    [Required, Column("email"), MaxLength(255)]
    public string Email { get; set; }

    [Column("full_name"), MaxLength(255)]
    public string? FullName { get; set; }

    [Required, Column("role"), StringLength(50)]
    public string Role { get; set; } = "therapist";

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Patient> ManagedPatients { get; set; } = new List<Patient>();
    public virtual ICollection<PatientAssignment> AssignedPatients { get; set; } = new List<PatientAssignment>();
    public virtual ICollection<VisitNote> AuthoredVisitNotes { get; set; } = new List<VisitNote>();
    public virtual ICollection<PatientCondition> PatientConditions { get; set; } = new List<PatientCondition>();
    public virtual ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    public virtual ICollection<Template> Templates { get; set; } = new List<Template>();
    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
    public virtual ICollection<ExerciseLog> ExerciseLogs { get; set; } = new List<ExerciseLog>();
    public virtual ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
    public virtual ICollection<TestDefinition> TestDefinitions { get; set; } = new List<TestDefinition>();
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
    public virtual ICollection<RehabProtocol> RehabProtocols { get; set; } = new List<RehabProtocol>();
    public virtual ICollection<PatientStageStatus> PatientStageStatuses { get; set; } = new List<PatientStageStatus>();
    public virtual ICollection<MediaAsset> MediaAssets { get; set; } = new List<MediaAsset>();
}