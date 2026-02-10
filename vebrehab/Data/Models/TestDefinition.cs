using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("test_definitions")]
[Index(nameof(OwnerId), Name = "ix_test_definitions_owner_user_id")]
[Index(nameof(Visibility), Name = "ix_test_definitions_visibility")]
[Index(nameof(Title), Name = "ix_test_definitions_title")]
public class TestDefinition : BaseEntity
{
    [Required, Column("title"), MaxLength(500)]
    public string Title { get; set; }

    [Column("description")]
    public string? Description { get; set; }

    [Column("instruction")]
    public string? Instruction { get; set; }

    [Column("result_schema_json"), Comment("JSON Schema for validation")]
    public string? ResultSchemaJson { get; set; }

    [Column("owner_user_id")]
    public int? OwnerId { get; set; }

    [Required, Column("visibility"), StringLength(50)]
    public string Visibility { get; set; } = "global";

    [Required, Column("created_at"), DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime CreatedAt { get; set; }

    [ForeignKey(nameof(OwnerId))]
    public virtual User? Owner { get; set; }

    public virtual ICollection<ConditionTestRecommendation> Recommendations { get; set; } = new List<ConditionTestRecommendation>();
    public virtual ICollection<TestResult> Results { get; set; } = new List<TestResult>();
}