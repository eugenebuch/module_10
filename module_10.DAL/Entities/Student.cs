using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace module_10.DAL.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public float AverageMark { get; set; }

        [Required]
        public int MissedLections { get; set; }

        public virtual List<Homework> StudentHomework { get; set; }
    }
}
