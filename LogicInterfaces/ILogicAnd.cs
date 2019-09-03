namespace LogicInterfaces
{
    public interface ILogicAnd<T> where T: ILogicAnd<T>
    {
        T And(T arg);
        T And(T[] args);
    }
}