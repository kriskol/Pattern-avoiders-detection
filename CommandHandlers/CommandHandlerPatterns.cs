using System;
using System.Collections.Generic;
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
            int maxValue;
            int lengthLongestAvoider = 0;
            IPermutationsCollection collection = new PermutationCollection();
            List<string[]> parsedLines = new List<string[]>();
            string[] parsedLine;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();
                parsedLine = line.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);
                lengthLongestAvoider = Math.Max(lengthLongestAvoider, parsedLine.Length);
                parsedLines.Add(parsedLine);
            }

            for(int i = 0; i < parsedLines.Count; i++)
            {
                parsedLine = parsedLines[i];
                maxValue = Math.Max(n, lengthLongestAvoider);
                byte letterSize = (byte)(int)(Math.Ceiling(Math.Log(maxValue,2))) - 1);
                collection.Add(builder.CreatePattern(parsedLine, letterSize));
            }

            return collection;
        }

        protected abstract void SetDefaultResultFactories();
        protected abstract void SetDefaultComputationHandlers();
    }
}