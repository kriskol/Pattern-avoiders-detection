using System;
using System.Text;

namespace ArrayExtensions
{
    public static class ArrayExtensions
    {
        public static T[] Slice<T>(this T[] array, int indexFrom, int indexTo)
        {
            T[] newArray = new T[indexTo - indexFrom];
            
            for (int i = indexFrom; i < indexTo; i++)
                newArray[i - indexFrom] = array[i];

            return newArray;
        }
    }
}