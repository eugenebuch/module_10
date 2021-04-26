using System;

namespace module_10.BLL.DTO
{
    public class Attendance
    {
        public string LectionName { get; set; }

        public string LecturerName { get; set; }

        public string StudentName { get; set; }

        public bool StudentPresence { get; set; }

        public bool HomeworkPresence { get; set; }

        public int Mark { get; set; }

        public DateTime Date { get; set; }
    }
}
