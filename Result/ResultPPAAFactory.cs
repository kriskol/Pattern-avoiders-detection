namespace Result
{
    public class ResultPPAAFactory : IResultPPAFactory
    {
        public ResultPPA CreateResultPPA()
        {
            return new ResultPPAA();
        }
    }
}