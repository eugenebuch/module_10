using System;

namespace module_10.WEB.ViewModels
{
    public class HomeworkViewModel
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
