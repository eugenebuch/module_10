using module_10.BLL.Services.HomeworkHandler;
using module_10.DAL.Entities;
using System.Threading.Tasks;

namespace module_10.BLL.Interfaces
{
    public interface IHomeworkHandler
    {
        Task UpdateAsync(Homework homework, HomeworkHandler.UpdateType updateType, bool previousPresence = true);
    }
}
