using System.Collections.Generic;
using PatternNode;
using Patterns;

namespace Result
{
    public class ResultPPAA : ResultPPA
    {
        private List<ResultPPAA> clones = new List<ResultPPAA>();
        private Dictionary<int, List<Permutation>> storedData = new Dictionary<int, List<Permutation>>();
        
        public Dictionary<int, List<Permutation>> StoredData => storedData;


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
                    FetchDataClone((ResultPPAA)clone);
        }

        public override void ProcessNode(PatternNodePPA nodePpa)
        {
            Permutation permutation = nodePpa.Permutation;
            
            ProcessPermutation(permutation);
        }

        public override bool TryProcessNodeChildren(PatternNodePPA nodePpa)
        {
            List<PatternNodePPA> children;
            
             if(nodePpa.TryGetChildren(out children))
             {
                 ProcessNodes(children);
                 return true;
             }
              
             return false;
        }

        public override void ProcessPermutation(Permutation permutation)
        {
            if(!storedData.ContainsKey(permutation.Length))
                storedData[permutation.Length] = new List<Permutation>();
            
            storedData[permutation.Length].Add(permutation);
        }

        private void FetchDataClone(ResultPPAA clone)
        {
            Dictionary<int, List<Permutation>> storedDataClone = clone.StoredData;

            foreach (var key in storedDataClone.Keys)
            {
                if(!storedData.ContainsKey(key))
                    storedData[key] = new List<Permutation>();
                    
                storedData[key].AddRange(storedDataClone[key]);
            }
        }

        public override void Accept(IResultWriter writer)
        {
            writer.Visit(this);
        }

        public override ResultPPA GetClone()
        {
            ResultPPAA result = new ResultPPAA();
            clones.Add(result);
            clonesHashed.Add(result);

            return result;
        }

        public ResultPPAA(int maximalLength)
        {
            storedData[maximalLength] = new List<Permutation>();
        }

        public ResultPPAA()
        {
            
        }
    }
}