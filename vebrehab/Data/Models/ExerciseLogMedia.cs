using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("exercise_log_media")]
[Index(nameof(ExerciseLogId), nameof(MediaAssetId), IsUnique = true, Name = "ux_exercise_log_media_log_media")]
[Index(nameof(ExerciseLogId), Name = "ix_exercise_log_media_exercise_log_id")]
[Index(nameof(MediaAssetId), Name = "ix_exercise_log_media_media_asset_id")]
public class ExerciseLogMedia : BaseEntity
{
    [Required, Column("exercise_log_id")]
    public int ExerciseLogId { get; set; }

    [Required, Column("media_asset_id")]
    public int MediaAssetId { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ExerciseLogId))]
    public virtual ExerciseLog ExerciseLog { get; set; } = null!;

    [ForeignKey(nameof(MediaAssetId))]
    public virtual MediaAsset MediaAsset { get; set; } = null!;
}