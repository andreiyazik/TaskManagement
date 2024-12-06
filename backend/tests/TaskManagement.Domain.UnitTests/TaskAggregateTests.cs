using TaskManagement.Domain.Models;
using TaskManagement.Models.Enums;

namespace TaskManagement.Domain.UnitTests;

public class TaskAggregateTests
{
    private const string TestName = "Test Task";
    private const string TestDescription = "This is a test task.";
    private const string AssignedTo = "John Doe";
    private const string ErrorInvalidTransitionTemplate = "Cannot transition from {0} to {1}.";

    [Fact]
    public void Create_ShouldReturnValidTaskAggregate_WhenValidDataProvided()
    {
        // Act
        var result = TaskAggregate.Create(TestName, TestDescription, AssignedTo);

        // Assert
        Assert.True(result.IsSucceeded);
        Assert.NotNull(result.Value);
        Assert.Equal(TestName, result.Value.Name);
        Assert.Equal(TestDescription, result.Value.Description);
        Assert.Equal(Status.NotStarted, result.Value.Status);
        Assert.Equal(AssignedTo, result.Value.AssignedTo);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateStatus_WhenValidTransitionFromNotStartedToInProgress()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;

        // Act
        var result = task.ChangeStatus(Status.InProgress);

        // Assert
        Assert.True(result.IsSucceeded);
        Assert.Equal(Status.InProgress, task.Status);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateStatus_WhenValidTransitionFromInProgressToCompleted()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;
        task.ChangeStatus(Status.InProgress);

        // Act
        var result = task.ChangeStatus(Status.Completed);

        // Assert
        Assert.True(result.IsSucceeded);
        Assert.Equal(Status.Completed, task.Status);
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateStatus_WhenValidTransitionFromInProgressToNotStarted()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;
        task.ChangeStatus(Status.InProgress);

        // Act
        var result = task.ChangeStatus(Status.NotStarted);

        // Assert
        Assert.True(result.IsSucceeded);
        Assert.Equal(Status.NotStarted, task.Status);
    }

    [Fact]
    public void ChangeStatus_ShouldFail_WhenInvalidTransitionFromNotStartedToCompleted()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;

        // Act
        var result = task.ChangeStatus(Status.Completed);

        // Assert
        Assert.False(result.IsSucceeded);
        Assert.Contains(result.Errors, e => e.Message == string.Format(ErrorInvalidTransitionTemplate, Status.NotStarted, Status.Completed));
        Assert.Equal(Status.NotStarted, task.Status);
    }

    [Fact]
    public void ChangeStatus_ShouldFail_WhenInvalidTransitionFromCompletedToInProgress()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;
        task.ChangeStatus(Status.InProgress);
        task.ChangeStatus(Status.Completed);

        // Act
        var result = task.ChangeStatus(Status.InProgress);

        // Assert
        Assert.False(result.IsSucceeded);
        Assert.Contains(result.Errors, e => e.Message == string.Format(ErrorInvalidTransitionTemplate, Status.Completed, Status.InProgress));
        Assert.Equal(Status.Completed, task.Status);
    }

    [Fact]
    public void ChangeStatus_ShouldFail_WhenInvalidTransitionFromCompletedToNotStarted()
    {
        // Arrange
        var task = TaskAggregate.Create(TestName, TestDescription, AssignedTo).Value;
        task.ChangeStatus(Status.InProgress);
        task.ChangeStatus(Status.Completed);

        // Act
        var result = task.ChangeStatus(Status.NotStarted);

        // Assert
        Assert.False(result.IsSucceeded);
        Assert.Contains(result.Errors, e => e.Message == string.Format(ErrorInvalidTransitionTemplate, Status.Completed, Status.NotStarted));
        Assert.Equal(Status.Completed, task.Status);
    }
}
