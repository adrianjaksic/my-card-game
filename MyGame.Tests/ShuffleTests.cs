using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame.BaseGame.Shuffle;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyGame.Tests
{
    [TestClass]
    public class ShuffleTests
    {
        private readonly IShuffler<int> _shuffler;
        private readonly Queue<int> NumberGenerator = new Queue<int>();
        private int ShuffleForTest(int max)
        {
            var num = NumberGenerator.Dequeue(); 
            NumberGenerator.Enqueue(num);
            return Math.Min(num, max);
        }
        private const int NumbersCount = 10;
        private List<int> _numbers;

        public ShuffleTests()
        {
            NumberGenerator.Enqueue(1);
            NumberGenerator.Enqueue(2);
            NumberGenerator.Enqueue(1);
            NumberGenerator.Enqueue(2);
            NumberGenerator.Enqueue(1);
            NumberGenerator.Enqueue(2);
            NumberGenerator.Enqueue(1);
            NumberGenerator.Enqueue(2);
            NumberGenerator.Enqueue(1);
            _shuffler = new FisherYatesShuffler<int>(ShuffleForTest);
            _numbers = new List<int>();
            for (int i = 1; i <= NumbersCount; i++)
            {
                _numbers.Add(i); 
            }
        }

        [TestMethod]
        public void TestIfShuffleChangeTheOrder()
        {
            var shuffledNumbers = _shuffler.Shuffle(_numbers).ToList();
            var different = false;
            for (int i = 0; i < NumbersCount; i++)
            {
                if (_numbers[i] != shuffledNumbers[i])
                {
                    different = true;
                    break;
                }
            }
            Assert.IsTrue(different);
        }
    }
}
