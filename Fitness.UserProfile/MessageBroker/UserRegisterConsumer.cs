using Fitness.Shared.Messaging.Auth;
using Fitness.UserProfile.Features.Profiles.CreateNewProfile;
using MassTransit;
using MediatR;


namespace Fitness.UserProfile.MessageBroker;

public class UserRegisterConsumer(IMediator mediator, ILogger<UserRegisterConsumer> logger) : IConsumer<UserRegisteredEvent>
{
    private readonly IMediator _mediator = mediator;
    private readonly ILogger<UserRegisterConsumer> _logger = logger;

    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        var request = new CreateNewProfileCommand(context.Message.FirstName,
            context.Message.LastName, context.Message.Email, context.Message.PhoneNumber);
        var result = await _mediator.Send(request);
        if (!result.Success)
            _logger.LogError("Fail To Create User Profile For UserId", context.Message.UserId);

    }

}
