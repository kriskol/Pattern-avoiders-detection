
using System;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using ArrayExtensions;

namespace NumericalSequences
{
    public abstract class NumSequenceBasic : NumSequenceBaseIm<NumSequenceBasic>
    {
        private static readonly INumSequenceBasicFactory numSequenceFactory;
        private readonly int maximalLength;
        private readonly int countBlockedBitsFromStart;
        private readonly bool maximalLengthSet;

        protected  bool MaximalLengthSet => maximalLengthSet;
        protected int CountBlockedBitsFromStart => MaximalLengthSet ? countBlockedBitsFromStart : 0;
        protected int MaximalLength => MaximalLengthSet ? maximalLength : 0;
        protected INumSequenceBasicFactory NumSequenceFactory => numSequenceFactory;
        protected override void ConvertPosition(int position, out int index, out byte positionWord, out int offset)
        {
            index = (position * LetterSize + CountBlockedBitsFromStart) / bitLengthWord;
            offset = ((position * LetterSize + CountBlockedBitsFromStart) - (bitLengthWord * index)) % LetterSize;
            positionWord = (byte)(((((position * LetterSize + CountBlockedBitsFromStart) % bitLengthWord)) 
                                   - offset) / LetterSize);
        }
        
        protected override NumSequenceBasic CreateNumSequenceThisProp(ulong[] words)
        {
            return CreateNumSequence(words, Length, LetterSize, CountBlockedBitsFromStart);
        }

        protected override NumSequenceBasic CreateNumSequence(ulong[] words, int length, byte letterSize)
        {
            return CreateNumSequence(words, length, letterSize, CountBlockedBitsFromStart);
        }

        protected NumSequenceBasic CreateNumSequence(ulong[] words, int length ,byte letterSize, 
                                                    int countBlockedBitsFromStart)
        {
            if (SuffixLengthSet)
            {
                if (MaximalLengthSet)
                    return NumSequenceFactory.GetNumSequence(words, letterSize, length, SuffixLength,
                        MaximalLength, countBlockedBitsFromStart);
                return NumSequenceFactory.GetNumSequence(words, letterSize, length, SuffixLength);
            }

            if (MaximalLengthSet)
                return NumSequenceFactory.GetNumSequence(words, letterSize, length, 
                    MaximalLength, countBlockedBitsFromStart);

            return NumSequenceFactory.GetNumSequence(words, letterSize, length);
        }

        protected ulong[] ComputeRelevantNumSequence()
        {
            if (!maximalLengthSet)
                return Words;
            
            ulong[] newWords = new ulong[Words.Length];
            newWords[0] = Words[0] >> countBlockedBitsFromStart;

            ulong letter = 0;
            ulong overFlow = 0;
            for (int i = newWords.Length-1; i >= 0; i--)
            {
                newWords[i] = ShiftRightBasic(newWords[i], (byte) countBlockedBitsFromStart, letter, out overFlow);
                letter = overFlow;
            }

            return newWords;
        }
        
        public override bool Equals(NumSequenceBasic other)
        {
            if (other == null)
                return false;
            
            if (maximalLengthSet)
            {
                if (Length == other.Length &&
                    LetterSize == other.LetterSize &&
                    other.ComputeRelevantNumSequence().SequenceEqual(ComputeRelevantNumSequence()))
                    return true;
                else
                    return false;

            }
                
            return base.Equals(other);
        }

        public override int GetHashCode()
        {
            if (maximalLengthSet)
            {
                ulong[] words = ComputeRelevantNumSequence();
                
                if (SuffixLengthSet && Length > SuffixLength)
                {
                    int index = (Length - SuffixLength) * LetterSize / bitLengthWord;
                    return ComputeHash(words, index);
                }
                else
                {
                    return ComputeHash(words);
                }
            }
            
            
            return base.GetHashCode();
        }

        protected override ulong[] InsertLetterInternal(int position, ulong letter)
        {
            ulong[] words = base.InsertLetterInternal(position, letter);

            if (maximalLengthSet && Length == MaximalLength)
            {
                int newCountBlockedBitsFromStart = countBlockedBitsFromStart + LetterSize;
                if (newCountBlockedBitsFromStart >= bitLengthWord)
                {
                    words = words.Slice(newCountBlockedBitsFromStart / bitLengthWord, words.Length);
                    newCountBlockedBitsFromStart = newCountBlockedBitsFromStart % bitLengthWord;
                }
            }

            return words;
        }

        public override NumSequenceBasic InsertLetter(int position, ulong letter)
        {
            if (maximalLengthSet && Length == MaximalLength)
            {
                ulong[] words = InsertLetterInternal(position, letter);
                int newCountBlockedBitsFromStart = countBlockedBitsFromStart + LetterSize;
                if (newCountBlockedBitsFromStart >= bitLengthWord)
                {
                    words = words.Slice(newCountBlockedBitsFromStart / bitLengthWord, words.Length);
                    newCountBlockedBitsFromStart = newCountBlockedBitsFromStart % bitLengthWord;
                }
                
                return CreateNumSequence(words, Length, LetterSize, newCountBlockedBitsFromStart);
            }
                
            return base.InsertLetter(position, letter);
        }

        public override void InsertLetterMutable(int position, ulong letter)
        {
            base.InsertLetterMutable(position, letter);
        }

        public static INumSequenceBasicFactory GetNumSequenceFactory()
        {
            return numSequenceFactory;
        }
        
        protected NumSequenceBasic(INumSequenceBasicBuilder builder) : base(builder)
        {
            maximalLength = builder.MaximalLength;
            countBlockedBitsFromStart = builder.CountBlockedBitsFromStart;
            maximalLengthSet = builder.MaximalLengthSet;
        }
        
        static NumSequenceBasic()
        {
            numSequenceFactory = new NumSequenceBasicFactory();
        }
    }
}