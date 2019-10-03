namespace Result
{
    public class ResultPPAAFactory : IResultPPAFactory
    {
        public ResultPPA CreateResultPPA(int maximalLength)
        {
            return new ResultPPAA(maximalLength);
        }
    }
}