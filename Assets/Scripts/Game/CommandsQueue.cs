using System.Collections.Generic;

public class CommandsQueue
{
    private bool _isProcessing = false;
    private readonly List<ICommand> _queue = new();

    public void AddCommand(ICommand command)
    {
        _queue.Add(command);
        if (!_isProcessing)
        {
            ProcessQueue();
        }
    }
    
    private void ProcessQueue()
    {        
        if (_queue.Count == 0) return;

        _isProcessing = true;

        _queue[0].Execute();
        _queue.RemoveAt(0);
        
        _isProcessing = false;
        ProcessQueue();
    }
}