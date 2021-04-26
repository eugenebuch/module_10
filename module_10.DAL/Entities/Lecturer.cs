using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Lecturer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        public virtual List<Lection> Lections { get; set; }
    }
}
