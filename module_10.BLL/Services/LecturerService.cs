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
    public class LecturerService : IDTOService<LecturerDTO, Lecturer>
    {
        private readonly IRepository<Lecturer> _lecturerRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LecturerService(IRepository<Lecturer> repository, IBllMapper mapper, ILoggerFactory factory = null)
        {
            _lecturerRepository = repository;
            _logger = factory?.CreateLogger("Lecturer Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<LecturerDTO>> GetAllAsync()
        {
            var Lecturers = await _lecturerRepository.GetAllAsync();

            if (!Lecturers.Any())
            {
                var mes = "There is no Lecturers in data base";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            return Lecturers
                .Select(p => _mapper.Map<LecturerDTO>(p));
        }

        public async Task<LecturerDTO> GetAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var Lecturer = await _lecturerRepository.GetAsync(id);

            validator.EntityValidation(Lecturer, _logger, nameof(Lecturer));

            return _mapper.Map<LecturerDTO>(Lecturer);
        }

        public async Task CreateAsync(LecturerDTO item)
        {
            var prof = _mapper.Map<Lecturer>(item);
            await _lecturerRepository.CreateAsync(prof);
        }

        public async Task UpdateAsync(LecturerDTO item)
        {
            var Lecturer = await _lecturerRepository.GetAsync(item.Id);

            var validator = new Validations();
            validator.EntityValidation(Lecturer, _logger, nameof(Lecturer));

            Lecturer.FirstName = item.FirstName;
            Lecturer.LastName = item.LastName;
            _lecturerRepository.Update(Lecturer);
        }

        public IEnumerable<LecturerDTO> Find(Func<Lecturer, bool> predicate)
        {
            var Lecturers = _lecturerRepository
                .Find(predicate)
                .ToList();
            return Lecturers
                .Select(p => _mapper.Map<LecturerDTO>(p));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var Lecturer = await _lecturerRepository.GetAsync(id);
            validator.EntityValidation(Lecturer, _logger, nameof(Lecturer));

            _lecturerRepository.Delete(Lecturer);
        }
    }
}
