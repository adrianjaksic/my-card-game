using System.Collections.Generic;
using System.Linq;

namespace MyGame.BaseGame.Extensions
{
    public static class StackExtensions
    {
        public static void AddToStackAndEmptySource<T>(this Stack<T> source, Stack<T> stack)
        {
            while (stack.Count > 0)
            {
                var item = stack.Pop();
                source.Push(item);
            }
        }

        public static void AddToStackAndEmptySource<T>(this Stack<T> source, List<T> items)
        {
            foreach (var item in items)
            {
                source.Push(item);
            }
            items.Clear();
        }
    }
}
