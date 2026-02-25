using Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("patient_stage_status")]
[Index(nameof(PatientConditionId), Name = "ix_patient_stage_status_patient_condition_id")]
[Index(nameof(ProtocolId), Name = "ix_patient_stage_status_protocol_id")]
[Index(nameof(CurrentStageId), Name = "ix_patient_stage_status_current_stage_id")]
[Index(nameof(ChangedByUserId), Name = "ix_patient_stage_status_changed_by_user_id")]
[Index(nameof(PatientConditionId), nameof(Status), Name = "ix_patient_stage_status_patient_condition_status")]
public class PatientStageStatus : BaseEntity
{
    [Required, Column("patient_condition_id")]
    public int PatientConditionId { get; set; }

    [Required, Column("protocol_id")]
    public int ProtocolId { get; set; }

    [Required, Column("current_stage_id")]
    public int CurrentStageId { get; set; }

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Required, Column("changed_by_user_id")]
    public Guid ChangedByUserId { get; set; } // Обязательный

    [Required, Column("changed_at")]
    public DateTime ChangedAt { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientConditionId))]
    public virtual PatientCondition PatientCondition { get; set; } = null!;

    [ForeignKey(nameof(ProtocolId))]
    public virtual RehabProtocol Protocol { get; set; } = null!;

    [ForeignKey(nameof(CurrentStageId))]
    public virtual RehabStage CurrentStage { get; set; } = null!;

    [ForeignKey(nameof(ChangedByUserId))]
    public virtual User ChangedBy { get; set; } = null!;
}