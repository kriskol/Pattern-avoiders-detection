using System.Collections.Generic;
using System.Text;
using PatternNode;
using Patterns;

namespace Result
{
    public class ResultPPAP : ResultPPA
    {
        private List<ResultPPAP> clones = new List<ResultPPAP>();
        private Dictionary<int, int> storedData = new Dictionary<int, int>();

        public Dictionary<int, int> StoredData => storedData;

        public override void DisposeClones()
        {
            clones = null;
            clonesHashed = null;
        }

        public override void FetchDataAllClones()
        {
            foreach (var clone in clones)
                FetchDataClone(clone);
        }

        public override void FetchDataClones(List<Result> clones)
        {
            foreach (var clone in clones)
                if(clonesHashed.Contains(clone))
                    FetchDataClone((ResultPPAP)clone);
        }

        public override void ProcessNode(PatternNodePPA nodePpa)
        {
            Permutation permutation = nodePpa.Permutation;
            
            ProcessPermutation(permutation);
        }

        public override bool TryProcessNodeChildren(PatternNodePPA nodePpa)
        {
            Permutation permutation = nodePpa.Permutation;
            if (!storedData.ContainsKey(permutation.Length + 1))
                storedData[permutation.Length + 1] = 0;

            storedData[permutation.Length + 1] += nodePpa.CountChildren;

            return true;
        }

        public override void ProcessPermutation(Permutation permutation)
        {
            if (!storedData.ContainsKey(permutation.Length))
                storedData[permutation.Length] = 0;

            storedData[permutation.Length]++;
        }

        protected void FetchDataClone(ResultPPAP clone)
        {
            Dictionary<int, int> storedDataClone = clone.StoredData;

            foreach (var key in storedDataClone.Keys)
            {
                if(!storedData.ContainsKey(key))
                    storedData[key] = 0;
                    
                storedData[key] += storedDataClone[key];
            } 
        }

        public override void Accept(IResultWriter writer)
        {
            writer.Visit(this);
        }

        public override ResultPPA GetClone()
        {
            ResultPPAP result = new ResultPPAP();
            clones.Add(result);
            clonesHashed.Add(result);

            return result;
        }
    }
}