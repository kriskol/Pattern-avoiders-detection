using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;

namespace NumericalSequences
{
    public abstract class NumSequenceBaseIm<T> : NumSequenceBaseC<T> where T: NumSequenceBaseIm<T>
    {
        protected readonly byte bitLengthWord = 64;
        
        public int CountUsedWords => Words.Length;
        protected abstract ulong[] Words { get; set; }
        
        public override T Clone()
        {
            ulong[] newWords = new ulong[Words.Length];

            for (int i = 0; i < Words.Length; i++)
            {
                newWords[i] = Words[i];
            }
            
            return CreateNumSequenceThisProp(newWords);
        }

        protected abstract void ConvertPosition(int position, out int index, out byte positionWord, out int offset);

        protected abstract T CreateNumSequenceThisProp(ulong[] words);
        protected abstract T CreateNumSequence(ulong[] words, int length, byte letterSize);

        protected virtual bool OverFlow(int position, int index, int offset)
        {
            if ((position + 1) * LetterSize + offset <= bitLengthWord)
                return false;
            else
                return true;
        }

        protected ulong[] GetNewWords(ulong[] words, ulong word, int index)
        {
            ulong[] newWords = new ulong[words.Length];
            for (int i = 0; i < words.Length; i++)
                if (index == i)
                    newWords[index] = word;
                else
                    newWords[i] = words[i];

            return newWords;
        }

        protected ulong[] GetNewWords(ulong[] words, ulong wordPrefix, ulong wordSuffix, int index)
        {
            ulong[] newWords = new ulong[words.Length];
            for (int i = 0; i < words.Length; i++)
                if (index == i)
                    newWords[index] = wordPrefix;
                else 
                {
                    if (index + 1 == i)
                        newWords[i] = wordSuffix;
                    else
                        newWords[i] = words[i];
                }
            
            return newWords;
        }

        protected ulong[] GetNewWordsUpToIndex(ulong[] newWords, ulong[] words, ulong word, int index)
        {
            for (int i = 0; i < index; i++)
                newWords[i] = words[i];

            newWords[index] = word;

            return newWords;
        }

        protected ulong[] GetNewWordsUpToIndex(ulong[] newWords, ulong[] words, ulong wordPrefix, 
                                                ulong wordSuffix, int index)
        {
            for (int i = 0; i < index; i++)
                newWords[i] = words[i];

            newWords[index] = wordPrefix;
            newWords[index + 1] = wordSuffix;
 
            return newWords;
        }
        
        private ulong GetPrefix(ulong word, byte size)
        {
            return (word & (((ulong)1 << size)-1));
        }
        private ulong GetSuffix(ulong word, byte size)
        {
            return  (word >> (bitLengthWord - size));
        }
        private void ComputeSizePrefixSuffix(byte position, int offset, byte size, 
                                                out byte sizePrefix, out  byte sizeSuffix)
        {
             sizePrefix = (byte) (bitLengthWord - position * size - offset);
             sizeSuffix = (byte) (size - sizePrefix);
        }
        
        protected ulong ShiftLeftBasic(ulong word, ulong letter, byte size)
        {
            return (word << size) | letter;
        }
        protected ulong ShiftLeftBasic(ulong word, ulong letter, byte size, out ulong overFlow)
        {
            overFlow = GetSuffix(word, size);
            return ShiftLeftBasic(word, letter, size);
        }
        
        protected ulong ShiftRightBasic(ulong word, byte size, out ulong overFlow)
        {
            overFlow = GetPrefix(word, size);
            return word >> size;
        }
        protected ulong ShiftRightBasic(ulong word, byte size, ulong letter, out ulong overFlow)
        {
            return (ShiftRightBasic(word, size, out overFlow) | (letter << (bitLengthWord  - size)));
        }
        
