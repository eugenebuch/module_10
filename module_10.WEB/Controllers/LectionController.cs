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

        public LectionController(IDTOService<LectionDTO, Lection> lectionService, IWebMapper mapper)
        {
            _db = lectionService;
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

            var lection = await _db.GetAsync(id);
            return Ok(lection);
        }

        ///<summary>
        ///POST: Lection
        ///</summary>
        [HttpPost]
        public async Task<ActionResult<Lection>> Post(LectionViewModel lectionViewModel)
        {
            if (lectionViewModel == null)
                return BadRequest();

            var lection = _mapper.Map<LectionDTO>(lectionViewModel);
            await _db.CreateAsync(lection);
            return Ok(lectionViewModel);
        }

        ///<summary>
        ///PUT: Lection
        ///</summary>
        [HttpPut]
        public async Task<ActionResult<LectionDTO>> Put(LectionViewModel lectionViewModel)
        {
            if (lectionViewModel == null)
                return BadRequest();

            if (!_db.Find(l => l.Id == lectionViewModel.Id).Any())
                return NotFound();

            var lection = _mapper.Map<LectionDTO>(lectionViewModel);
            await _db.UpdateAsync(lection);

            return Ok(lectionViewModel);
        }

        ///<summary>
        ///DELETE: Lection by id
        ///</summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Lection>> Delete(int? id)
        {
            if (id == null)
                return BadRequest();

            var lection = await _db.GetAsync(id);
            await _db.DeleteAsync(id);
            return Ok(lection);
        }
    }
}
