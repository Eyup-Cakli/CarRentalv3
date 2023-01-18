using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        IColorService _colorservice;

        public ColorsController(IColorService colorservice)
        {
            _colorservice = colorservice;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _colorservice.GetAll();
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("add")]
        public IActionResult PostAdd(Color color)
        {
            var result = _colorservice.Add(color);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("delete")]
        public IActionResult Delete(Color color)
        {
            var result =_colorservice.Delete(color);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("Update")]
        public IActionResult Update(Color color)
        {
            var result = _colorservice.Update(color);
            if(result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
