using System.Collections.Generic;

namespace GeneralCollectionsInterfaces
{
    public interface ICollectionBasic<T>: IAdd<T>, IContain<T>, IRemove<T>, ICount, IEnumerable<T>
    {
        
    }
}