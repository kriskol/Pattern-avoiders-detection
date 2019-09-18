using System;
using System.Collections.Generic;
using System.IO;

namespace InputHandling
{
    public abstract class Reader: IReader

    {
    protected HashSet<string> correctCommands;

    protected void AddCorrectCommand(string command)
    {
        correctCommands.Add(command);
    }

    protected abstract void SetCorrectCommands();

    protected bool ProcessPositiveInteger(string numberProcessed, int position, out int number)
    {
        if (!int.TryParse(numberProcessed, out number))
        {
            OnPositionPositiveInteger(position);
            return false;
        }

        return true;
    }

    protected bool ProcessCommand(string command)
    {
        if (correctCommands.Contains(command))
            return true;
        else
        {
            IncorrectCommand();
            return false;
        }
    }

    protected bool ProcessFile(string filePath, int position, out StreamReader reader)
    {
        reader = null;

        try
        {
            reader = new StreamReader(filePath);
        }
        catch (Exception e)
        {
            OnPositionReadableFile(position);
            return false;
        }

        return true;
    }

    protected void IncorrectNumArguments(int correctNumber)
    {
        Console.WriteLine("Incorrect number of arguments was set, correct number of arguments is " + correctNumber);
    }

    protected void OnPositionPositiveInteger(int position)
    {
        Console.WriteLine("On position " + position + " has to be set positive integer.");
    }

    protected void OnPositionReadableFile(int position)
    {
        Console.WriteLine("On position " + position + " has to be set readable file.");
    }

    protected virtual void IncorrectCommand()
    {
        Console.WriteLine("Incorrect command was set. Correct commands are: ");
        foreach (var command in correctCommands)
            Console.WriteLine(command);
    }

    public abstract void CorrectInput();
    public abstract bool ReadArgs(string[] args);

    }
}