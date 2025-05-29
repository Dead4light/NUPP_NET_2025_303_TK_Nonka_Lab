using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zoo.Infrastructure.Data
{
    public class AnimalModel
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public int? ZookeeperId { get; set; }

        public ZookeeperModel Zookeeper { get; set; }
    }
}
