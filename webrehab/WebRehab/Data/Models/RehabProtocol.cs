using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("rehab_protocols")]
[Index(nameof(ConditionId), Name = "ix_rehab_protocols_condition_id")]
[Index(nameof(OwnerId), Name = "ix_rehab_protocols_owner_user_id")]
[Index(nameof(Visibility), Name = "ix_rehab_protocols_visibility")]
public class RehabProtocol : BaseEntity
{
    [Required, Column("condition_id")]
    public int ConditionId { get; set; }

    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("owner_user_id")]
    public Guid? OwnerId { get; set; }

    [Required, Column("visibility"), StringLength(50)]
    public string Visibility { get; set; } = "global";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ConditionId))]
    public virtual Condition Condition { get; set; } = null!;

    [ForeignKey(nameof(OwnerId))]
    public virtual User? Owner { get; set; }

    public virtual ICollection<RehabStage> Stages { get; set; } = new List<RehabStage>();
    public virtual ICollection<PatientStageStatus> PatientStatuses { get; set; } = new List<PatientStageStatus>();
}