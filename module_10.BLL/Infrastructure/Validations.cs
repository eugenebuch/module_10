using Microsoft.Extensions.Logging;

namespace module_10.BLL.Infrastructure
{
    public class Validations
    {
        public void IdValidation(int? id, ILogger logger = null)
        {
            if (id == null)
            {
                var mes = "id not entered";
                logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }
        }

        public void EntityValidation<T>(T entity, ILogger logger, string type = null) where T : class
        {
            if (entity == null)
            {
                var mes = $"There is no {type} with this id in the database";
                logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }
        }
    }
}
