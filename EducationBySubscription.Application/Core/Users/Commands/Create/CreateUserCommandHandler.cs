using EducationBySubscription.Application.Core.Users.Views;
using EducationBySubscription.Application.Providers.Authentication;
using EducationSubscription.Core.Domain.Users;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Primitives.Errors;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Users.Commands.Create;

/// <summary>
/// Represents the <see cref="CreateUserCommand"/> handler.
/// </summary>
public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<UserCreatedViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtProvider;

    public CreateUserCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtProvider)
    {
        _unitOfWork = unitOfWork;
        _jwtProvider = jwtProvider;
    }

    public async Task<Result<UserCreatedViewModel>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isEmailUnique = await _unitOfWork.UserRepository.IsEmailUnique(request.Email);
        if (!isEmailUnique)
        {
            return Result.Fail<UserCreatedViewModel>(UserErrors.UserEmailAlreadyExists);
        }
        var passwordHash = _jwtProvider.Encrypt(request.Password);
        var user = new User(request.Email, passwordHash, request.Role);
        await _unitOfWork.UserRepository.Add(user);
        await _unitOfWork.Complete();
        var userCreatedViewModel = new UserCreatedViewModel(user.Id);
        return Result.Ok(userCreatedViewModel);
    }
}