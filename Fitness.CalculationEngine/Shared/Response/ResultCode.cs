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
    UserFitnessStatsNotFound = 107,
    FitnessGoalRetrieved = 108,
    UserHasNotGoal = 109,


    //Calculate Status
    UserNotFound = 200,
    CanNotCalculate = 202,
    CalculatedSucess =203,
    UserCalculatedMetricsReturnSucess = 204,
    UserCalculatedMetricsExistBefore = 205,
    NotFoundUserCalculatedMetrics = 206,
    ThereIsNoCalculatedMetricsForThisUser = 207,
    UserStatusRetrived =208,


    //User Fitness Plan
    CanNotUpdateOldPlans = 300,
    OldPlansUpdatedSuccessfully = 301,
    NotMatchedPlanFound = 302,
    PlanRetrivedSuccessfully = 303,
    PlanAssignedSuccessfully = 304,
    PlanAssignmentFailed = 305,
    PlansRetrivedSuccessfully = 306,


    //Fitness Plan Configuration
    PlanConfigurationNotFound = 400,
    PlanConfigurationRetrieved = 401,

}
