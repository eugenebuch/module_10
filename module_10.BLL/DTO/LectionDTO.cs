using System.Collections.Generic;

namespace module_10.BLL.DTO
{
    public class LectionDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? LecturerId { get; set; }

        public List<HomeworkDTO> LectionHomework { get; set; }

    }
}
