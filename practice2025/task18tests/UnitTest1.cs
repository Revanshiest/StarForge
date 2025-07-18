namespace task18tests;

using System.Threading;
using task18;
using task17;
using Xunit;

public class TestCommand : ICommand
{
    private int _executedCount = 0;
    public int ExecutedCount => _executedCount;
    public void Execute() => Interlocked.Increment(ref _executedCount);
}

public class SchedulerThreadTests
{
    [Fact]
    public void SchedulerThread_Executes_Queued_Command()
    {
        var scheduler = new RoundRobinScheduler();
        var thread = new SchedulerThread(scheduler);
        var cmd = new TestCommand();

        thread.Start();
        thread.AddCommand(cmd);

        Thread.Sleep(200);

        thread.RequestHardStop();

        Assert.True(cmd.ExecutedCount > 0);
    }

    [Fact]
    public void SchedulerThread_Executes_Scheduled_Command()
    {
        var scheduler = new RoundRobinScheduler();
        var thread = new SchedulerThread(scheduler);
        var cmd = new TestCommand();

        scheduler.Add(cmd);

        thread.Start();

        Thread.Sleep(200);

        thread.RequestHardStop();

        Assert.True(cmd.ExecutedCount > 0);
    }

    [Fact]
    public void RoundRobinScheduler_Cycles_Commands()
    {
        var scheduler = new RoundRobinScheduler();
        var cmd1 = new TestCommand();
        var cmd2 = new TestCommand();

        scheduler.Add(cmd1);
        scheduler.Add(cmd2);

        var selected1 = scheduler.Select();
        var selected2 = scheduler.Select();

        Assert.True(selected1 == cmd1 || selected1 == cmd2);
        Assert.True(selected2 == cmd1 || selected2 == cmd2);
        Assert.NotEqual(selected1, selected2);
    }
}
