using System.Collections.Generic;

namespace MyGame.BaseGame.Shuffle
{
    public interface IShuffler<T>
    {
        List<T> Shuffle(List<T> items);
    }
}
