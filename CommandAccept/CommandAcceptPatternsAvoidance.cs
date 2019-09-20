using System.Collections.Generic;
using System.IO;
using CommandHandlers;

namespace CommandAccept
{
    public class CommandAcceptPatternsAvoidance : CommandAcceptPatterns
    {
        private Dictionary<string, ICommandHandlerPatternAvoidance> handlersPatternAvoidance;
        
        public void Compute(string command, int n, StreamReader reader, int numThreads)
        {
            ICommandHandlerPatternAvoidance handler = handlersPatternAvoidance[command];
            handler.Compute(command, n, reader, numThreads);
        }

        public override void Compute(ICommandAcceptVisitor visitor)
        {
            visitor.Visit(this);
        }

        public void AddHandler(string key, ICommandHandlerPatternAvoidance handlerPatternAvoidance)
        {
            handlersPatternAvoidance[key] = handlerPatternAvoidance;
        }
        
        protected void SetDefaultHandlers()
        {
            AddHandler("PPAA", new CommandHandlerPatternAvoidancePPA());
            AddHandler("PPAP", new CommandHandlerPatternAvoidancePPA());
        }

        public CommandAcceptPatternsAvoidance()
        {
            handlersPatternAvoidance = new Dictionary<string, ICommandHandlerPatternAvoidance>();
            SetDefaultHandlers();
        }
    }
}