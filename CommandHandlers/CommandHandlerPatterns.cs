using System;
using System.IO;
using System.Text;
using Patterns;
using PermutationsCollections;

namespace CommandHandlers
{
    public abstract class CommandHandlerPatterns : CommandHandlerW
    {
        private IPermutationBuilderExternal builder;
        
        protected IPermutationsCollection ProcessPermutations(StreamReader reader, int n)
        {
            string line;
            string[] parsedLine;
            int maxValue;
            IPermutationsCollection collection = new PermutationCollection();
            
            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                parsedLine = line.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
                maxValue = Math.Max(n, parsedLine.Length);
                byte letterSize = (byte)(int)(Math.Ceiling(Math.Log(maxValue,2))) - 1);
                collection.Add(builder.CreatePattern(parsedLine, letterSize));
            }

            return collection;
        }

        protected abstract void SetDefaultResultFactories();
        protected abstract void SetDefaultComputationHandlers();
    }
}