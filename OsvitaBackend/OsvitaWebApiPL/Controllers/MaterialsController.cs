using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/materials")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialService materialService;
        public MaterialsController(IMaterialService materialService)
        {
            this.materialService = materialService;
        }

        // GET: api/<MaterialsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MaterialModel>>> Get()
        {
            var materialsModels = await materialService.GetAllAsync();
            if (materialsModels is not null)
            {
                return Ok(materialsModels);
            }
            return NotFound();
        }

        // GET api/<MaterialsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MaterialModel>> Get(int id)
        {
            var materialModel = await materialService.GetByIdAsync(id);
            if (materialModel is not null)
            {
                return Ok(materialModel);
            }
            return NotFound();
        }

        // POST api/<MaterialsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] MaterialModel model)
        {
            try
            {
                await materialService.AddAsync(model);
                return Created();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/materials/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] MaterialModel model)
        {
            try
            {
                model.Id = id;
                await materialService.UpdateAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/materials/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await materialService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
