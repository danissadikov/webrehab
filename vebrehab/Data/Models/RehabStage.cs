using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("rehab_stages")]
[Index(nameof(ProtocolId), Name = "ix_rehab_stages_protocol_id")]
[Index(nameof(ProtocolId), nameof(SortOrder), Name = "ix_rehab_stages_protocol_sort")]
[Index(nameof(ProtocolId), nameof(Code), IsUnique = true, Name = "ux_rehab_stages_protocol_code")]
public class RehabStage : BaseEntity
{
    [Required, Column("protocol_id")]
    public int ProtocolId { get; set; }

    [Required, Column("code"), StringLength(100)]
    public string Code { get; set; }

    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Required, Column("sort_order")]
    public int SortOrder { get; set; } = 1;

    [Column("goals")]
    public string? Goals { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(ProtocolId))]
    public virtual RehabProtocol Protocol { get; set; } = null!;

    public virtual ICollection<StageCriterion> Criteria { get; set; } = new List<StageCriterion>();
    public virtual ICollection<PatientStageStatus> PatientStatuses { get; set; } = new List<PatientStageStatus>();
}