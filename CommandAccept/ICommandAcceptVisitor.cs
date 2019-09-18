namespace CommandAccept
{
    public interface ICommandAcceptVisitor
    {
        void Visit(CommandAcceptPatternsAvoidance commandAccept);
    }
}