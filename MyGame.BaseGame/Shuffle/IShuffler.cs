using System.Collections.Generic;

namespace MyGame.BaseGame.Shuffle
{
    public interface IShuffler<T>
    {
        void ShuffleStack(Stack<T> items);
        IEnumerable<T> Shuffle(IEnumerable<T> items);
    }
}
