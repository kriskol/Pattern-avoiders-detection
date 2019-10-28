using System;
using CommandAccept;
using InputHandling;

namespace PermutationPatterns
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            InputHandler inputHandler = new InputHandler(args);
            ICommandAcceptFactory factory = new CommandAcceptFactory();
            CommandAccept.CommandAccept commandAccept;
            bool commandAccepted;
            
            if (args.Length == 0)
                commandAccepted = factory.CommandAccept("", out commandAccept);
            else
                commandAccepted = factory.CommandAccept(args[0], out commandAccept);

            if (commandAccepted)
                commandAccept.Compute(inputHandler);
            else
                throw new ArgumentException("Supplied arguments were not valid.");
            
        }
    }
}