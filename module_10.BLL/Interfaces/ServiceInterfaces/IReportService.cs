using module_10.BLL.DTO;
using System;
using System.Collections.Generic;

namespace module_10.BLL.Interfaces.ServiceInterfaces
{
    public interface IReportService
    {
        string MakeStudentReport(string firstName, string lastName, Func<IEnumerable<Attendance>, string> serializer = null);
        string MakeLectionReport(string lectionName, Func<IEnumerable<Attendance>, string> serializer = null);
    }
}
