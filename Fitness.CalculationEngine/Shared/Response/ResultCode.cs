namespace Fitness.CalculationEngine.Shared.Response;

public enum ResultCode
{
    // User Fitness Stats
    UserIsAlreadyExist = 100,
    InvalidGender = 101,
    InvalidGoal = 102,
    InvalidActivityLevel = 103,
    StatsCreatedSuccesses = 104,
    CanNotCreateUserFitnessStats = 105,
    UserFitnessStatsReturnSucess = 106,


    //Calculate Status
    UserNotFound = 200,
    CanNotCalculate = 202,
    CalculatedSucess =203,


}
