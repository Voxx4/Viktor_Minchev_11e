using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer
{
    public class Interest
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public List<User> Users { get; set; }

        public Area Area { get; set; }
        
        [ForeignKey("Area")]
        public int AreaID { get; set; }

        private Interest()
        {

        }

        public Interest(string name, Area area)
        {
            Name = name;
            Area = area;
        }
    }
}
