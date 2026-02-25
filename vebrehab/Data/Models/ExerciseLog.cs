using Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("exercise_logs")]
[Index(nameof(WorkoutExerciseId), Name = "ix_exercise_logs_workout_exercise_id")]
[Index(nameof(WorkoutExerciseId), nameof(PerformedAt), Name = "ix_exercise_logs_exercise_performed_at")]
[Index(nameof(PatientId), Name = "ix_exercise_logs_patient_id")]
[Index(nameof(PatientId), nameof(PerformedAt), Name = "ix_exercise_logs_patient_performed_at")]
[Index(nameof(CreatedByUserId), Name = "ix_exercise_logs_created_by_user_id")]
[Index(nameof(Completed), Name = "ix_exercise_logs_completed")]
public class ExerciseLog : BaseEntity
{
    [Required, Column("workout_exercise_id")]
    public int WorkoutExerciseId { get; set; }

    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Required, Column("created_by_user_id")]
    public Guid CreatedByUserId { get; set; } // Обязательный

    [Column("performed_at")]
    public DateTime? PerformedAt { get; set; }

    [Required, Column("completed")]
    public bool Completed { get; set; }

    [Column("pain_int")]
    public int? PainInt { get; set; }

    [Column("rom_value")]
    public decimal? RomValue { get; set; }

    [Column("metrics_json"), Comment("JSON: detailed metrics")]
    public string? MetricsJson { get; set; }

    [Column("patient_comment")]
    public string? PatientComment { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(WorkoutExerciseId))]
    public virtual WorkoutExercise WorkoutExercise { get; set; } = null!;

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<ExerciseLogMedia> Media { get; set; } = new List<ExerciseLogMedia>();
}