using System.IO;

namespace CommandHandlers
{
    public interface ICommandHandlerPatternAvoidance
    {
        void Compute(string command, int n, StreamReader reader, int numThreads);
    }
}