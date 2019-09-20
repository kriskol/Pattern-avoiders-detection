using ExtensionMaps;
using Patterns;
using PermutationContainers;
using PermutationsCollections;

namespace PatternNode
{
    public class PatternNodePPAFactory : IPatternNodePPAFactory
    {
        public PatternNodePPA CreateDefaultPatternNodePpa(byte letterSize, IPermutationsCollection avoidedPermutations)
        {
           IPatternNodePPABuilderFactory nodePpaBuilderFactory = new PatternNodePPABuilder();
            IPermutationContainerPPAFactory containerPpaFactory = 
                                                new PermutationContainerPPAFactory(avoidedPermutations);
            IPermutationBuilderExternal permutationBuilderExternal = new PermutationBuilderExternal();
            IPatternBasicBuilderExternal patternBasicBuilderExternal = new PatternBasicBuilderExternal();
            IExtensionMapFactory extensionMapFactory = new ExtensionMapNumSeqFactory();
            Permutation permutation;
            PatternBasic patternBasic;
            ExtensionMap extensionMap;
            PermutationContainerPPA permutationContainer;

            permutationBuilderExternal.SetSuffixLength(avoidedPermutations.LengthLongestPermutation);
            permutation = permutationBuilderExternal.CreatePattern(new ulong[] {0}, letterSize, 0);
            
            patternBasicBuilderExternal.SetMaximalLength(avoidedPermutations.LengthLongestPermutation);
            patternBasic = patternBasicBuilderExternal.CreatePattern(new ulong[] {0}, letterSize, 0);
            
            
            if(avoidedPermutations.Contains(permutation.Insert(0,0)))
                extensionMap = extensionMapFactory.GetExtensionMapDefault(1, true);
            else
                extensionMap = extensionMapFactory.GetExtensionMapDefault(1, false);

            permutationContainer = containerPpaFactory.CreatePermutation(permutation, patternBasic, extensionMap);
            
            nodePpaBuilderFactory.SetContainerPPA(permutationContainer);

            return CreatePatternNodePpa(nodePpaBuilderFactory);
        }

        public PatternNodePPA CreatePatternNodePpa(IPatternNodePPABuilderFactory nodePpaBuilderFactory)
        {
            IPatternNodePPABuilder builder;

            if (nodePpaBuilderFactory.TryGetBuilder(out builder))
                return new PatternNodePPA(builder);
            else
                return null;

        }
    }
}