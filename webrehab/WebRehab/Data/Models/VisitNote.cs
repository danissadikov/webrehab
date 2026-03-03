using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Data.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("visit_notes")]
[Index(nameof(PatientId), Name = "ix_visit_notes_patient_id")]
[Index(nameof(AuthorUserId), Name = "ix_visit_notes_author_user_id")]
[Index(nameof(PatientId), nameof(VisitDate), Name = "ix_visit_notes_patient_date")]
public class VisitNote : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Required, Column("author_user_id")]
    public Guid AuthorUserId { get; set; } // Обязательный

    [Required, Column("visit_date")]
    public DateOnly VisitDate { get; set; }

    [Column("diagnosis_text")]
    public string? DiagnosisText { get; set; }

    [Column("subjective")]
    public string? Subjective { get; set; }

    [Column("objective")]
    public string? Objective { get; set; }

    [Column("assessment")]
    public string? Assessment { get; set; }

    [Column("plan")]
    public string? Plan { get; set; }

    [Column("extra_json"), Comment("JSON: additional structured data")]
    public string? ExtraJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(AuthorUserId))]
    public virtual User Author { get; set; } = null!;
}