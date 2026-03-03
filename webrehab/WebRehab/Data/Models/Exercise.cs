using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("exercises")]
[Index(nameof(OwnerId), Name = "ix_exercises_owner_user_id")]
[Index(nameof(Visibility), Name = "ix_exercises_visibility")]
[Index(nameof(BodyRegion), Name = "ix_exercises_body_region")]
[Index(nameof(Title), Name = "ix_exercises_title")]
public class Exercise : BaseEntity
{
    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("body_region"), StringLength(100)]
    public string? BodyRegion { get; set; }

    [Column("owner_user_id")]
    public Guid? OwnerId { get; set; }

    [Required, Column("visibility"), StringLength(50)]
    public string Visibility { get; set; } = "global";

    [Column("params_schema_json"), Comment("JSON Schema for exercise parameters")]
    public string? ParamsSchemaJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public virtual User? Owner { get; set; }

    public virtual ICollection<ExerciseMedia> ExerciseMedia { get; set; } = new List<ExerciseMedia>();
    public virtual ICollection<TemplateItem> TemplateItems { get; set; } = new List<TemplateItem>();
    public virtual ICollection<WorkoutExercise> WorkoutExercises { get; set; } = new List<WorkoutExercise>();
}