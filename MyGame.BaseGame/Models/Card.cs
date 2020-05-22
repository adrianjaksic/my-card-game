namespace MyGame.BaseGame.Models
{
    public class Card
    {
        public int Value { get; set; }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
