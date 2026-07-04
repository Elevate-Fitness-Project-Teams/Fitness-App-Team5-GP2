namespace Fitness.Auth.Infrastructure.MessageBroker.Contacts;

public record UserRegisteredEvent(Guid UserId, string Email);