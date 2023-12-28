using Newtonsoft.Json;
using NodaTime;
using ProjectManager.Api.Controllers.Models.Todos;
using ProjectManager.Data.Entities;

namespace ProjectManager.Api.Controllers.Models.Statuses;

public class StatusDetailModel
{
    [JsonProperty("id")]
    public Guid Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; } = null!;

    [JsonProperty("project")]
    public Project Project { get; set; } = null!;

    [JsonProperty("todos")]
    public IEnumerable<TodoDetailModel> Todos { get; set; } = Enumerable.Empty<TodoDetailModel>();

    [JsonProperty("isDone")]
    public bool IsDone { get; set; }

    [JsonProperty("createdAt")]
    public Instant CreatedAt { get; set; }
}
public static class StatusDetailModelExtensions
{
    public static StatusDetailModel ToDetail(this Status source)
        => new()
        {
            Id = source.Id,
            Title = source.Title,
            Project = source.Project,
            Todos = source.Todos.Select(x => x.ToDetail()),
            IsDone = source.IsDone,
            CreatedAt = source.CreatedAt,
        };
}
