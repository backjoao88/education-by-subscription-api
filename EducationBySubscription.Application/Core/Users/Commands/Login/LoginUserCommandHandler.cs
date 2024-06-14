using EducationBySubscription.Application.Core.Users.Views;
using EducationBySubscription.Application.Providers.Authentication;
using EducationSubscription.Core.Primitives;
using EducationSubscription.Core.Primitives.Errors;
using EducationSubscription.Core.Repositories;
using MediatR;

namespace EducationBySubscription.Application.Core.Users.Commands.Login;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<LoginViewModel>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtProvider _jwtService;
    
    public LoginUserCommandHandler(IUnitOfWork unitOfWork, IJwtProvider jwtService)
    {
        _unitOfWork = unitOfWork;
        _jwtService = jwtService;
    }

    public async Task<Result<LoginViewModel>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = _jwtService.Encrypt(request.Password);
        var matchedUser = await _unitOfWork.UserRepository.MatchEmailAndPassword(request.Email, passwordHash);
        if (matchedUser is null)
        {
            return Result.Fail<LoginViewModel>(UserErrors.UserInvalidCredentials);
        }
        var token = _jwtService.GenerateAuthenticationToken(matchedUser.Id, matchedUser.UserRole);
        return Result.Ok(new LoginViewModel(token));
    }
}