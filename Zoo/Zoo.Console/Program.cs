using Zoo.Common;

Animal.OnAnimalFed += name => Console.WriteLine($"[EVENT] {name} was just fed!");

var lion = new Lion("Simba", 5, 25.3);
var parrot = new Parrot("Polly", 2, "Green");
var zookeeper = new Zookeeper("John Smith", 8);

// CRUD-сервіс
var animalService = new CrudService<Animal>();

animalService.Create(lion);
animalService.Create(parrot);

// Вивід усіх
Console.WriteLine("All animals:");
foreach (var animal in animalService.ReadAll())
{
    Console.WriteLine($"{animal.Name}, Age: {animal.Age}, IsOld: {animal.IsOld()}");
}

// Виклик методів
lion.MakeSound();
parrot.MakeSound();

// Feed з подією
zookeeper.Feed(parrot);

// Update приклад
lion.Age = 11;
animalService.Update(lion);

// Read по Id
var found = animalService.Read(lion.Id);
Console.WriteLine($"Found: {found?.Name}, Age: {found?.Age}");

// Remove
animalService.Remove(parrot);
Console.WriteLine("After removal:");
foreach (var animal in animalService.ReadAll())
{
    Console.WriteLine(animal.Name);
}
