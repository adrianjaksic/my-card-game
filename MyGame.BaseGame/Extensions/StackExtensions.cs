using System.Collections.Generic;
using System.Linq;

namespace MyGame.BaseGame.Extensions
{
    public static class StackExtensions
    {
        public static void AddToStackAndEmptySource<T>(this Stack<T> obj, Stack<T> source)
        {
            while (source.Count > 0)
            {
                var item = source.Pop();
                obj.Push(item);
            }
        }

        public static void AddToStackAndEmptySource<T>(this Stack<T> obj, List<T> source)
        {
            foreach (var item in source)
            {
                obj.Push(item);
            }
            source.Clear();
        }
    }
}
