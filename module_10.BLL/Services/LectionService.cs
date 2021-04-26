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
        private readonly IRepository<Lection> _LectionRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public LectionService(IRepository<Lection> repository, IBLL_Mapper mapper, ILoggerFactory factory = null)
        {
            _LectionRepository = repository;
            _logger = factory?.CreateLogger("Lection Service");
            _mapper = mapper.CreateMapper();
        }

        public async Task<IEnumerable<LectionDTO>> GetAllAsync()
        {
            var Lections = await _LectionRepository.GetAllAsync();

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

            var Lection = await _LectionRepository.GetAsync(id);

            validator.EntityValidation(Lection, _logger, nameof(Lection));

            return _mapper.Map<LectionDTO>(Lection);
        }

        public async Task CreateAsync(LectionDTO item)
        {
            var Lection = _mapper.Map<Lection>(item);
            await _LectionRepository.CreateAsync(Lection);
        }

        public async Task UpdateAsync(LectionDTO item)
        {
            var Lection = await _LectionRepository.GetAsync(item.Id);

            var validator = new Validations();
            validator.EntityValidation(Lection, _logger, nameof(Lection));

            Lection.Name = item.Name;
            Lection.LecturerId = item.LecturerId;
            _LectionRepository.Update(Lection);
        }

        public IEnumerable<LectionDTO> Find(Func<Lection, bool> predicate)
        {
            var Lections = _LectionRepository
                .Find(predicate)
                .ToList();
            return Lections
                .Select(l => _mapper.Map<LectionDTO>(l));
        }

        public async Task DeleteAsync(int? id)
        {
            var validator = new Validations();
            validator.IdValidation(id, _logger);

            var Lection = await _LectionRepository.GetAsync(id);
            validator.EntityValidation(Lection, _logger, nameof(Lection));

            _LectionRepository.Delete(Lection);
        }
    }
}
