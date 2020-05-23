using System.Collections.Generic;
using System.Linq;

namespace MyGame.BaseGame.Extensions
{
    public static class StackExtensions
    {
        public static void AddToStackAndEmptySource<T>(this Stack<T> source, Stack<T> stack)
        {
            T[] arr = new T[stack.Count];
            stack.CopyTo(arr, 0);
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                source.Push(arr[i]);
            }
            stack.Clear();
        }

        public static void AddToStackAndEmptySource<T>(this Stack<T> source, List<T> items)
        {
            for (int i = items.Count - 1; i >= 0; i--)
            {
                source.Push(items[i]);
            }
            items.Clear();
        }
    }
}
