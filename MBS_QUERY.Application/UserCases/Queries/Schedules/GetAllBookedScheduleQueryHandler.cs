using System.Xml.Linq;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Schedule;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace MBS_QUERY.Application.UserCases.Queries.Schedules;
public class
    GetAllBookedScheduleQueryHandler(
        IRepositoryBase<Schedule, Guid> scheduleRepository,
        ICurrentUserService currentUserService,
        IRepositoryBase<Feedback, Guid> feedbackRepository,
        IRepositoryBase<Group, Guid> groupRepository)
    : IQueryHandler<Query.GetAllBookedScheduleQuery, List<Response.ScheduleResponse>>
{
    public async Task<Result<List<Response.ScheduleResponse>>> Handle(Query.GetAllBookedScheduleQuery request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(currentUserService.UserId!);
        

        // Fetch all groups the user is a part of asynchronously
        var groupsTask = groupRepository
            .FindAll(x => x.Members != null && x.Members.Any(m => m.StudentId == userId))
            .Include(g => g.Project) // Include related Project data to avoid N+1 query
            .ToListAsync(cancellationToken);

        // Execute the groups query first and get group IDs
        var groups = await groupsTask;
        var groupIds = groups.Select(g => g.Id).ToList();

        if (groupIds.Count == 0)
            return Result.Success(new List<Response.ScheduleResponse>()); // Early return if no groups found

        // Fetch all schedules for these groups in parallel
        var schedulesTask = scheduleRepository
            .FindAll(s => groupIds.Contains(s.GroupId))
            .ToListAsync(cancellationToken);

        var schedules = await schedulesTask;
        if (schedules.Count == 0)
            return Result.Success(new List<Response.ScheduleResponse>()); // Early return if no schedules found

        var scheduleIds = schedules.Select(s => s.Id).ToList();

        // Fetch all feedback for these schedules asynchronously
        var feedbackScheduleIdsTask = feedbackRepository
            .FindAll(f => scheduleIds.Contains((Guid)f.ScheduleId!))
            .Select(f => f.ScheduleId)
            .ToListAsync(cancellationToken);

        var feedbackScheduleIds = await feedbackScheduleIdsTask;

        // Build the response asynchronously
        var res = schedules.Select(s =>
        {
            var group = groups.First(gr => gr.Id == s.GroupId);
            var isFeedback = feedbackScheduleIds.Contains(s.Id);
            var projectName = group.Project?.Name ?? "No Project";

            return new Response.ScheduleResponse
            {
                GroupName = $"{group.Name} - {projectName}",
                StartTime = s.StartTime,
                EndTime = s.EndTime,
                Date = s.Date,
                IsFeedback = isFeedback
            };
        }).ToList();

        return Result.Success(res);
    }
}