using MediatR;
using User.Application.Command;
using User.Domain;
using User.Domain.Data;
using User.Domain.ValueObject;

namespace User.Application.CommandHandlers;

public class AddTeacherHandler(UserDbContext context) : IRequestHandler<AddTeacher, Guid>
{
    public async Task<Guid> Handle(AddTeacher command, CancellationToken cancellationToken)
    {
        var fullName = new FullName(command.FullName.FirstName, command.FullName.LastName, command.FullName.MiddleName);
        
        var teacher = new Teacher(
            id: Guid.NewGuid(),
            personalNumber: command.PersonalNumber,
            fullName: fullName
            
        );
        
        teacher.SetPassword(command.Password);
        
        await context.Teachers.AddAsync(teacher, CancellationToken.None);
        await context.SaveChangesAsync(cancellationToken);
        
        return teacher.Id;
    }
}