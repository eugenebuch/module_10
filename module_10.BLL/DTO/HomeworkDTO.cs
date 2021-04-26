using System;

namespace module_10.BLL.DTO
{
    public class HomeworkDTO
    {
        public int Id { get; set; }

        public int? StudentId { get; set; }

        public int? LectionId { get; set; }

        public bool StudentPresence { get; set; }

        public bool HomeworkPresence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
