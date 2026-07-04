namespace Fitness.Auth.Shared.Responses;

public enum ResultCode
{
    // Register
    DuplicateEmail = 100,
    WeakPassword = 101,
    RegistrationSuccess =102,

    // Login
    InvalidCredentials = 200,
    AccountLockedOut = 201,
    LoginSuccess = 202,

    //Refresh
    RefreshTokenInvalid = 300,
    RefreshSuccess = 301,
}
