using module_10.BLL.DTO;
using module_10.BLL.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace module_10.BLL.Services.Report
{
    public class XMLReportSerializer : ISerializer
    {
        public string Serialize(IEnumerable<Attendance> attendance)
        {
            attendance = attendance.ToList();
            var memoryStream = new MemoryStream();
            var serializer = new XmlSerializer(typeof(List<Attendance>));
            serializer.Serialize(memoryStream, attendance);

            return System.Text.Encoding.UTF8.GetString(memoryStream.ToArray());
        }
    }
}
