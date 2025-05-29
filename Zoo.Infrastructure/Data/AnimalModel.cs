using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data
{
    public class AnimalModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public Guid? ZookeeperId { get; set; }

        public ZookeeperModel Zookeeper { get; set; }
    }
}
