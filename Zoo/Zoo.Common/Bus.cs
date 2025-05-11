using System;

namespace Zoo.Common
{
    public class Bus
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public int Capacity { get; set; }
        public double Mileage { get; set; }

        public Bus()
        {
            Id = Guid.NewGuid();
        }

        public static Bus CreateNew()
        {
            var rand = new Random();
            return new Bus
            {
                Brand = $"Brand {rand.Next(1, 100)}",
                Capacity = rand.Next(20, 80),
                Mileage = rand.NextDouble() * 100_000
            };
        }
    }
}
