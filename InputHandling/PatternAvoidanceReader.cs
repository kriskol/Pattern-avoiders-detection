using System;
using System.Collections.Generic;
using System.IO;

namespace InputHandling
{
    public class PatternAvoidanceReader : Reader, IReader

    {
        private string command;
        private int maximalLengthAvoiders;
        private int numExecutionThreads;
        private StreamReader fileReader;
        private bool intputSet;
        private bool correctInput;

        public bool IntputSet => intputSet;
        public string Command => intputSet ? command : "";
        public int MaximalLengthAvoiders => intputSet ? maximalLengthAvoiders : -1;
        public int NumExecutionThreads => intputSet ? numExecutionThreads : -1;
        public StreamReader PatternsToBeAvoidedFile => intputSet ? fileReader : null;
        
        public override bool ReadArgs(string[] args)
        {
            Reset();
            
            if (args.Length == 4)
            {
                
                correctInput &= ProcessCommand(args[0]);
                command = args[0];
                correctInput &= ProcessPositiveInteger(args[1], 1, out maximalLengthAvoiders);
                correctInput &= ProcessPositiveInteger(args[2], 2, out numExecutionThreads);
                correctInput &= ProcessFile(args[3], 3, out fileReader);
            }
            else
            {
                IncorrectNumArguments(4);
                correctInput = false;
            }

            if (!correctInput)
            {
                CorrectInput();

                return false;
            }
            else
            {
                intputSet = true;
            }

            return true;
        }
        
        public override void CorrectInput()
        {
            Console.WriteLine("Correct input:");
            Console.WriteLine("Command MaximalLengthAvoiders NumberOfThreadsUsed FileConsistingOfPatternsToBeAvoided");
        }
        
        
        protected override void SetCorrectCommands()
        {
            AddCorrectCommand("PPAP");
            AddCorrectCommand("PPAA");
        }
        
        protected void Reset()
        {
            correctInput = true;
            intputSet = false;
        }

        public PatternAvoidanceReader():base()
        {
            SetCorrectCommands();
            Reset();
        }
    }
}