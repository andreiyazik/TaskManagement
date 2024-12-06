using System.Text.Json;

namespace TaskManagement.Application.UnitTests;

public class MockServiceBusHandlerTests
{
    private readonly MockServiceBusHandler _mockHandler;

    public MockServiceBusHandlerTests()
    {
        _mockHandler = new MockServiceBusHandler();
    }

    [Fact]
    public async Task PublishMessageAsync_ShouldAddMessageToQueue()
    {
        // Arrange
        var testMessage = new { Id = 1, Name = "Test Task" };

        // Act
        await _mockHandler.PublishMessageAsync(testMessage);

        // Assert
        var receivedMessages = new List<string>();
        await _mockHandler.ReceiveMessagesAsync(async message =>
        {
            receivedMessages.Add(message);
            await Task.CompletedTask;
        });

        Assert.Single(receivedMessages);
        Assert.Equal(JsonSerializer.Serialize(testMessage), receivedMessages[0]);
    }

    [Fact]
    public async Task ReceiveMessagesAsync_ShouldProcessAllMessagesInQueue()
    {
        // Arrange
        var messages = new[]
        {
            new { Id = 1, Name = "Task 1" },
            new { Id = 2, Name = "Task 2" },
            new { Id = 3, Name = "Task 3" }
        };

        foreach (var message in messages)
        {
            await _mockHandler.PublishMessageAsync(message);
        }

        var processedMessages = new List<string>();

        // Act
        await _mockHandler.ReceiveMessagesAsync(async message =>
        {
            processedMessages.Add(message);
            await Task.CompletedTask;
        });

        // Assert
        Assert.Equal(3, processedMessages.Count);
        for (var i = 0; i < messages.Length; i++)
        {
            Assert.Equal(JsonSerializer.Serialize(messages[i]), processedMessages[i]);
        }
    }

    [Fact]
    public async Task ReceiveMessagesAsync_ShouldHandleEmptyQueueGracefully()
    {
        // Arrange
        var processedMessages = new List<string>();

        // Act
        await _mockHandler.ReceiveMessagesAsync(async message =>
        {
            processedMessages.Add(message);
            await Task.CompletedTask;
        });

        // Assert
        Assert.Empty(processedMessages);
    }

    [Fact]
    public async Task PublishMessageAsync_ShouldAllowMultipleConcurrentMessages()
    {
        // Arrange
        var tasks = new List<Task>();
        var messages = new[]
        {
            new { Id = 1, Name = "Concurrent Task 1" },
            new { Id = 2, Name = "Concurrent Task 2" },
            new { Id = 3, Name = "Concurrent Task 3" }
        };

        foreach (var message in messages)
        {
            tasks.Add(_mockHandler.PublishMessageAsync(message));
        }

        await Task.WhenAll(tasks);

        var processedMessages = new List<string>();

        // Act
        await _mockHandler.ReceiveMessagesAsync(async message =>
        {
            processedMessages.Add(message);
            await Task.CompletedTask;
        });

        // Assert
        Assert.Equal(3, processedMessages.Count);
        foreach (var message in messages)
        {
            Assert.Contains(JsonSerializer.Serialize(message), processedMessages);
        }
    }
}
