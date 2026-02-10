using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("workout_sessions")]
[Index(nameof(WorkoutPlanId), Name = "ix_workout_sessions_workout_plan_id")]
[Index(nameof(PatientId), Name = "ix_workout_sessions_patient_id")]
[Index(nameof(PatientId), nameof(ScheduledAt), Name = "ix_workout_sessions_patient_scheduled_at")]
[Index(nameof(Status), Name = "ix_workout_sessions_status")]
public class WorkoutSession : BaseEntity
{
    [Column("workout_plan_id")]
    public int? WorkoutPlanId { get; set; }

    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Column("scheduled_at")]
    public DateTime? ScheduledAt { get; set; }

    [Column("completed_at")]
    public DateTime? CompletedAt { get; set; }

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "planned";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(WorkoutPlanId))]
    public virtual WorkoutPlan? WorkoutPlan { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    public virtual ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();
    public virtual ICollection<ProgressEntry> ProgressEntries { get; set; } = new List<ProgressEntry>();
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}