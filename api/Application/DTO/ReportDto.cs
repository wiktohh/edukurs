using Domain.Entities;
using Domain.ValueObjects.RepTask;
using Domain.ValueObjects.User;

namespace Application.DTO;

public record ReportDto (Guid Id, string Path, Guid UserId, Guid RepTaskId);
