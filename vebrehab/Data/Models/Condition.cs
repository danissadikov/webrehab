using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

[Table("conditions")]
[Index(nameof(Code), Name = "ix_conditions_code")]
[Index(nameof(Title), Name = "ix_conditions_title")]
[Index(nameof(BodyRegion), Name = "ix_conditions_body_region")]
public class Condition : BaseEntity
{
    [Column("code"), StringLength(100)]
    public string? Code { get; set; }

    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Column("body_region"), StringLength(100)]
    public string? BodyRegion { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<PatientCondition> PatientConditions { get; set; } = new List<PatientCondition>();
    public virtual ICollection<ConditionTestRecommendation> TestRecommendations { get; set; } = new List<ConditionTestRecommendation>();
    public virtual ICollection<RehabProtocol> RehabProtocols { get; set; } = new List<RehabProtocol>();
}