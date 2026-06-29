using Fitness.Workout.Features.Queries.GetWorkoutById;
using Fitness.Workout.Features.Queries.GetWorkouts;
using Fitness.Workout.Features.Queries.GetWorkoutsByCategory;
using Fitness.Workout.Features.Queries.GetWorkoutsByPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.Workout.Controllers;

[ApiController]
[Route("api/v1/workouts")]
public class WorkoutsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetWorkouts(
        [FromQuery] string? category,
        [FromQuery] string? difficulty,
        [FromQuery] string? search,
        [FromQuery] int? duration,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var query = new GetWorkoutsQuery
        {
            Category = category,
            Difficulty = difficulty,
            Search = search,
            Duration = duration,
            Page = page,
            PageSize = pageSize
        };

        var result = await mediator.Send(query);
        return Ok(result);
    }

	[HttpGet("{id:int}")]
	public async Task<IActionResult> GetWorkoutById([FromRoute] int id)
    {
        var result = await mediator.Send(new GetWorkoutByIdQuery(id));
        return Ok(result);
    }

    [HttpGet("by-plan/{planId}")]
    public async Task<IActionResult> GetWorkoutsByPlan([FromRoute] string planId)
    {
        var result = await mediator.Send(new GetWorkoutsByPlanQuery(planId));
        return Ok(result);
    }

    [HttpGet("category/{categoryName}")]
    public async Task<IActionResult> GetWorkoutsByCategory(
        [FromRoute] string categoryName,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        var result = await mediator.Send(new GetWorkoutsByCategoryQuery(categoryName, page, pageSize));
        return Ok(result);
    }
}
