namespace task17;

using System;
using System.Collections.Concurrent;
using System.Threading;

public class ServerThread
{
    private readonly BlockingCollection<ICommand> _queue = new BlockingCollection<ICommand>();
    private readonly Thread _thread;
    private volatile bool _softStopping = false;
    private volatile bool _hardStopping = false;
    private readonly Action<ICommand, Exception> _exceptionHandler;

    public ServerThread(Action<ICommand, Exception> exceptionHandler = null)
    {
        _exceptionHandler = exceptionHandler;
        _thread = new Thread(Run) { IsBackground = true };
    }

    public void Start() => _thread.Start();

    public void AddCommand(ICommand command)
{
    if (_hardStopping)
        return;
    _queue.Add(command);
}

    public bool IsAlive => _thread.IsAlive;

    public Thread Thread => _thread;

    private void Run()
    {
        try
        {
            while (true)
            {
                ICommand cmd;
                try
                {
                    cmd = _queue.Take();
                }
                catch (WrongThreadException)
                {
                    break;
                }

                try
                {
                    cmd.Execute();
                }
                catch (Exception ex)
                {
                    _exceptionHandler?.Invoke(cmd, ex);
                }

                if (_hardStopping)
                    break;

                if (_softStopping && _queue.Count == 0)
                    break;
            }
        }
        finally
        {
            _queue.Dispose();
        }
    }

    internal void RequestHardStop()
    {
        _hardStopping = true;
        _queue.CompleteAdding();
    }

    internal void RequestSoftStop()
    {
        _softStopping = true;
    }
}
