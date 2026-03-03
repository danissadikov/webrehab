using Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("workout_plans")]
[Index(nameof(PatientId), Name = "ix_workout_plans_patient_id")]
[Index(nameof(CreatedByUserId), Name = "ix_workout_plans_created_by_user_id")]
[Index(nameof(SourceTemplateId), Name = "ix_workout_plans_source_template_id")]
[Index(nameof(Status), Name = "ix_workout_plans_status")]
public class WorkoutPlan : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Required, Column("created_by_user_id")]
    public Guid CreatedByUserId { get; set; } // Обязательный

    [Column("title"), MaxLength(500)]
    public string? Title { get; set; }

    [Column("source_template_id")]
    public int? SourceTemplateId { get; set; }

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedBy { get; set; } = null!;

    [ForeignKey(nameof(SourceTemplateId))]
    public virtual Template? SourceTemplate { get; set; }

    public virtual ICollection<WorkoutSession> Sessions { get; set; } = new List<WorkoutSession>();
}