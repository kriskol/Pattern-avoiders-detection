namespace Result
{
    public class ResultPPAPFactory : IResultPPAFactory
    {
        public ResultPPA CreateResultPPA(int maximalLength)
        {
            return new ResultPPAP(maximalLength);
        }
    }
}