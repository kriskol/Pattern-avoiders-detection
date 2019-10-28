using System.Collections.Generic;

namespace CommandAccept
{
    public class CommandAcceptFactory : ICommandAcceptFactory
    {
        private Dictionary<string, CommandAccept> commandAccepts;
        
        public void AddCommandAccept(string command, CommandAccept commandAccept)
        {
            commandAccepts.Add(command, commandAccept);
        }

        protected void SetDefaultCommandAccepts()
        {
            commandAccepts.Add("PPAP", new CommandAcceptPatternsAvoidance());
            commandAccepts.Add("PPAA", new CommandAcceptPatternsAvoidance());
        }
        
        public bool CommandAccept(string command, out CommandAccept commandAccept)
        {
            if (commandAccepts.ContainsKey(command))
            {
                commandAccept = commandAccepts[command];
                return true;
            }
            else
            {
                commandAccept = null;
                return false;
            }
        }
        
        public CommandAcceptFactory()
        {
            commandAccepts = new Dictionary<string, CommandAccept>();
            SetDefaultCommandAccepts();
        }

        public CommandAcceptFactory(Dictionary<string, CommandAccept> commandAccepts)
        {
            this.commandAccepts = commandAccepts;
        }
    }
}