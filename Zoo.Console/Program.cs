using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using Zoo.Common;
using Zoo.Infrastructure;
using Zoo.Infrastructure.Data;
using Zoo.Infrastructure.Repositories;
using Zoo.Infrastructure.Services;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Створення DI-контейнера
var services = new ServiceCollection();

// Додаємо контекст БД з підключенням до SQLite
services.AddDbContext<ZooContext>(options =>
    options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
    //options.UseMongoDB(config.GetConnectionString("Mongo")!, config.GetConnectionString("MongoDb")!));

// Реєструємо універсальний репозиторій для роботи з базою
services.AddScoped<IRepository<LionModel>, Repository<LionModel>>();

// Реєстрація інших необхідних сервісів, якщо є
services.AddScoped<ICrudServiceAsync<LionModel>, DataCrudService<LionModel>>();

// Побудова ServiceProvider
var serviceProvider = services.BuildServiceProvider();

// Виконання міграцій
using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ZooContext>();
    context.Database.Migrate(); // Переконайтеся, що міграції створено
}

// Перевірка запуску
Console.WriteLine("Zoo Console App запущено.");

var lion = new LionModel()
{
    Age = Random.Shared.Next(1, 10),
    Name = "Lion",
    IsAlpha = true
};

var service = serviceProvider.GetRequiredService<ICrudServiceAsync<LionModel>>();

if (await service.CreateAsync(lion))
{
    Console.WriteLine("Lion created.");
}

lion.Name = $"Lion {Random.Shared.Next(1, 10)}";

if (await service.UpdateAsync(lion))
{
    Console.WriteLine("Lion updated.");
}

Console.WriteLine("Lions:");

foreach (var element in await service.ReadAllAsync())
{
    Console.WriteLine($"Lion {element.Name} is {element.Age} years old.");   
}

var lions = (await service.ReadAllAsync()).ToList();

Console.WriteLine($"Lions count: {lions.Count}");
Console.WriteLine($"Max Age: {lions.Max(x => x.Age)}");
Console.WriteLine($"Min Age: {lions.Min(x => x.Age)}");
Console.WriteLine($"Average Age: {lions.Average(x => x.Age)}");
