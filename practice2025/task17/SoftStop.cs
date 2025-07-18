namespace task17;
using System;
using System.Threading;

public class SoftStop : ICommand
{
    private readonly ServerThread _serverThread;

    public SoftStop(ServerThread serverThread)
    {
        _serverThread = serverThread;
    }

    public void Execute()
    {
        if (Thread.CurrentThread != _serverThread.Thread)
            throw new WrongThreadException("SoftStop can only be executed in the associated ServerThread.");
        _serverThread.RequestSoftStop();
    }
}
