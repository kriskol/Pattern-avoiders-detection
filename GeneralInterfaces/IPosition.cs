namespace GeneralInterfaces
{
    public interface IPosition
    {
        int GetPosition(int index);
        int LowestPosition { get; }
        int HighestPosition { get; }
    }
}