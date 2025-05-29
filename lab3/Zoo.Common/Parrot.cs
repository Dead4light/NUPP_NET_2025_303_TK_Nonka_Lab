namespace Zoo.Common
{
    public class Parrot : Animal
    {
        public string Color { get; set; }

        // Конструктор
        public Parrot(string name, int age, string color) : base(name, age)
        {
            Color = color;
        }

        public override void MakeSound()
        {
            Console.WriteLine("Parrot says hello!");
        }
    }
}
