using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("template_items")]
[Index(nameof(TemplateId), Name = "ix_template_items_template_id")]
[Index(nameof(TemplateId), nameof(SortOrder), Name = "ix_template_items_template_sort")]
[Index(nameof(ExerciseId), Name = "ix_template_items_exercise_id")]
public class TemplateItem : BaseEntity
{
    [Required, Column("template_id")]
    public int TemplateId { get; set; }

    [Required, Column("sort_order")]
    public int SortOrder { get; set; } = 1;

    [Column("exercise_id")]
    public int? ExerciseId { get; set; }

    [Column("custom_exercise_title"), MaxLength(500)]
    public string? CustomExerciseTitle { get; set; }

    [Column("params_json"), Comment("JSON: instance-specific parameters")]
    public string? ParamsJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(TemplateId))]
    public virtual Template Template { get; set; } = null!;

    [ForeignKey(nameof(ExerciseId))]
    public virtual Exercise? Exercise { get; set; }
}