using NumericalSequences;

namespace Patterns
{
    public abstract class PatternBuilderExternal<T> : IPatternBuilderExternal<T> where T: PatternC<T>
    {
        protected INumSequenceBasicFactoryExternal factoryNumSequence;
        protected IBasicWsBuilderFactory factoryBuilder;
        protected IPatternFactory<T> patternFactory;
        
        protected void FillNumSequenceLetters(NumSequenceBasic numSequenceBasic, string[] letters)
        {
            int letter;
            
            for (int i = letters.Length - 1; i >= 0; i--)
            {
                int.TryParse(letters[i], out letter);
                numSequenceBasic.InsertLetterMutable(0, (ulong)letter);
            }
        }
        

        public void SetSuffixLength(int suffixLength)
        {
            factoryBuilder.SetSuffixLength(suffixLength);
        }

        public void Reset()
        {
            factoryBuilder.Reset();
        }
        
        
        public T CreatePattern(string[] letters, byte letterSize)
        {
            factoryBuilder.SetLength(0);
            factoryBuilder.SetLetterSize(letterSize);
            factoryBuilder.SetWords(new ulong[] {0});

            INumSequenceBasicWsBuilder builder;
            factoryBuilder.TryGetBuilder(out builder);

            NumSequenceBasic numSequenceBasic = factoryNumSequence.CreateNumSequenceBasic(builder);

            FillNumSequenceLetters(numSequenceBasic, letters);

            return patternFactory.GetPattern(numSequenceBasic, 0,
                letters.Length - 1, letters.Length - 1);
        }

        public T CreatePattern(ulong[] words, byte letterSize, int length)
        {
            factoryBuilder.SetLength(length);
            factoryBuilder.SetLetterSize(letterSize);
            factoryBuilder.SetWords(words);
            
            INumSequenceBasicWsBuilder builder;
            factoryBuilder.TryGetBuilder(out builder);

            NumSequenceBasic numSequenceBasic = factoryNumSequence.CreateNumSequenceBasic(builder);

            return patternFactory.GetPattern(numSequenceBasic, 0,
                length - 1, length - 1);
        }

        public PatternBuilderExternal()
        {
            factoryNumSequence = new NumSequenceBasicFactoryExternal();
            factoryBuilder = new NumSequenceBasicWsBuilder.BasicWsBuilderFactory();
            Reset();
        }

        public PatternBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                        IBasicWsBuilderFactory factoryBuilder,
                                        IPatternFactory<T> patternFactory)
        {
            this.factoryNumSequence = factoryNumSequence;
            this.factoryBuilder = factoryBuilder;
            this.patternFactory = patternFactory;
            Reset();
        }
            
     }
}