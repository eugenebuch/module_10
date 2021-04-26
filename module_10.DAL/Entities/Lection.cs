using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Lection
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? LecturerId { get; set; }
        public virtual Lecturer Lecturer { get; set; }

        public virtual List<Homework> LectionHomework { get; set; }
    }
}
