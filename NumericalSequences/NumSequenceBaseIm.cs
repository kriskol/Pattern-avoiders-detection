using System.Linq;
using System.Threading;

namespace NumericalSequences
{
    public abstract class NumSequenceBaseIm<T> : NumSequenceBaseC<T> where T: NumSequenceBaseIm<T>
    {
        protected readonly byte bitLengthWord = 64;

        public abstract byte CountUsedWords { get; }
        public abstract ulong[] Words { get; }
        
        public override T Clone()
        {
            return CreateNumSequenceThisProp(Words);
        }

        public override bool Equals(T other)
        {
            if (other == null)
                return false;

            if (other.LetterSize == LetterSize &&
                other.SuffixLength == SuffixLength &&
                Words.SequenceEqual(other.Words))
                return true;

            return false;
        }

        protected void ConvertPosition(int position, out int index, out byte positionWord, out int offset)
        {
            index = position * LetterSize / bitLengthWord;
            offset = LetterSize - (bitLengthWord * index) % LetterSize;
            positionWord = (byte)(((((position * LetterSize) % bitLengthWord)) - offset) / LetterSize);
        }

        protected T CreateNumSequenceThisProp(ulong[] words)
        {
            if (maximalLengthSet)
                if (suffixLengthSet)
                    return numSequenceFactory.GetNumSequence(words, LetterSize, Length, MaximalLength, SuffixLength);
                else
                    return numSequenceFactory.GetNumSequence(words, LetterSize, Length, MaximalLength);
                
            
            return numSequenceFactory.GetNumSequence(words, LetterSize, Length);
        }
        protected T CreateNumSequence(ulong[] words, int length, byte letterSize)
        {
            if (maximalLengthSet)
                if (suffixLengthSet)
                    return numSequenceFactory.GetNumSequence(words, letterSize, length, MaximalLength, SuffixLength);
                else
                    return numSequenceFactory.GetNumSequence(words, letterSize, length, MaximalLength);
            

            return numSequenceFactory.GetNumSequence(words, letterSize, length);
        }
        
        
        private byte GetPrefix(ulong word, byte size)
        {
            return (byte) (word & (((ulong)1 << size)-1));
        }
        private byte GetSuffix(ulong word, byte size)
        {
            return (byte) (word >> (bitLengthWord - size));
        }
        private void ComputeSizePrefixSuffix(byte position, int offset, byte size, out byte sizePrefix, out  byte sizeSuffix)
        {
             sizePrefix = (byte) (bitLengthWord - position * size - offset);
             sizeSuffix = (byte) (size - sizePrefix);
        }
        
        private ulong ShiftLeftBasic(ulong word, byte letter, byte size)
        {
            return (word << size) | letter;
        }
        private ulong ShiftLeftBasic(ulong word, byte letter, byte size, out byte overFlow)
        {
            overFlow = GetSuffix(word, size);
            return ShiftLeftBasic(word, letter, size);
        }
        
        private ulong ShiftRightBasic(ulong word, byte size, out byte overFlow)
        {
            overFlow = GetPrefix(word, size);
            return word >> size;
        }
        private ulong ShiftRightBasic(ulong word, byte size, byte letter, out byte overFlow)
        {
            return (ShiftRightBasic(word, size, out overFlow) | ((ulong)letter << (bitLengthWord  - size)));
        }
        
        protected ulong SetLetter(ulong word, int offset, byte position, byte letter, byte size)
        {
            
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = (((word >> ((position + 1) * size + offset)) << size) | letter)
                                  << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected void SetLetter(ulong wordPrefix, ulong wordsSuffix, byte position,
                                    int offset, byte letter, byte size,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            byte letterPrefix = (byte) (letter & (((ulong)1 << sizePrefix) - 1));
            byte letterSuffix = (byte) (letter >> sizePrefix);
            newWordPrefix = SetLetter(wordPrefix, offset, position, letterPrefix, sizePrefix);
            newWordSuffix = SetLetter(wordsSuffix, 0,0, letterSuffix, sizeSuffix);

        }

        protected byte GetLetter(ulong word, int offset, byte position, byte size)
        {
            return (byte)((word >> (position * size + offset)) & (((ulong)1 << size) - 1));
        }
        protected byte GetLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position, byte size)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            byte letterPrefix = GetLetter(wordPrefix, offset, position, sizePrefix);
            byte letterSuffix = GetLetter(wordSuffix, 0,0, sizeSuffix);
            return (byte)(letterPrefix | (letterSuffix << sizePrefix));
        }
        
        protected ulong InsertLetter(ulong word, int offset, byte position, byte letter, byte size)
        {
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = (((word >> (position * size + offset)) << size) | letter) << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected ulong InsertLetter(ulong word, int offset, byte position, byte letter, byte size, out byte overFlow)
        {
            overFlow = GetSuffix(word, size);
            return InsertLetter(word, offset, position, letter, size);
        }
        protected void InsertLetter(ulong wordPrefix, ulong wordSuffix, int offset,
                                    byte position, byte letter, byte size,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset,size, out byte sizePrefix, out byte sizeSuffix);
            byte letterPrefix = (byte) (letter & (((ulong)1 << sizePrefix) - 1));
            newWordPrefix = InsertLetter(wordPrefix, offset, position, letterPrefix, sizePrefix, 
                                    out byte overFlowPrefix);
            byte letterSuffix = (byte)((letter >> sizePrefix) | (overFlowPrefix << sizeSuffix));
            newWordSuffix = ShiftLeftBasic(wordSuffix, letterSuffix, size);
        }
        protected void InsertLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    byte letter, byte size, out byte overFlow,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            overFlow = GetSuffix(wordSuffix, size);
            InsertLetter(wordPrefix, wordSuffix, offset,position, letter,size, out newWordPrefix, out newWordSuffix);
        }
        
        protected ulong DeleteLetter(ulong word, int offset, byte position, byte size)
        {
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = ((word >> ((position+1) * size + offset)) << size) << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected ulong DeleteLetter(ulong word, int offset, byte position, byte size, byte shiftedLetter)
        {
            return DeleteLetter(word, offset, position, size) | ((ulong)shiftedLetter << (bitLengthWord - size));
        }
        protected void DeleteLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    byte size, out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            newWordSuffix = ShiftRightBasic(wordSuffix, size, out byte overFlow);
            byte newLetterPrefix = (byte)(overFlow >> sizeSuffix);
            newWordPrefix = (wordPrefix & (((ulong)1 << (bitLengthWord - sizePrefix)) - 1)) |
                            ((ulong)newLetterPrefix << (bitLengthWord - sizePrefix));
        }
        protected void DeleteLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    byte size, byte shiftedLetter, out ulong newWorPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            newWordSuffix = ShiftRightBasic(wordSuffix, size, shiftedLetter,out byte overFlow);
            byte newLetterPrefix = (byte) (overFlow >> sizeSuffix);
            newWorPrefix = (wordPrefix & (((ulong)1 << (bitLengthWord - sizePrefix)) - 1)) |
                           ((ulong)newLetterPrefix << (bitLengthWord - sizePrefix));
        }
    }
}