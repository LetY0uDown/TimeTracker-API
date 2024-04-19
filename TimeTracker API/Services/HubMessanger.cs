using Microsoft.AspNetCore.SignalR;
using TimeTracker.Database.Models;

namespace TimeTracker.API.Services;

public class HubMessanger (IHubContext<MainHub> hub)
{
    private readonly IHubContext<MainHub> _hub = hub;

    public async Task TaskCreatedAsync (TrackedTask task)
    {
        await _hub.Clients.All.SendAsync("TaskPosted", task);
    }

    public async Task TaskUpdatedAsync (TaskAction action)
    {
        await _hub.Clients.All.SendAsync("TaskStateUpdated", action);
    }
}