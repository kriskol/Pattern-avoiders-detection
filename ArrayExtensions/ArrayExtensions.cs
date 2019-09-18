using System;

namespace ArrayExtensions
{
    public static class ArrayExtensions
    {

        public static T[] Slice<T>(this T[] array, int indexFrom, int indexTo)
        {
            if (array == null ||
                indexFrom >= indexTo ||
                indexFrom < 0 ||
                indexTo < 0 ||
                array.Length < indexTo)
                throw new ArgumentOutOfRangeException();

            T[] newArray = new T[indexTo - indexFrom];

            for (int i = indexFrom; i < indexTo; i++)
                newArray[i - indexFrom] = array[i];

            return newArray;
        }
    }
}