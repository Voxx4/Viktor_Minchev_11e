using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer
{
    public class Area
    {
        [Key]
        public int ID { get; private set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        private Area()
        {

        }

        public Area(string name)
        {
            Name = name;
        }
    }
}
