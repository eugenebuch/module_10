using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace module_10.BLL.Services.Report
{
    public class JsonReportSerializer : ISerializer
    {
        public string Serialize(IEnumerable<Attendance> attendance)
        {
            return JsonConvert.SerializeObject(attendance, Formatting.Indented);
        }
    }
}
