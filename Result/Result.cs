using System.Collections.Generic;

namespace Result
{
    public abstract class Result
    {
        protected  HashSet<Result> clonesHashed = new HashSet<Result>();

        public abstract void DisposeClones();
        public abstract void FetchDataAllClones();
        public abstract void FetchDataClones(List<Result> clones);
        public abstract void Accept(IResultWriter writer);
    }
}