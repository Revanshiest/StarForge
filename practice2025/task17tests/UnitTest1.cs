using task17;
using CommandLib;

namespace task17tests;

internal class TestCommand : ICommand
{
    private readonly Action _action;
    public TestCommand(Action action) => _action = action;
    public void Execute() => _action();
}

public class ServerThreadTests
{
    [Fact]
    public void ServerThread_HardStopStopsImmediately()
    {
        var serverThread = new ServerThread();
        bool commandExecuted = false;

        serverThread.Start();
        serverThread.AddCommand(new HardStop(serverThread));
        serverThread.AddCommand(new TestCommand(() => commandExecuted = true));

        Thread.Sleep(200);

        Assert.False(commandExecuted);
    }

    [Fact]
    public void ServerThread_SoftStopExecuteAllCommandsBeforeStop()
    {
        var serverThread = new ServerThread();
        bool firstCommand = false;
        bool secondCommand = false;

        serverThread.Start();
        serverThread.AddCommand(new TestCommand(() => firstCommand = true));
        serverThread.AddCommand(new SoftStop(serverThread));
        serverThread.AddCommand(new TestCommand(() => secondCommand = true));

        Thread.Sleep(200);

        Assert.True(firstCommand);
        Assert.True(secondCommand);
    }

    [Fact]
    public void ServerThread_ThrowsExceptionIfHardStopNotInServerThread()
    {
        var serverThread = new ServerThread();
        serverThread.Start();

        Assert.Throws<WrongThreadException>(() =>
        {
            new HardStop(serverThread).Execute();
        });
    }
    
    [Fact]
    public void ServerThread_ThrowsExceptionIfSoftStopNotInServerThread()
    {
        var serverThread = new ServerThread();
        serverThread.Start();

        Assert.Throws<WrongThreadException>(() =>
        {
            new SoftStop(serverThread).Execute();
        });
    }
}