using Microsoft.Extensions.Logging;
using module_10.DAL.Entities;

namespace module_10.BLL.Interfaces
{
    public interface IMessageSender
    {
        void Send(Student student, ILogger logger);
    }
}
