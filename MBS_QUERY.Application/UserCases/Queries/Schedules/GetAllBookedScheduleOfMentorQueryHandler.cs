using MBS_QUERY.Contract.Abstractions.Messages;
using MBS_QUERY.Contract.Abstractions.Shared;
using MBS_QUERY.Contract.Services.Schedule;
using MBS_QUERY.Domain.Abstractions.Repositories;
using MBS_QUERY.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MBS_QUERY.Application.UserCases.Queries.Schedules;
public class
    GetAllBookedScheduleOfMentorQueryHandler : IQueryHandler<Query.GetAllBookedScheduleOfMentorQuery,
    List<Response.ScheduleResponse>>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IRepositoryBase<Schedule, Guid> _scheduleRepository;
    private readonly IRepositoryBase<Feedback, Guid> _feedbackRepository;

    public GetAllBookedScheduleOfMentorQueryHandler(ICurrentUserService currentUserService,
        IRepositoryBase<Schedule, Guid> scheduleRepository, IRepositoryBase<Feedback, Guid> feedbackRepository)
    {
        _currentUserService = currentUserService;
        _scheduleRepository = scheduleRepository;
        _feedbackRepository = feedbackRepository;
    }

    public async Task<Result<List<Response.ScheduleResponse>>> Handle(Query.GetAllBookedScheduleOfMentorQuery request,
        CancellationToken cancellationToken)
    {
        var UserId = Guid.Parse(_currentUserService.UserId!);
        var schedules = await _scheduleRepository.FindAll(x => x.MentorId == UserId)
            .ToListAsync(cancellationToken: cancellationToken);
        var feedbackIds = await _feedbackRepository
            .FindAll(x => schedules.Select(x => x.Id).Contains((Guid)x.ScheduleId))
            .Select(x => x.ScheduleId).ToListAsync(cancellationToken);

        var result = schedules.Select(x =>
        {
            var isFeedback = feedbackIds.Contains(x.Id);
            return new Response.ScheduleResponse
            {
                Id = x.Id,
                GroupId = x.GroupId,
                GroupName = x.Group.Name,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Date = x.Date,
                IsFeedback = isFeedback,
                IsAccepted = x.IsAccepted
                
            };
        }).ToList();
        return Result.Success(result);
    }
}