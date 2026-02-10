using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("patient_conditions")]
[Index(nameof(PatientId), Name = "ix_patient_conditions_patient_id")]
[Index(nameof(ConditionId), Name = "ix_patient_conditions_condition_id")]
[Index(nameof(PatientId), nameof(Status), Name = "ix_patient_conditions_patient_status")]
[Index(nameof(CreatedByUserId), Name = "ix_patient_conditions_created_by_user_id")]
public class PatientCondition : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Required, Column("condition_id")]
    public int ConditionId { get; set; }

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Column("side"), StringLength(50)]
    public string? Side { get; set; }

    [Column("onset_date")]
    public DateOnly? OnsetDate { get; set; }

    [Required, Column("is_primary")]
    public bool IsPrimary { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Required, Column("created_by_user_id")]
    public int CreatedByUserId { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(ConditionId))]
    public virtual Condition Condition { get; set; } = null!;

    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedBy { get; set; } = null!;

    public virtual ICollection<PatientStageStatus> StageStatuses { get; set; } = new List<PatientStageStatus>();
    public virtual ICollection<TestResult> TestResults { get; set; } = new List<TestResult>();
}