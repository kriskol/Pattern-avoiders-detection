using System;
using System.Collections.Generic;
using System.Security.Policy;
using CommandAccept;

namespace InputHandling
{
    public class InputHandler : ICommandAcceptVisitor
    {
        private Dictionary<string, IReader> readers;
        private HashSet<string> correctCommands;

        private PatternAvoidanceReader patternAvoidanceReader;
        
        public void Visit(CommandAcceptPatternsAvoidance commandAccept)
        {
            if (patternAvoidanceReader.IntputSet)
            {
                commandAccept.Compute(patternAvoidanceReader.Command, 
                                        patternAvoidanceReader.MaximalLengthAvoiders,
                                        patternAvoidanceReader.PatternsToBeAvoidedFile, 
                                        patternAvoidanceReader.NumExecutionThreads);
            }
            else
                InSufficientArguments();    
        }
        
        protected void AddReader(string command, IReader reader)
        {
            readers[command] = reader;
        }
        
        protected void SetReaders()
        {
            patternAvoidanceReader = new PatternAvoidanceReader();
            AddReader("PPAA", patternAvoidanceReader);
            AddReader("PPAP", patternAvoidanceReader);
        }
        
        protected void Reset()
        {
            correctCommands = new HashSet<string>() {"PPAA","PPAP"};
            readers = new Dictionary<string, IReader>();
            SetReaders();
        }
        
        protected void IncorrectCommand()
        {
            Console.WriteLine("Incorrect command was set. Correct commands are: ");
            foreach (var command in correctCommands)
            {
                Console.WriteLine(command);
                if (command == "")
                    Console.WriteLine("\"empty\" command");
                else
                    Console.WriteLine(command);
            }
        }

        protected InputHandler()
        {
            Reset();
        }

        protected void InSufficientArguments()
        {
            throw new ArgumentException("Supplied arguments were not valid.");
        }
        
        public InputHandler(string[] args):base()
        {
            if ((args.Length == 0 && !correctCommands.Contains(""))
                || !correctCommands.Contains(args[0]))
            {
                IncorrectCommand();
                InSufficientArguments();
            }

            IReader reader;
            
            if (args.Length == 0)
            {
                reader = readers[""];
                if (!reader.ReadArgs(args))
                    InSufficientArguments();
            }
            else
            {
                reader = readers[args[0]];
                if(!reader.ReadArgs(args))
                    InSufficientArguments();
            }
        }
    }
}