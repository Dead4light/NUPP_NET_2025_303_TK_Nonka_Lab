namespace Zoo.Common
{
    public class Lion : Animal
    {
        public double ManeLength { get; set; }

        // Конструктор
        public Lion(string name, int age, double maneLength) : base(name, age)
        {
            ManeLength = maneLength;
        }

        public override void MakeSound()
        {
            Console.WriteLine("Lion roars!");
        }
    }
}
