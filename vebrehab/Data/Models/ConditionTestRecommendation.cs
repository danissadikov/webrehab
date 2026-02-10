using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("condition_test_recommendations")]
[Index(nameof(ConditionId), Name = "ix_ctr_condition_id")]
[Index(nameof(TestDefinitionId), Name = "ix_ctr_test_definition_id")]
[Index(nameof(ConditionId), nameof(TestDefinitionId), IsUnique = true, Name = "ux_ctr_condition_test")]
[Index(nameof(ConditionId), nameof(Priority), Name = "ix_ctr_condition_priority")]
public class ConditionTestRecommendation : BaseEntity
{
    [Required, Column("condition_id")]
    public int ConditionId { get; set; }

    [Required, Column("test_definition_id")]
    public int TestDefinitionId { get; set; }

    [Column("recommended_stage_code"), StringLength(100)]
    public string? RecommendedStageCode { get; set; }

    [Required, Column("priority")]
    public int Priority { get; set; } = 100;

    [Column("notes")]
    public string? Notes { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ConditionId))]
    public virtual Condition Condition { get; set; } = null!;

    [ForeignKey(nameof(TestDefinitionId))]
    public virtual TestDefinition TestDefinition { get; set; } = null!;
}