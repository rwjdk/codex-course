using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApp.Data;

[Table("TodoItems")]
public sealed class TodoItem : IValidatableObject
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? StartDate { get; set; }

    [Column(TypeName = "date")]
    [DataType(DataType.Date)]
    public DateTime? DueDate { get; set; }

    [DefaultValue(false)]
    public bool Completed { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (StartDate is { } startDate && DueDate is { } dueDate && dueDate.Date < startDate.Date)
        {
            yield return new(
                "Due date must be on or after the start date.",
                [nameof(DueDate)]);
        }
    }
}
