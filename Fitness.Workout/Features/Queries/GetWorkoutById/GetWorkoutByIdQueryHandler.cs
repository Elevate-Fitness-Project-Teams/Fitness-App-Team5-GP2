using System.Net;
using BuildingBlocks.Models;
using Fitness.Workout.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fitness.Workout.Features.Queries.GetWorkoutById;

public class GetWorkoutByIdQueryHandler(WorkoutDbContext context)
    : IRequestHandler<GetWorkoutByIdQuery, ApiResponse<WorkoutDetailDto>>
{
    public async Task<ApiResponse<WorkoutDetailDto>> Handle(
        GetWorkoutByIdQuery request,
        CancellationToken cancellationToken)
    {
        var workout = await context.Workouts
            .Include(w => w.WorkoutExercises)
                .ThenInclude(we => we.Exercise)
            .FirstOrDefaultAsync(w => w.WorkoutId == request.WorkoutId, cancellationToken);

        if (workout is null)
            return ApiResponse<WorkoutDetailDto>.Failure("RES_WORKOUT_NOT_FOUND", HttpStatusCode.NotFound);

        var result = new WorkoutDetailDto
        {
            WorkoutId = workout.WorkoutId,
            Name = workout.Name,
            Category = workout.Category,
            DurationInMinutes = workout.DurationInMinutes,
            Difficulty = workout.Difficulty,
            CaloriesBurn = workout.CaloriesBurn,
            ImageUrl = workout.ImageUrl,
            IsPremium = workout.IsPremium,
            Exercises = workout.WorkoutExercises
                .OrderBy(we => we.OrderIndex)
                .Select(we => new WorkoutExerciseDto
                {
                    OrderIndex = we.OrderIndex,
                    SetsDefault = we.SetsDefault,
                    RepsDefault = we.RepsDefault,
                    RestTimeInSeconds = we.RestTimeInSeconds,
                    ExerciseId = we.ExerciseId,
                    ExerciseName = we.Exercise.Name,
                    TargetMuscles = we.Exercise.TargetMuscles,
                    Equipment = we.Exercise.Equipment,
                    Difficulty = we.Exercise.Difficulty,
                    VideoUrl = we.Exercise.VideoUrl
                })
                .ToList()
        };

        return ApiResponse<WorkoutDetailDto>.Successed(result);
    }
}
