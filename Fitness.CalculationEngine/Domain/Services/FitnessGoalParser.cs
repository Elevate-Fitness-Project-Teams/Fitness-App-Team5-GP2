using Fitness.CalculationEngine.Domain.Enums;

namespace Fitness.CalculationEngine.Domain.Services;

public static class FitnessGoalParser
{
    public static bool TryParse(string value , out  FitnessGoal goal)
    {
        var stringWithoutSpaces = value.Trim();
       return Enum.TryParse(stringWithoutSpaces,true, out goal);
    }
}
