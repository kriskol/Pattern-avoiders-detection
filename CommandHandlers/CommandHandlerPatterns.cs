using System.IO;
using PermutationsCollections;

namespace CommandHandlers
{
    public abstract class CommandHandlerPatterns : CommandHandlerW
    {
        protected IPermutationsCollection ProcessPermutations(StreamReader reader)
        {
            
        }

        protected abstract void SetDefaultResultFactories();
        protected abstract void SetDefaultComputationHandlers();
    }
}