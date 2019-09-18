namespace CommandAccept
{
    public interface ICommandAcceptFactory
    {
        bool CommandAccept(string command, out CommandAccept commandAccept);
    }
}