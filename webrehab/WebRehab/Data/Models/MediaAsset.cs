using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("media_assets")]
[Index(nameof(UploadedByUserId), Name = "ix_media_assets_uploaded_by_user_id")]
[Index(nameof(PatientId), Name = "ix_media_assets_patient_id")]
public class MediaAsset : BaseEntity
{
    [Required, Column("kind"), StringLength(50)]
    public string Kind { get; set; }

    [Required, Column("url")]
    public string Url { get; set; }

    [Column("uploaded_by_user_id")]
    public Guid? UploadedByUserId { get; set; }

    [Column("patient_id")]
    public int? PatientId { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(UploadedByUserId))]
    public virtual User? UploadedBy { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient? Patient { get; set; }

    public virtual ICollection<ExerciseMedia> ExerciseMedia { get; set; } = new List<ExerciseMedia>();
    public virtual ICollection<ExerciseLogMedia> ExerciseLogMedia { get; set; } = new List<ExerciseLogMedia>();
}