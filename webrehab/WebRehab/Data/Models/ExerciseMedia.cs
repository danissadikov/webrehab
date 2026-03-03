using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("exercise_media")]
[Index(nameof(ExerciseId), nameof(MediaAssetId), IsUnique = true, Name = "ux_exercise_media_exercise_media")]
[Index(nameof(ExerciseId), Name = "ix_exercise_media_exercise_id")]
[Index(nameof(MediaAssetId), Name = "ix_exercise_media_media_asset_id")]
public class ExerciseMedia : BaseEntity
{
    [Required, Column("exercise_id")]
    public int ExerciseId { get; set; }

    [Required, Column("media_asset_id")]
    public int MediaAssetId { get; set; }

    [Required, Column("purpose"), StringLength(50)]
    public string Purpose { get; set; } = "instruction";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ExerciseId))]
    public virtual Exercise Exercise { get; set; } = null!;

    [ForeignKey(nameof(MediaAssetId))]
    public virtual MediaAsset MediaAsset { get; set; } = null!;
}