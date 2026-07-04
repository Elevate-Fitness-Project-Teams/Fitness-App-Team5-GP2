using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Domain.Services;

public class CalculationService
{
    private Dictionary<ActivityLevel,double> _activityLevelsFactory = new Dictionary<ActivityLevel, double>();
    public CalculationService()
    {
        _activityLevelsFactory.Add(ActivityLevel.Rookie, 1.2);
        _activityLevelsFactory.Add(ActivityLevel.Beginner, 1.375);
        _activityLevelsFactory.Add(ActivityLevel.Intermediate, 1.55);
        _activityLevelsFactory.Add(ActivityLevel.Advance, 1.725);
        _activityLevelsFactory.Add(ActivityLevel.TrueBeast, 1.9);
    }
    public double CalculateBMR (Gender gender , double weight , double height , int age)
    {
        return gender == Gender.Male 
             ? ((10 * weight) + (6.25 * height) - (5 * age) + 5)
             : ((10 * weight) + (6.25 * height) - (5 * age) - 161);
    }

    public double CalculateTDEE(double bmr ,ActivityLevel activityLevel)
    {
        var factory = _activityLevelsFactory[activityLevel];
        return bmr * factory;
    }

    public double CalculateCalorieTarget (double tdee , FitnessGoal goal)
    {
        return goal switch
        {
            FitnessGoal.LoseWeight => tdee - 500,
            FitnessGoal.GainWeight => tdee +300 ,
            FitnessGoal.GainMoreFlexible => tdee + 150,
            _ => tdee
        };
    }

    public UserStatus GetUserStatus(double calorieTarget)
    {
        return calorieTarget <= 1800.0 ? UserStatus.Weak
            : (calorieTarget >= 1801 && calorieTarget <= 2500) ? UserStatus.Normal
            : UserStatus.Hard;
    }

    public CalculatedMetrics RunFullCalculationPipeline(string userId ,double height ,double weight ,int age ,Gender gender ,ActivityLevel activityLevel ,FitnessGoal goal)
    {
        var bmr = CalculateBMR(gender, weight, height, age);
        var tdee = CalculateTDEE(bmr, activityLevel);
        var calorieTarget = CalculateCalorieTarget(tdee, goal);
        var userStatus = GetUserStatus(calorieTarget);

        return new CalculatedMetrics
        {
            UserId = userId,
            Bmr = bmr,
            Status = userStatus,
            Tdee = tdee,
            CalorieTarget = calorieTarget
        };
    }
}
