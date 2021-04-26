using AutoMapper;
using Microsoft.Extensions.Logging;
using module_10.BLL.DTO;
using module_10.BLL.Infrastructure;
using module_10.BLL.Interfaces;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.BLL.Services
{
    public class HomeworkService : IDTOService<HomeworkDTO, Homework>
    {
        private readonly IRepository<Homework> _homeworkRepository;
        private readonly IHomeworkHandler _homeworkHandler;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public HomeworkService(IRepository<Homework> homeworkRepository,
            IHomeworkHandler homeworkHandler, IBllMapper mapper, ILoggerFactory factory = null)
        {
            _homeworkRepository = homeworkRepository;
            _homeworkHandler = homeworkHandler;
            _logger = factory?.CreateLogger("Homework Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<HomeworkDTO>> GetAllAsync()
        {
            var homework = await _homeworkRepository.GetAllAsync();

            if (!homework.ToList().Any())
            {
                _logger?.LogWarning("There is no homework in data base");
                throw new ValidationException("There is no homework in data base");
            }

            return homework.Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public async Task<HomeworkDTO> GetAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var homework = await _homeworkRepository.GetAsync(id);

            validator.EntityValidation(homework, _logger, nameof(homework));

            return _mapper.Map<HomeworkDTO>(homework);
        }

        public async Task CreateAsync(HomeworkDTO item)
        {
            HomeworkValidation(item);
            var homework = _mapper.Map<Homework>(item);
            await _homeworkRepository.CreateAsync(homework);

            await _homeworkHandler.UpdateAsync(homework,
                HomeworkHandler.HomeworkHandler.UpdateType.AddHomework);
        }

        public async Task UpdateAsync(HomeworkDTO item)
        {
            HomeworkValidation(item);
            var homework = await _homeworkRepository.GetAsync(item.Id);

            var validator = new Validations();
            validator.EntityValidation(homework, _logger, nameof(homework));

            var previousHomeworkPresence = homework.StudentPresence;
            var previousStudentId = homework.StudentId;

            if (previousStudentId != item.StudentId)
                await _homeworkHandler.UpdateAsync(homework,
                    HomeworkHandler.HomeworkHandler.UpdateType.RemoveHomeworkWhileUpdate,
                    previousHomeworkPresence);

            homework.StudentId = item.StudentId;
            homework.LectionId = item.LectionId;
            homework.StudentPresence = item.StudentPresence;
            homework.HomeworkPresence = item.HomeworkPresence;
            homework.Mark = item.Mark;
            homework.Date = item.Date;
            _homeworkRepository.Update(homework);

            if (previousStudentId != homework.StudentId)
                await _homeworkHandler.UpdateAsync(homework,
                    HomeworkHandler.HomeworkHandler.UpdateType.AddHomework,
                    previousHomeworkPresence);
            else
                await _homeworkHandler.UpdateAsync(homework,
                    HomeworkHandler.HomeworkHandler.UpdateType.UpdateHomework,
                    previousHomeworkPresence);
        }

        public IEnumerable<HomeworkDTO> Find(Func<Homework, bool> predicate)
        {
            var homework = _homeworkRepository
                .Find(predicate)
                .ToList();
            return homework
                .Select(h => _mapper.Map<HomeworkDTO>(h));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var homework = await _homeworkRepository.GetAsync(id);
            validator.EntityValidation(homework, _logger, nameof(homework));

            _homeworkRepository.Delete(homework);
            await _homeworkHandler.UpdateAsync(homework, HomeworkHandler.HomeworkHandler.UpdateType.RemoveHomework);
        }

        private void HomeworkValidation(HomeworkDTO homework)
        {
            if (homework.StudentPresence && homework.HomeworkPresence && (homework.Mark < 1 || homework.Mark > 5))
            {
                var mes = $"{homework.Mark} is not suitable. Must be at least 1 and at most 5";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            if ((!homework.StudentPresence || !homework.HomeworkPresence) && homework.Mark > 0)
            {
                var mes = $"{homework.Mark} is not suitable. Must be 0";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }
        }
    }
}
