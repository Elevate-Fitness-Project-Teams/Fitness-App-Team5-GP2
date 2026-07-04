namespace Fitness.CalculationEngine.Features.FitnessPlanConfigurations.Common;

public record GetFitnessPlanConfigurationResponse(Guid Id, string Name, string Description, double MinCalorie
    , double MaxCalorie, int WorkoutsPerWeek, string EstimatedDuration, string ProgramType, string Goal, string Status);