        protected ulong SetLetter(ulong word, int offset, byte position, ulong letter, byte size)
        {
            
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = (((word >> ((position + 1) * size + offset)) << size) | letter)
                                  << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected void SetLetter(ulong wordPrefix, ulong wordsSuffix, byte position,
                                    int offset, ulong letter, byte size,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            ulong letterPrefix = letter & (((ulong)1 << sizePrefix) - 1);
            ulong letterSuffix = letter >> sizePrefix;
            newWordPrefix = SetLetter(wordPrefix, offset, position, letterPrefix, sizePrefix);
            newWordSuffix = SetLetter(wordsSuffix, 0,0, letterSuffix, sizeSuffix);

        }
        public override T SetLetter(int position, ulong letter)
        {
            if(position >= Length || letter >= (ulong)1<<LetterSize)
                throw new ArgumentOutOfRangeException();
            
            ConvertPosition(position, out int index, out byte positionWord, out int offset);

            if (OverFlow(positionWord, index, offset))
            {
                SetLetter(Words[index], Words[index + 1], positionWord, offset, letter, LetterSize,
                    out ulong newWordPrefix, out ulong newWordSuffix);
                return CreateNumSequence(GetNewWords(Words, newWordPrefix, newWordSuffix, index), Length, LetterSize);
            }
            else
            {
                ulong word = SetLetter(Words[index], offset, positionWord, letter, LetterSize);
                return CreateNumSequence(GetNewWords(Words, word, index), Length, LetterSize);
            }
        }

        public override void SetLetterMutable(int position, ulong letter)
        {
            if(position >= Length || letter >= (ulong)1<<LetterSize)
                throw new ArgumentOutOfRangeException();
            
            ConvertPosition(position, out int index, out byte positionWord, out int offset);

            if (OverFlow(positionWord, index, offset))
            {
                SetLetter(Words[index], Words[index + 1], positionWord, offset, letter, LetterSize,
                    out ulong newWordPrefix, out ulong newWordSuffix);
                Words = GetNewWords(Words, newWordPrefix, newWordSuffix, index);
            }
            else
            {
                ulong word = SetLetter(Words[index], offset, positionWord, letter, LetterSize);
                Words = GetNewWords(Words, word, index);
            }
        }

        protected ulong GetLetter(ulong word, int offset, byte position, byte size)
        {
            return (word >> (position * size + offset)) & (((ulong)1 << size) - 1);
        }
        protected ulong GetLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position, byte size)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            ulong letterPrefix = GetLetter(wordPrefix, offset, position, sizePrefix);
            ulong letterSuffix = GetLetter(wordSuffix, 0,0, sizeSuffix);
            return letterPrefix | (letterSuffix << sizePrefix);
        }

        public override ulong GetLetter(int position)
        {
            if(position >= Length)
                throw new ArgumentOutOfRangeException();
            
            ConvertPosition(position, out int index, out byte positionWord, out int offset);

            if (OverFlow(positionWord, index, offset))
            {
                return GetLetter(Words[index], Words[index + 1], offset, positionWord, LetterSize);
            }
            else
            {
                return GetLetter(Words[index], offset, positionWord, LetterSize);
            }
        }

