namespace CommandAccept
{
    public abstract class CommandAccept
    {
        public abstract void Compute(ICommandAcceptVisitor visitor);
    }
}