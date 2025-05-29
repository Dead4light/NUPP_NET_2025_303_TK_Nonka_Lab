using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Zoo.Infrastructure.Data
{
    public class ZookeeperModel
    {
        [Key]
        public int Id { get; set; }

        public string FullName { get; set; }

        public List<AnimalModel> Animals { get; set; }
    }
}