        protected ulong InsertLetter(ulong word, int offset, byte position, ulong letter, byte size)
        {
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = (((word >> (position * size + offset)) << size) | letter) << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected ulong InsertLetter(ulong word, int offset, byte position, ulong letter, byte size, out ulong overFlow)
        {
            overFlow = GetSuffix(word, size);
            return InsertLetter(word, offset, position, letter, size);
        }
        protected void InsertLetter(ulong wordPrefix, ulong wordSuffix, int offset,
                                    byte position, ulong letter, byte size,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset,size, out byte sizePrefix, out byte sizeSuffix);
            ulong letterPrefix = letter & (((ulong)1 << sizePrefix) - 1);
            newWordPrefix = InsertLetter(wordPrefix, offset, position, letterPrefix, sizePrefix, 
                                    out ulong overFlowPrefix);
            ulong letterSuffix = (letter >> sizePrefix) | (overFlowPrefix << sizeSuffix);
            newWordSuffix = ShiftLeftBasic(wordSuffix, letterSuffix, size);
        }
        protected void InsertLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    ulong letter, byte size, out ulong overFlow,
                                    out ulong newWordPrefix, out ulong newWordSuffix)
        {
            overFlow = GetSuffix(wordSuffix, size);
            InsertLetter(wordPrefix, wordSuffix, offset,position, letter,size, out newWordPrefix, out newWordSuffix);
        }

        protected virtual ulong[] InsertLetterInternal(int position, ulong letter)
        {

            ConvertPosition(position, out int index, out byte positionWord, out int offset);

            ulong[] newWords;

            if ((Length + 1) * LetterSize > Words.Length * bitLengthWord)
            {
                newWords = new ulong[Words.Length + 1];
                newWords[newWords.Length-1] = 0;
            }
            else
                newWords = new ulong[Words.Length];
            
            int newIndex;
            ulong overFlow;
            
            if (OverFlow(positionWord, index, offset))
            {
                
                InsertLetter(Words[index], Words[index + 1], offset,
                    positionWord, letter, LetterSize, out overFlow,
                    out ulong newWordPrefix, out ulong newWordSuffix);

                GetNewWordsUpToIndex(newWords, Words, newWordPrefix, newWordSuffix, index);
                newIndex = index + 2;
            }
            else
            {
                GetNewWordsUpToIndex(newWords, Words,
                    InsertLetter(Words[index], offset,
                        positionWord, letter, LetterSize, out overFlow), index);
                newIndex = index + 1;
            }

            ulong newOverFlow;

            for (int i = newIndex; i < newWords.Length; i++)
            {
                newWords[i] = ShiftLeftBasic(Words[i], overFlow, LetterSize, out newOverFlow);
                overFlow = newOverFlow;
            }

            return newWords;
        }
        
        public override T InsertLetter(int position, ulong letter)
        {
            if(position > Length + 1 || letter >= (ulong)1<<LetterSize)
                throw new ArgumentOutOfRangeException();
            
            return CreateNumSequence(InsertLetterInternal(position, letter), Length + 1, LetterSize);
        }

        public override void InsertLetterMutable(int position, ulong letter)
        {
            if(position > Length + 1 || letter >= (ulong)1<<LetterSize)
                throw new ArgumentOutOfRangeException();

            ulong[] words = InsertLetterInternal(position, letter);
            Words = words;
            Length = Length + 1;
        }

        protected ulong DeleteLetter(ulong word, int offset, byte position, byte size)
        {
            ulong newWordPrefix = word & (((ulong)1 << (position * size + offset)) - 1);
            ulong newWordSuffix = ((word >> ((position+1) * size + offset))) << (position * size + offset);
            return newWordPrefix | newWordSuffix;
        }
        protected ulong DeleteLetter(ulong word, int offset, byte position, byte size, ulong shiftedLetter)
        {
            return DeleteLetter(word, offset, position, size) | (shiftedLetter << (bitLengthWord - size));
        }
        protected void DeleteLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    byte size, out ulong newWordPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            newWordSuffix = ShiftRightBasic(wordSuffix, size, out ulong overFlow);
            ulong newLetterPrefix = overFlow >> sizeSuffix;
            newWordPrefix = (wordPrefix & (((ulong)1 << (bitLengthWord - sizePrefix)) - 1)) |
                            (newLetterPrefix << (bitLengthWord - sizePrefix));
        }
        protected void DeleteLetter(ulong wordPrefix, ulong wordSuffix, int offset, byte position,
                                    byte size, ulong shiftedLetter, out ulong newWorPrefix, out ulong newWordSuffix)
        {
            ComputeSizePrefixSuffix(position, offset, size, out byte sizePrefix, out byte sizeSuffix);
            newWordSuffix = ShiftRightBasic(wordSuffix, size, shiftedLetter,out ulong overFlow);
            ulong newLetterPrefix =  overFlow >> sizeSuffix;
            newWorPrefix = (wordPrefix & (((ulong)1 << (bitLengthWord - sizePrefix)) - 1)) |
                           (newLetterPrefix << (bitLengthWord - sizePrefix));
        }

        public override T DeleteLetterPosition(int position)
        { 
            if(position >= Length)
                throw new ArgumentOutOfRangeException();
            
            ConvertPosition(position, out int index, out byte positionWord, out int offset);

            int newIndex;

            if (OverFlow(positionWord, index, offset))
                newIndex = index + 2;
            else
                newIndex = index + 1;

            ulong[] newWords;
            ulong overFlow;
            
            if((Length-(position+1))*LetterSize + index * bitLengthWord 
                + offset + positionWord*LetterSize <= (Words.Length - 1)*bitLengthWord)
            {
                newWords = new ulong[Math.Max(Words.Length-1,1)];
                ShiftRightBasic(Words[Words.Length-1], LetterSize, out overFlow);
            }
            else
            {
                newWords = new ulong[Math.Max(Words.Length-1,1)];
                newWords[newWords.Length-1] = ShiftRightBasic(Words[Words.Length-1], LetterSize, out overFlow);
            }

            ulong letterShifted = overFlow;
            
            for (int i = newWords.Length - 2; i >= newIndex; i--)
            {
                newWords[i] = ShiftRightBasic(Words[i], LetterSize, letterShifted, out overFlow);
                letterShifted = overFlow;
            }

            if (newWords.Length - 2 < newIndex)
                letterShifted = 0;

            if (OverFlow(positionWord, index, offset))
            {
                DeleteLetter(Words[index], Words[index + 1], offset, positionWord, LetterSize,
                            letterShifted, out ulong newWordPrefix, out ulong newWordSuffix);
                GetNewWordsUpToIndex(newWords, Words, newWordPrefix, newWordSuffix, index);
            }
            else
            {
                GetNewWordsUpToIndex(newWords, Words,
                                    DeleteLetter(Words[index], offset,
                                        positionWord, LetterSize, letterShifted),
                                        index);
            }

            return CreateNumSequence(newWords, Length - 1, LetterSize);
        }

        public override T Switch(int positionFrom, int positionTo)
        {
            T numSequence = this.InsertLetter(positionTo, GetLetter(positionFrom));

            if (positionTo <= positionFrom)
                positionFrom++;
            
            return numSequence.DeleteLetterPosition(positionFrom);
        }

        protected ulong ChangeLetterGetChangedLetter(ulong letter, int difference)
        {
            if (difference > 0)
                letter = letter + (ulong)difference;
            else if (letter > (ulong) (-1 * difference))
                letter = letter - (ulong) (-1 * difference);
            else
                letter = 0;

            return letter;
        }
        
        protected ulong ChangeLetter(ulong word, int offset, byte position, int difference, byte size)
        {
            ulong letter = word >> (position * LetterSize + offset) & (((ulong)1<< LetterSize)-1) ;

            letter = ChangeLetterGetChangedLetter(letter, difference);
            
            return ((((word >> ((position + 1) * LetterSize + offset)) << LetterSize) | letter)
                       << (position * LetterSize + offset)) |
                      word & ((((ulong) 1) << position * LetterSize + offset) - 1);
        }

        protected ulong[] ChangeWordsMutable(IEnumerable<int> positions, int difference)
        {
            ulong[] newWords = new ulong[Words.Length];

            for (int i = 0; i < newWords.Length; i++)
                newWords[i] = Words[i];
            
            foreach (var position in positions)
            {
                ConvertPosition(position, out int index, out  byte positionWord, out int offset);

                if (!OverFlow(positionWord, index, offset))
                    newWords[index] = ChangeLetter(newWords[index], offset, positionWord, difference, LetterSize);
                else
                {
                    ulong letter = GetLetter(newWords[index], newWords[index + 1],
                                        offset, positionWord, LetterSize);
                    letter = ChangeLetterGetChangedLetter(letter, difference);
                    SetLetter(newWords[index], newWords[index + 1], positionWord, offset, 
                                    letter, LetterSize, out ulong newWordPrefix, out ulong newWordSuffix);
                    
                    newWords[index] = newWordPrefix;
                    newWords[index + 1] = newWordSuffix;
                } 
            }

            return newWords;

            /*
            int index;
            byte positionWord;
            int offset;
            ulong letter;
            ulong newWord;
            ulong word;
            ulong[] newWords = new ulong[Words.Length];

            for (int i = 0; i < Words.Length; i++)
            {
                newWords[i] = Words[i];
            }
            
            foreach (var position in positions)
            {
                ConvertPosition(position, out index, out positionWord, out offset);
                word = newWords[index];
                letter = word >> (positionWord * LetterSize + offset) & (((ulong)1<< LetterSize)-1) ;
                
                if (difference > 0)
                    letter = letter + (ulong)difference;
                else if (letter > (ulong) (-1 * difference))
                    letter = letter - (ulong) (-1 * difference);
                else
                    letter = 0;
                newWord = ((((word >> ((positionWord + 1) * LetterSize + offset)) << LetterSize) | letter)
                           << (positionWord * LetterSize + offset)) |
                          word & ((((ulong) 1) << position * LetterSize + offset) - 1);

                newWords[index] = newWord;
            }

            return newWords;
            */
        }
        
        public override T Change(IEnumerable<int> positions, int difference)
        {

            return CreateNumSequenceThisProp(ChangeWordsMutable(positions, difference));
        }

        public override void ChangeMutable(IEnumerable<int> positions, int difference)
        {
            Words = ChangeWordsMutable(positions, difference);
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            if (Length == 0)
                return "";
            
            ConvertPosition(0, out int index, out byte positionWord, out int offset);
            ulong word;
            ulong number;
            
            for (int i = 0; i < Length; i++)
            {
                int position = (positionWord + 1) * LetterSize + offset;
                
                if (position <= bitLengthWord)
                {
                    word = Words[index];
                    stringBuilder.Append((word >> (positionWord*LetterSize + offset)) & ((((ulong)1)<<LetterSize)-1));
                    stringBuilder.Append('-');

                    if (position < bitLengthWord)
                    {
                        positionWord++;
                    }
                    else
                    {
                        positionWord = 0;
                        offset = 0;
                        index++;
                    }
                }
                else
                { 
                    byte size = (byte) (bitLengthWord - (position - LetterSize));
                    number = GetSuffix(Words[index], size);
                    number = number | (GetPrefix(Words[index + 1], (byte)(LetterSize - size)) << size);
                    stringBuilder.Append(number);
                    stringBuilder.Append("-");

                    index++;
                    positionWord = 0;
                    offset = LetterSize - size;
                }
            }

            return stringBuilder.Remove(stringBuilder.Length - 1, 1).ToString();

        }
        
        protected int ComputeHash(ulong[] words, int index = 0)
        {
            int hash = 0;

            for (int i = 0; i < words.Length; i++)
            {
                hash = hash * 17 + Words[i].GetHashCode();
            }

            return hash;
        }
        
        public override int GetHashCode()
        {
            if (SuffixLengthSet && Length > SuffixLength)
            {
                ConvertPosition(Length-SuffixLength, out int index, out byte positionWord, out int offset);
                return ComputeHash(Words, index);
            }
            else
            {
                return ComputeHash(Words);
            }
        }
        
        public override bool Equals(T other)
        {
            if (other == null)
                return false;

            if (other.Length == Length &&
                other.LetterSize == LetterSize &&
                Words.SequenceEqual(other.Words))
                return true;

            return false;
        }
        
        protected NumSequenceBaseIm(INumSequenceBaseBuilder builder) : base(builder)
        {
        }
    }
}