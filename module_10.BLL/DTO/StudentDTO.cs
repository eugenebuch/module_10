using System.Collections.Generic;

namespace module_10.BLL.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public float AverageMark { get; set; }

        public int MissedLections { get; set; }

        public List<HomeworkDTO> StudentHomework { get; set; }
    }
}
