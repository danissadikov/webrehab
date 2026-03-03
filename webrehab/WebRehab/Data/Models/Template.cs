using Data.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

[Table("templates")]
[Index(nameof(OwnerId), Name = "ix_templates_owner_user_id")]
[Index(nameof(Visibility), Name = "ix_templates_visibility")]
public class Template : BaseEntity
{
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

    [ForeignKey(nameof(OwnerId))]
    public virtual User? Owner { get; set; }

    public virtual ICollection<TemplateItem> Items { get; set; } = new List<TemplateItem>();
    public virtual ICollection<WorkoutPlan> WorkoutPlans { get; set; } = new List<WorkoutPlan>();
}