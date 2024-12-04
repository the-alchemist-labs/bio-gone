public interface ICommand
{
    void Execute();
}

public interface ICommandPayload
{
    string PlayerId { get; }
}

public interface ICommandFactory
{
    ICommand CreateCommand(ICommandPayload payload);
}