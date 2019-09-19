using NumericalSequences;

namespace Patterns
{
    public class PatternBasicFactoryExternal : PatternFactoryExternal, IPatternBasicFactoryExternal
    {
        private INumSequenceBasicFactory numSequenceBasicFactory;
        private IPatternFactory<PatternBasic> patternBasicFactory;
        private 
        
        public PatternBasic CreatePatternBasic(ulong[] words, int length, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(words, letterSize, 
                                                                        length);
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                length - 1, length - 1);
        }
        
        public PatternBasic CreatePatternBasic(ulong[] words, int length, byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(words,
                                                                letterSize, length, suffixLength);
            
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                length - 1, length - 1);
        }
        
        public PatternBasic CreatePatternBasic(ulong[] words, int length, int maximalLength, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(words, letterSize, length, 
                maximalLength, 0);
            
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                length - 1, length - 1);
        }
        
        public PatternBasic CreatePatternBasic(ulong[] words, int length, int maximalLength, 
                                                    byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.GetNumSequence(
                words, letterSize, length, suffixLength, maximalLength, 0);

            return patternBasicFactory.GetPattern(numSequenceBasic, 0,
                                        length - 1, length - 1);
        }
        public PatternBasic CreatePatternBasic(string[] letters, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                                                GetNumSequence(new ulong[] {0}, letterSize, 0);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                                        letters.Length - 1, letters.Length - 1);
        }
        
        public PatternBasic CreatePatternBasic(string[] letters, byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                GetNumSequence(new ulong[] {0}, letterSize, 0, suffixLength);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                                            letters.Length - 1, letters.Length - 1);
        }
        
        public PatternBasic CreatePatternBasic(string[] letters, int maximalLength, byte letterSize)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                GetNumSequence(new ulong[] {0}, letterSize, 0, maximalLength,0);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                letters.Length - 1, letters.Length - 1);
        }

        public PatternBasic CreatePatternBasic(string[] letters, int maximalLength, byte letterSize, int suffixLength)
        {
            NumSequenceBasic numSequenceBasic = numSequenceBasicFactory.
                GetNumSequence(new ulong[] {0}, letterSize, 0, suffixLength, 
                                                        maximalLength,0);
            FillNumSequenceLetters(numSequenceBasic, letters);
            return patternBasicFactory.GetPattern(numSequenceBasic, 0, 
                letters.Length - 1, letters.Length - 1);
        }

        public PatternBasic CreatePatternBasic(ulong[] words, int length, byte letterSize,
            IPatternBasicBuilderAdvancedAttributes attributes)
        {
            
        }

        public PatternBasic CreatePatternBasic(string[] letters, byte letterSize, 
            IPatternBasicBuilderAdvancedAttributes attributes)
        {
            
        }
        
        public PatternBasicFactoryExternal()
        {
            numSequenceBasicFactory = new NumSequenceBasicFactory();
            patternBasicFactory = new PatternBasic.PatternBasicFactory();
        }
        
        public PatternBasicFactoryExternal(INumSequenceBasicFactory numSequenceBasicFactory,
            IPatternFactory<PatternBasic> patternBasicFactory)
        {
            this.numSequenceBasicFactory = numSequenceBasicFactory;
            this.patternBasicFactory = patternBasicFactory;
        }
    }
}