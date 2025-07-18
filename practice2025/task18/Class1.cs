using System.Collections.Concurrent;
using System.Threading;
using task17;

namespace task18
{
    public class SchedulerThread
    {
        private readonly IScheduler _scheduler;
        private readonly BlockingCollection<ICommand> _queue = new();
        private Thread _thread;
        private volatile bool _running = false;

        public SchedulerThread(IScheduler scheduler)
        {
            _scheduler = scheduler;
        }

        public void Start()
        {
            _running = true;
            _thread = new Thread(Run) { IsBackground = true };
            _thread.Start();
        }

        public void AddCommand(ICommand cmd)
        {
            _queue.Add(cmd);
        }

        public void RequestHardStop()
        {
            _running = false;
            _queue.CompleteAdding();
            _thread?.Join();
        }

        private void Run()
        {
            while (_running)
            {
                while (_queue.TryTake(out var cmd))
                {
                    _scheduler.Add(cmd);
                }

                if (_scheduler.HasCommand())
                {
                    var command = _scheduler.Select();
                    command.Execute();
                }
                else
                {
                    Thread.Sleep(10);
                }
            }
        }
    }
}
