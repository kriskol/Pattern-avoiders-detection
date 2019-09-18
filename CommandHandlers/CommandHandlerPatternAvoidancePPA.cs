using System.Collections.Generic;
using System.IO;
using PatternAvoidersPPAComputation;
using PermutationsCollections;
using Result;

namespace CommandHandlers
{
    public class CommandHandlerPatternAvoidancePPA : CommandHandlerPatterns, ICommandHandlerPatternAvoidance
    {
        private Dictionary<string, IResultPPAFactory> resultFactories;
        private Dictionary<string, IAvoidersPPAComputationHandler> computationHandlers;
        
        public void AddResultFactory(string key, IResultPPAFactory resultFactory)
        {
            resultFactories[key] = resultFactory;
        }
        protected override void SetDefaultResultFactories()
        {
            AddResultFactory("PPAA", new ResultPPAAFactory());
            AddResultFactory("PPAP", new ResultPPAPFactory());
        }

        public void AddComputationHandler(string key, IAvoidersPPAComputationHandler computationHandler)
        {
            computationHandlers[key] = computationHandler;
        }

        protected override void SetDefaultComputationHandlers()
        {
            AddComputationHandler("PPAA", new AvoidersComputationHandler(new AvoidersComputationFactoryPPAA()));
            AddComputationHandler("PPAP", new AvoidersComputationHandler(new AvoidersComputationFactoryPPAP()));
        }
        
        public void Compute(string command, int n, StreamReader reader, int numThreads)
        {
            IPermutationsCollection avoidedPermutations = ProcessPermutations(reader);
            ResultPPA result = resultFactories[command].CreateResultPPA();
            IAvoidersPPAComputationHandler computationHandler = computationHandlers[command];
            result = computationHandler.Compute(avoidedPermutations, n, result, numThreads);
            ProcessResult(result);
        }

        public CommandHandlerPatternAvoidancePPA()
        {
            SetDefaultResultFactories();
            SetDefaultComputationHandlers();
        }
    }
}