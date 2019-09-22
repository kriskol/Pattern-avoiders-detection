using Result;

namespace CommandHandlers
{
    public abstract class CommandHandlerW
    {
        protected IResultWriter resultWriter;

        protected virtual void ProcessResult(Result.Result result)
        {
            result.Accept(resultWriter);
        }
        
        public CommandHandlerW(IResultWriter resultWriter)
        {
            this.resultWriter = resultWriter;
        }
        
    }
}