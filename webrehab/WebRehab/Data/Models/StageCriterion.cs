using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("stage_criteria")]
[Index(nameof(StageId), Name = "ix_stage_criteria_stage_id")]
public class StageCriterion : BaseEntity
{
    [Required, Column("stage_id")]
    public int StageId { get; set; }

    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Required, Column("criteria_json"), Comment("JSON: rule definitions (pain/rom/tests)")]
    public string CriteriaJson { get; set; } = "{}";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(StageId))]
    public virtual RehabStage Stage { get; set; } = null!;
}