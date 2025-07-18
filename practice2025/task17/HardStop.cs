namespace task17;
using System;
using System.Threading;

public class HardStop : ICommand
{
    private readonly ServerThread _serverThread;

    public HardStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread != _serverThread.Thread)
            throw new WrongThreadException("HardStop can only be executed in the associated ServerThread.");
        _serverThread.RequestHardStop();
    }
}
