using System.Xml.Linq;
using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Schedule;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace MBS_QUERY.Application.UserCases.Queries.Schedules;

public class
    GetAllBookedScheduleQueryHandler : IQueryHandler<Query.GetAllBookedScheduleQuery, List<Response.ScheduleResponse>>
{
    private readonly IRepositoryBase<Schedule, Guid> _scheduleRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepositoryBase<Feedback, Guid> _feedbackRepository;
    private readonly IRepositoryBase<Group, Guid> _groupRepository;

    public GetAllBookedScheduleQueryHandler(IRepositoryBase<Schedule, Guid> scheduleRepository,
        ICurrentUserService currentUserService, IRepositoryBase<Feedback, Guid> feedbackRepository,
        IRepositoryBase<Group, Guid> groupRepository)
    {
        _scheduleRepository = scheduleRepository;
        _currentUserService = currentUserService;
        _feedbackRepository = feedbackRepository;
        _groupRepository = groupRepository;
    }

    public async Task<Result<List<Response.ScheduleResponse>>> Handle(Query.GetAllBookedScheduleQuery request,
        CancellationToken cancellationToken)
    {
        var userId = Guid.Parse(_currentUserService.UserId!);
    
        // Fetch all groups the user is a part of
        var groups = await _groupRepository.FindAll(x => x.Members.Any(m => m.StudentId == userId))
            .Include(g => g.Project) // Include related Project data to avoid N+1 query
            .ToListAsync(cancellationToken);

        var groupIds = groups.Select(g => g.Id).ToList();
    
        // Fetch all schedules for these groups
        var schedules = await _scheduleRepository.FindAll(s => groupIds.Contains(s.GroupId))
            .ToListAsync(cancellationToken);

        var scheduleIds = schedules.Select(s => s.Id).ToList();
    
        // Fetch all feedback for these schedules in one go
        var feedbackScheduleIds = await _feedbackRepository.FindAll(f => scheduleIds.Contains((Guid)f.ScheduleId))
            .Select(f => f.ScheduleId)
            .ToListAsync(cancellationToken);

        // Build the response
        var res = schedules.Select(s => {
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