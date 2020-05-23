using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.BaseGame.Shuffle
{
    public class FisherYatesShuffler<T> : IShuffler<T>
    {
        private Func<int, int> _randomFunc;

        public FisherYatesShuffler(Func<int, int> randomFunc)
        {
            _randomFunc = randomFunc;
        }

        public FisherYatesShuffler()
        {
            _randomFunc = new Random(DateTime.Now.Millisecond).Next;
        }

        public IEnumerable<T> Shuffle(IEnumerable<T> items)
        {
            var array = items.ToArray();
            int n = array.Length;
            for (int i = 0; i < (n - 1); i++)
            {
                // Use Next on random instance with an argument.
                // ... The argument is an exclusive bound.
                //     So we will not go past the end of the array.
                int r = i + Next(n - i);
                ReplaceItems(array, i, r);
            }
            return array;
        }

        public void ShuffleStack(Stack<T> items)
        {
            var array = Shuffle(items);
            items.Clear();
            foreach (var item in array)
            {
                items.Push(item);
            }
        }

        private int Next(int max)
        {
            return _randomFunc(max);
        }

        private void ReplaceItems(T[] array, int firstIndex, int secondIndex)
        {
            T t = array[secondIndex];
            array[secondIndex] = array[firstIndex];
            array[firstIndex] = t;
        }
    }
}
