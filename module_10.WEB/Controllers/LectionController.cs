using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using module_10.BLL.DTO;
using module_10.BLL.Interfaces.ServiceInterfaces;
using module_10.DAL.Entities;
using module_10.WEB.Interfaces;
using module_10.WEB.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace module_10.WEB.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectionController : ControllerBase
    {
        private readonly IDTOService<LectionDTO, Lection> _db;
        private readonly IMapper _mapper;

        public LectionController(IDTOService<LectionDTO, Lection> LectionService, IWEB_Mapper mapper)
        {
            _db = LectionService;
            _mapper = mapper.Create();
        }

        ///<summary>
        ///GET: Lection
        ///</summary>
        [HttpGet]
        public async Task<IEnumerable<LectionDTO>> Get()
        {
            return await _db.GetAllAsync();
        }

        ///<summary>
        ///GET: Lection by id
        ///</summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<LectionDTO>> Get(int? id)
        {
            if (id == null)
                return BadRequest();

            var Lection = await _db.GetAsync(id);
            return Ok(Lection);
        }

        ///<summary>
        ///POST: Lection
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<Lection>> Post(LectionViewModel LectionViewModel)
        {
            if (LectionViewModel == null)
                return BadRequest();

            var Lection = _mapper.Map<LectionDTO>(LectionViewModel);
            await _db.CreateAsync(Lection);
            return Ok(LectionViewModel);
        }

        ///<summary>
        ///PUT: Lection
        ///</summary>
        [HttpPut]
        public async Task<ActionResult<LectionDTO>> Put(LectionViewModel LectionViewModel)
        {
            if (LectionViewModel == null)
                return BadRequest();

            if (!_db.Find(l => l.Id == LectionViewModel.Id).Any())
                return NotFound();

            var Lection = _mapper.Map<LectionDTO>(LectionViewModel);
            await _db.UpdateAsync(Lection);

            return Ok(LectionViewModel);
        }

        ///<summary>
        ///DELETE: Lection by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lection>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var Lection = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(Lection);
        }
    }
}
