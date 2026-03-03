using Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("test_results")]
[Index(nameof(PatientId), Name = "ix_test_results_patient_id")]
[Index(nameof(PatientId), nameof(PerformedAt), Name = "ix_test_results_patient_performed_at")]
[Index(nameof(PatientConditionId), Name = "ix_test_results_patient_condition_id")]
[Index(nameof(TestDefinitionId), Name = "ix_test_results_test_definition_id")]
[Index(nameof(WorkoutSessionId), Name = "ix_test_results_workout_session_id")]
[Index(nameof(CreatedByUserId), Name = "ix_test_results_created_by_user_id")]
public class TestResult : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Column("patient_condition_id")]
    public int? PatientConditionId { get; set; }

    [Required, Column("test_definition_id")]
    public int TestDefinitionId { get; set; }

    [Column("workout_session_id")]
    public int? WorkoutSessionId { get; set; }

    [Required, Column("created_by_user_id")]
    public Guid CreatedByUserId { get; set; } // Обязательный

    [Required, Column("performed_at")]
    public DateTime PerformedAt { get; set; }

    [Column("score_numeric")]
    public decimal? ScoreNumeric { get; set; }

    [Column("result_json"), Required, Comment("JSON: structured test result")]
    public string ResultJson { get; set; } = "{}";

    [Column("notes")]
    public string? Notes { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(PatientConditionId))]
    public virtual PatientCondition? PatientCondition { get; set; }

    [ForeignKey(nameof(TestDefinitionId))]
    public virtual TestDefinition TestDefinition { get; set; } = null!;

    [ForeignKey(nameof(WorkoutSessionId))]
    public virtual WorkoutSession? Session { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedBy { get; set; } = null!;
}