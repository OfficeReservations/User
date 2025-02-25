using MediatR;
using User.Domain.ValueObject;

namespace User.Application.Command;

public class AddTeacher : IRequest<Guid>
{
    public required string PersonalNumber { get; init; }
    public required FullName FullName { get; init; }
    public required string Password { get; init; }
}