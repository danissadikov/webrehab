using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("patient_assignments")]
[Index(nameof(PatientId), nameof(UserId), IsUnique = true, Name = "ux_patient_assignments_patient_user")]
[Index(nameof(PatientId), Name = "ix_patient_assignments_patient_id")]
[Index(nameof(UserId), Name = "ix_patient_assignments_user_id")]
public class PatientAssignment : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Required, Column("user_id")]
    public int UserId { get; set; }

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public virtual User Therapist { get; set; } = null!;
}