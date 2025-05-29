namespace Zoo.Common
{
    public class Zookeeper
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int ExperienceYears { get; set; }

        // Конструктор
        public Zookeeper(string fullName, int experienceYears)
        {
            Id = Guid.NewGuid();
            FullName = fullName;
            ExperienceYears = experienceYears;
        }

        public void Feed(Animal animal)
        {
            Console.WriteLine($"{FullName} fed {animal.Name}.");
            Animal.FeedAnimal(animal.Name);
        }
    }
}
