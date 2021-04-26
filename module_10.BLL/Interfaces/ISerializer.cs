using module_10.BLL.DTO;
using System.Collections.Generic;

namespace module_10.BLL.Interfaces
{
    public interface ISerializer
    {
        string Serialize(IEnumerable<Attendance> attendance);
    }
}
