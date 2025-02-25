using Grpc.Core;
using MediatR;
using User.Application.Command;

namespace User.Api.Services;

public class UserGrpcService(IMediator mediator) : UserService.UserServiceBase
{
    public override async Task<AddTeacherResponse> AddTeacher(AddTeacherRequest request, ServerCallContext context)
    {

        var fullName = new Domain.ValueObject.FullName(request.Teacher.FullName.FirstName,
            request.Teacher.FullName.LastName, request.Teacher.FullName.MiddleName);
        
        
        var command = new AddTeacher
        {
            PersonalNumber = request.Teacher.PersonalNumber,
            FullName = fullName,
            Password = request.Teacher.Password
        };
        
        var response = await mediator.Send(command, CancellationToken.None);

        return new AddTeacherResponse { Id = response.ToString() };
    }
}