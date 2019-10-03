using System.Collections.Generic;
using System.IO;
using PatternAvoidersPPAComputation;
using Patterns;
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
            IPermutationsCollection avoidedPermutations = ProcessPermutations(reader, n);
            ResultPPA result = resultFactories[command].CreateResultPPA(n);
            IAvoidersPPAComputationHandler computationHandler = computationHandlers[command];
            result = computationHandler.Compute(avoidedPermutations, n, result, numThreads);
            ProcessResult(result);
        }

        public CommandHandlerPatternAvoidancePPA():base( new ResultWriter.ResultWriter(),
                                                    new PermutationBuilderExternal() )
        {
            resultFactories = new Dictionary<string, IResultPPAFactory>();
            computationHandlers = new Dictionary<string, IAvoidersPPAComputationHandler>();
            SetDefaultResultFactories();
            SetDefaultComputationHandlers();
        }
    }
}