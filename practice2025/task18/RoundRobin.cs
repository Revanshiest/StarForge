namespace task18;
using System.Collections.Generic;
using task17;

public class RoundRobinScheduler : IScheduler
{
    private readonly Queue<ICommand> _commands = new Queue<ICommand>();

    public bool HasCommand() => _commands.Count > 0;

    public ICommand Select()
    {
        if (!HasCommand()) return null;
        return _commands.Dequeue();
    }

    public void Add(ICommand cmd)
    {
        _commands.Enqueue(cmd);
    }

    public void Requeue(ICommand cmd)
    {
        if (cmd != null)
            _commands.Enqueue(cmd);
    }

    
}

public class LongRunningCommand : ICommand
{
    public bool IsCompleted { get; private set; } = false;
    private int _stepsLeft;

    public LongRunningCommand(int steps)
    {
        _stepsLeft = steps;
    }

    public void Execute()
    {
        if (_stepsLeft > 0)
        {
            _stepsLeft--;
            if (_stepsLeft == 0)
                IsCompleted = true;
        }
    }
}
