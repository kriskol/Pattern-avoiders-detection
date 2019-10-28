using NumericalSequences;

namespace Patterns
{
    public abstract class PatternBuilderExternal<T, U> : IPatternBuilderExternal<T,U> where T: PatternC<T>
                                                            where U: IPatternBuilderExternal<T,U>
    {
        protected INumSequenceBasicFactoryExternal factoryNumSequence;
        protected IBasicWsBuilderFactory factoryBuilder;
        
        protected void FillNumSequenceLetters(NumSequenceBasic numSequenceBasic, string[] letters)
        {
            int letter;
            
            for (int i = letters.Length - 1; i >= 0; i--)
            {
                int.TryParse(letters[i], out letter);
                numSequenceBasic.InsertLetterMutable(0, letter);
            }
        }

        protected IBasicWsBuilderFactory GetBuilderFactory()
        {
            return  new NumSequenceBasicWsBuilder.BasicWsBuilderFactory();
        }

        public void SetSuffixLength(int suffixLength)
        {
            factoryBuilder.SetSuffixLength(suffixLength);
        }

        public void Reset()
        {
        }

        protected abstract T ConstructPattern(NumSequenceBasic numSequenceBasic, int highestPosition, int maximum);
        
        
        public T CreatePattern(string[] letters, byte letterSize)
        {
            factoryBuilder.SetLength(0);
            factoryBuilder.SetLetterSize(letterSize);
            factoryBuilder.SetWords(new ulong[] {0});

            INumSequenceBasicWsBuilder builder;
            factoryBuilder.TryGetBuilder(out builder);

            NumSequenceBasic numSequenceBasic = factoryNumSequence.CreateNumSequenceBasic(builder);

            FillNumSequenceLetters(numSequenceBasic, letters);

            return ConstructPattern(numSequenceBasic,
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

            return ConstructPattern(numSequenceBasic,
                length - 1, length - 1);
        }

        public PatternBuilderExternal()
        {
            factoryNumSequence = new NumSequenceBasicFactoryExternal();
            factoryBuilder = new NumSequenceBasicWsBuilder.BasicWsBuilderFactory();
            Reset();
        }

        public PatternBuilderExternal(INumSequenceBasicFactoryExternal factoryNumSequence,
                                        IBasicWsBuilderFactory factoryBuilder)
        {
            this.factoryNumSequence = factoryNumSequence;
            this.factoryBuilder = factoryBuilder;
            Reset();
        }

        public abstract U Clone();
    }
}