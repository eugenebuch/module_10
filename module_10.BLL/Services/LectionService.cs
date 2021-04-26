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
    public class LectionService : IDTOService<LectionDTO, Lection>
    {
        private readonly IRepository<Lection> _lectionRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LectionService(IRepository<Lection> repository, IBllMapper mapper, ILoggerFactory factory = null)
        {
            _lectionRepository = repository;
            _logger = factory?.CreateLogger("Lection Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<LectionDTO>> GetAllAsync()
        {
            var Lections = await _lectionRepository.GetAllAsync();

            if (!Lections.Any())
            {
                var mes = "There are no Lections in database";
                _logger?.LogWarning(mes);
                throw new ValidationException(mes);
            }

            return Lections
                .Select(l => _mapper.Map<LectionDTO>(l));
        }

        public async Task<LectionDTO> GetAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var Lection = await _lectionRepository.GetAsync(id);

            validator.EntityValidation(Lection, _logger, nameof(Lection));

            return _mapper.Map<LectionDTO>(Lection);
        }

        public async Task CreateAsync(LectionDTO item)
        {
            var Lection = _mapper.Map<Lection>(item);
            await _lectionRepository.CreateAsync(Lection);
        }

        public async Task UpdateAsync(LectionDTO item)
        {
            var Lection = await _lectionRepository.GetAsync(item.Id);

            var validator = new Validations();
            validator.EntityValidation(Lection, _logger, nameof(Lection));

            Lection.Name = item.Name;
            Lection.LecturerId = item.LecturerId;
            _lectionRepository.Update(Lection);
        }

        public IEnumerable<LectionDTO> Find(Func<Lection, bool> predicate)
        {
            var Lections = _lectionRepository
                .Find(predicate)
                .ToList();
            return Lections
                .Select(l => _mapper.Map<LectionDTO>(l));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var Lection = await _lectionRepository.GetAsync(id);
            validator.EntityValidation(Lection, _logger, nameof(Lection));

            _lectionRepository.Delete(Lection);
        }
    }
}
