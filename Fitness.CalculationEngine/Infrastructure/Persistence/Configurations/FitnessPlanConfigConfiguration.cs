using Fitness.CalculationEngine.Domain.Entities;
using Fitness.CalculationEngine.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Fitness.CalculationEngine.Infrastructure.Persistence.Configurations;

public class FitnessPlanConfigConfiguration : IEntityTypeConfiguration<FitnessPlanConfig>
{
    public void Configure(EntityTypeBuilder<FitnessPlanConfig> builder)
    {
        builder.HasData(
            new FitnessPlanConfig
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Beginner Weight Loss",
                Description = "A gentle introduction to weight loss with manageable workouts and gradual calorie reduction.",
                Goal = FitnessGoal.LoseWeight,
                Status = UserStatus.Weak,
                MinCalorie = 1500,
                MaxCalorie = 1800,
                WorkoutsPerWeek = 3,
                EstimatedDuration = "45 minutes",
                ProgramType = "Cardio & Light Strength",
                CreatedAt = new DateTime(2024, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Beginner Weight Loss",
                Description = "A gentle introduction to weight loss with manageable workouts and gradual calorie reduction.",
                Goal = FitnessGoal.LoseWeight,
                Status = UserStatus.Weak,
                MinCalorie = 1500,
                MaxCalorie = 1800,
                WorkoutsPerWeek = 3,
                EstimatedDuration = "45 minutes",
                ProgramType = "Cardio & Light Strength",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""

            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Intermediate Weight Loss",
                Description = "Accelerated weight loss plan with increased intensity and stricter calorie management.",
                Goal = FitnessGoal.LoseWeight,
                Status = UserStatus.Normal,
                MinCalorie = 1400,
                MaxCalorie = 1700,
                WorkoutsPerWeek = 4,
                EstimatedDuration = "60 minutes",
                ProgramType = "HIIT & Strength Training",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""

            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Weight Loss",
                Description = "Intensive weight loss program with high-intensity workouts and aggressive calorie deficit.",
                Goal = FitnessGoal.LoseWeight,
                Status = UserStatus.Hard,
                MinCalorie = 1300,
                MaxCalorie = 1600,
                WorkoutsPerWeek = 5,
                EstimatedDuration = "75 minutes",
                ProgramType = "Advanced HIIT & Compound Exercises",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""

            },

            // Muscle Gain Plans
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Beginner Muscle Gain",
                Description = "Foundation building program focused on proper form and progressive overload.",
                Goal = FitnessGoal.GainMoreFlexible,
                Status = UserStatus.Weak,
                MinCalorie = 2500,
                MaxCalorie = 2800,
                WorkoutsPerWeek = 3,
                EstimatedDuration = "50 minutes",
                ProgramType = "Full Body Strength",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Intermediate Muscle Gain",
                Description = "Structured strength program with split routines and increased volume.",
                Goal = FitnessGoal.GainMoreFlexible,
                Status = UserStatus.Normal,
                MinCalorie = 2700,
                MaxCalorie = 3000,
                WorkoutsPerWeek = 4,
                EstimatedDuration = "65 minutes",
                ProgramType = "Upper/Lower Split",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Muscle Gain",
                Description = "Intensive muscle building program with advanced techniques and optimal nutrition timing.",
                Goal = FitnessGoal.GainMoreFlexible,
                Status = UserStatus.Hard,
                MinCalorie = 2900,
                MaxCalorie = 3200,
                WorkoutsPerWeek = 5,
                EstimatedDuration = "80 minutes",
                ProgramType = "Push/Pull/Legs Split",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },

            // Endurance Plans
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Beginner Endurance",
                Description = "Build cardiovascular endurance with steady-state cardio and basic conditioning.",
                Goal = FitnessGoal.GainWeight,
                Status = UserStatus.Weak,
                MinCalorie = 2000,
                MaxCalorie = 2200,
                WorkoutsPerWeek = 3,
                EstimatedDuration = "40 minutes",
                ProgramType = "Steady State Cardio",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Intermediate Endurance",
                Description = "Improve endurance with varied intensity training and longer sessions.",
                Goal = FitnessGoal.GainWeight,
                Status = UserStatus.Normal,
                MinCalorie = 2100,
                MaxCalorie = 2300,
                WorkoutsPerWeek = 4,
                EstimatedDuration = "55 minutes",
                ProgramType = "Interval Training",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Endurance",
                Description = "Maximum endurance training with long-distance sessions and high-volume workouts.",
                Goal = FitnessGoal.GainWeight,
                Status = UserStatus.Hard,
                MinCalorie = 2200,
                MaxCalorie = 2500,
                WorkoutsPerWeek = 5,
                EstimatedDuration = "90 minutes",
                ProgramType = "High Volume Cardio",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },

            // Strength Plans
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Beginner Strength",
                Description = "Develop fundamental strength with basic compound movements and proper technique.",
                Goal = FitnessGoal.GetFitter,
                Status = UserStatus.Weak,
                MinCalorie = 2200,
                MaxCalorie = 2400,
                WorkoutsPerWeek = 3,
                EstimatedDuration = "50 minutes",
                ProgramType = "Compound Lifts",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Intermediate Strength",
                Description = "Increase strength with progressive overload and accessory exercises.",
                Goal = FitnessGoal.GetFitter,
                Status = UserStatus.Normal,
                MinCalorie = 2400,
                MaxCalorie = 2600,
                WorkoutsPerWeek = 4,
                EstimatedDuration = "70 minutes",
                ProgramType = "Powerlifting Focus",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            },
            new FitnessPlanConfig
            {
                Id = Guid.NewGuid(),
                Name = "Advanced Strength",
                Description = "Maximal strength development with periodization and peak performance training.",
                Goal = FitnessGoal.GetFitter,
                Status = UserStatus.Hard,
                MinCalorie = 2600,
                MaxCalorie = 2800,
                WorkoutsPerWeek = 4,
                EstimatedDuration = "90 minutes",
                ProgramType = "Strength & Power",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = ""
            }
        );
    }
}
