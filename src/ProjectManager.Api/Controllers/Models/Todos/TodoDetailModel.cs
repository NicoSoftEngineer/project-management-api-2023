using NodaTime.Text;
using ProjectManager.Data.Entities;

namespace ProjectManager.Api.Controllers.Models.Todos;

public class TodoDetailModel
{
    public Guid Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string CreatedAt { get; set; } = null!;
}

public class TodoFilter
{
    public Guid? ProjectId { get; set; }
}

public static class TodoDetailModelExtensions
{
    public static IQueryable<Todo> ApplyFilter(this IQueryable<Todo> query, TodoFilter? filter)
    {
        if (filter != null)
        {
            if (filter.ProjectId != null)
            {
                query = query.Where(x => x.ProjectId ==  filter.ProjectId);
            }
        }

        return query;
    }

    public static TodoDetailModel ToDetail(this Todo source)
        => new()
        {
            Id = source.Id,
            Description = source.Description,
            Title = source.Title,
            CreatedAt = InstantPattern.ExtendedIso.Format(source.CreatedAt),
        };
}
