using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Models;

namespace Data.Identity;

[Table("users")]
[Index(nameof(Email), IsUnique = true, Name = "ux_users_email")]
[Index(nameof(Role), Name = "ix_users_role")]
[Index(nameof(Status), Name = "ix_users_status")]
public class User : BaseEntity
{
    [Required, Column("email"), MaxLength(255)]
    public string Email { get; set; }

    [Column("full_name"), MaxLength(255)]
    public string? FullName { get; set; }

    [Required, Column("role"), StringLength(50)]
    public string Role { get; set; } = "therapist";

    [Required, Column("status"), StringLength(50)]
    public string Status { get; set; } = "active";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Patient> ManagedPatients { get; set; } = new List<Patient>();
    public virtual ICollection<PatientAssignment> AssignedPatients { get; set; } = new List<PatientAssignment>();
    public virtual ICollection<VisitNote> AuthoredVisitNotes { get; set; } = new List<VisitNote>();
}