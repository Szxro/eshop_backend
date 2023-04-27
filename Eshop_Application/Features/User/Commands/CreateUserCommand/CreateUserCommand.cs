using Eshop_Application.Common.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eshop_Application.Features.User.Commands.RegisterUserCommand;


public record CreateUserCommand : IRequest<Unit>
{
    public CreateUserCommand()
    {
        UserShippingInfo = new HashSet<CreateUserShippingInfo>();   
    }

    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string ConfirmPassword { get; init; } = string.Empty;

    public ICollection<CreateUserShippingInfo> UserShippingInfo { get; set; }
}

public record CreateUserShippingInfo
{
    public string Address { get; init; } = string.Empty;

    public string Country { get; init; } = string.Empty;

    public string Phone { get; init; } = string.Empty;

    public string State { get; init; } = string.Empty;

    public string City { get; init; } = string.Empty;

    public string PostalCode { get; init; } = string.Empty;

    public string CountryCode { get; init; } = string.Empty;
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IAuthRepository _repository;

    public CreateUserCommandHandler(IAuthRepository repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await _repository.CreateUserAsync(request);

        return Unit.Value;
    }
}

