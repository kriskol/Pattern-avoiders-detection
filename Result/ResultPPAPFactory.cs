namespace Result
{
    public class ResultPPAPFactory : IResultPPAFactory
    {
        public ResultPPA CreateResultPPA()
        {
            return new ResultPPAP();
        }
    }
}