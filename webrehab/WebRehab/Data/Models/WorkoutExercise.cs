using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("workout_exercises")]
[Index(nameof(WorkoutSessionId), Name = "ix_workout_exercises_workout_session_id")]
[Index(nameof(WorkoutSessionId), nameof(SortOrder), Name = "ix_workout_exercises_session_sort")]
[Index(nameof(ExerciseId), Name = "ix_workout_exercises_exercise_id")]
[Index(nameof(InstructionMediaId), Name = "ix_workout_exercises_instruction_media_id")]
public class WorkoutExercise : BaseEntity
{
    [Required, Column("workout_session_id")]
    public int WorkoutSessionId { get; set; }

    [Required, Column("sort_order")]
    public int SortOrder { get; set; } = 1;

    [Column("exercise_id")]
    public int? ExerciseId { get; set; }

    [Column("custom_exercise_title"), MaxLength(500)]
    public string? CustomExerciseTitle { get; set; }

    [Column("instruction_media_id")]
    public int? InstructionMediaId { get; set; }

    [Column("params_json"), Comment("JSON: runtime parameters")]
    public string? ParamsJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(WorkoutSessionId))]
    public virtual WorkoutSession Session { get; set; } = null!;

    [ForeignKey(nameof(ExerciseId))]
    public virtual Exercise? Exercise { get; set; }

    [ForeignKey(nameof(InstructionMediaId))]
    public virtual MediaAsset? InstructionMedia { get; set; }

    public virtual ICollection<ExerciseLog> Logs { get; set; } = new List<ExerciseLog>();
}