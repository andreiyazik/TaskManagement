using System.Collections.Concurrent;
using TaskManagement.Application.Integrations;

namespace TaskManagement.Application.UnitTests;

public class MockServiceBusHandler : IServiceBusHandler
{
    private readonly ConcurrentQueue<string> _messageQueue = new();

    public Task PublishMessageAsync<T>(T message)
    {
        var serializedMessage = System.Text.Json.JsonSerializer.Serialize(message);
        _messageQueue.Enqueue(serializedMessage);

        return Task.CompletedTask;
    }

    public Task ReceiveMessagesAsync(Func<string, Task> onEventReceived)
    {
        while (_messageQueue.TryDequeue(out var message))
        {
            onEventReceived(message).Wait();
        }

        return Task.CompletedTask;
    }
}
