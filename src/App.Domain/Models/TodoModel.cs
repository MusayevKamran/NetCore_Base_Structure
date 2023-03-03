using App.Domain.Core;

namespace App.Domain.Models;

public class TodoModel : EntityBase
{
    public virtual string Name { get; set; }
}