using System;

namespace Zoo.Common
{
    public delegate void AnimalFedHandler(string name);

    public abstract class Animal
    {
        // Статичне поле
        public static int AnimalCount;

        // Подія
        public static event AnimalFedHandler OnAnimalFed;

        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        // Статичний конструктор
        static Animal()
        {
            AnimalCount = 0;
        }

        // Конструктор
        public Animal(string name, int age)
        {
            Id = Guid.NewGuid();
            Name = name;
            Age = age;
            AnimalCount++;
        }

        public abstract void MakeSound();

        // Статичний метод
        public static void FeedAnimal(string name)
        {
            OnAnimalFed?.Invoke(name);
        }
    }
}
