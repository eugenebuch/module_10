using System.Collections.Generic;

namespace module_10.BLL.DTO
{
    public class LecturerDTO
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<LectionDTO> Lections { get; set; }
    }
}
