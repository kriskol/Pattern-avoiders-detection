namespace Result
{
    public interface IResultWriter
    {
        void Visit(ResultPPAA result);
        void Visit(ResultPPAP result);
    }
}