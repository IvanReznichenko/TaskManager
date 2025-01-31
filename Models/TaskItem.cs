using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class TaskItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string? Description { get; set; }

    public bool IsCompleted { get; set; } = false;

    public string? UserId { get; set; }

    [ForeignKey("UserId")]
    public IdentityUser? User { get; set; }
}
