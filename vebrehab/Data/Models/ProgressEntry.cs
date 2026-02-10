using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("progress_entries")]
[Index(nameof(PatientId), Name = "ix_progress_entries_patient_id")]
[Index(nameof(WorkoutSessionId), Name = "ix_progress_entries_workout_session_id")]
[Index(nameof(CreatedByUserId), Name = "ix_progress_entries_created_by_user_id")]
[Index(nameof(PatientId), nameof(RecordedAt), Name = "ix_progress_entries_patient_recorded_at")]
public class ProgressEntry : BaseEntity
{
    [Required, Column("patient_id")]
    public int PatientId { get; set; }

    [Column("workout_session_id")]
    public int? WorkoutSessionId { get; set; }

    [Required, Column("created_by_user_id")]
    public int CreatedByUserId { get; set; }

    [Required, Column("recorded_at")]
    public DateTime RecordedAt { get; set; }

    [Column("pain_json"), Comment("JSON: pain assessment per region")]
    public string? PainJson { get; set; }

    [Column("rom_json"), Comment("JSON: ROM measurements")]
    public string? RomJson { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(PatientId))]
    public virtual Patient Patient { get; set; } = null!;

    [ForeignKey(nameof(WorkoutSessionId))]
    public virtual WorkoutSession? Session { get; set; }

    [ForeignKey(nameof(CreatedByUserId))]
    public virtual User CreatedBy { get; set; } = null!;
}