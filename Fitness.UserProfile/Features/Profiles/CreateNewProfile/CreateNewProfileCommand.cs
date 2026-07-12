using Fitness.UserProfile.Domain.Entities;
using Fitness.UserProfile.Repositories;
using Fitness.UserProfile.Shared.Responses;
using MediatR;

namespace Fitness.UserProfile.Features.Profiles.CreateNewProfile;

public record CreateNewProfileCommand
(string FirstName, string LastName, string Email, string PhoneNumber)
    : IRequest<RequestResult<bool>>;

public class CreateNewProfileCommandHandler(Repository<Profile> repository ,ILogger<CreateNewProfileCommandHandler> logger)
    : IRequestHandler<CreateNewProfileCommand, RequestResult<bool>>
{
    private readonly Repository<Profile> _repository = repository;
    private readonly ILogger<CreateNewProfileCommandHandler> _logger = logger;

    public async Task<RequestResult<bool>> Handle(CreateNewProfileCommand request, CancellationToken cancellationToken)
    {
        var userProfile = new Profile
        {
            IsPremiumCached = false,
            Email = request.Email ??"",
            FirstName = request.FirstName ?? "",
            LastName = request.LastName ??"",
            PhoneNumber = request.PhoneNumber??"",
        };
        _repository.Add(userProfile);
        try
        {
            await _repository.SaveChangeAsync(cancellationToken);
            return RequestResult<bool>.succeeded(true, ResultCode.UserProfileCreatedSuccesfully);
        }
        catch (Exception ex)
        {
            _logger.LogError("Faild To Create User Profile {email}",request.Email);
            return RequestResult<bool>.Failure(false, ResultCode.FailToCreateUserProfile);

        }

    }
}
