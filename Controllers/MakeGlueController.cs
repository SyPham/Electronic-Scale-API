using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EC_API.Helpers;
using EC_API._Services.Interface;
using EC_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace EC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MakeGlueController : ControllerBase
    {
        private readonly IGlueIngredientService _glueIngredientService;
        private readonly IMakeGlueService _makeGlueService;
        public MakeGlueController(IMakeGlueService makeGlueService, IGlueIngredientService glueIngredientService)
        {
            _makeGlueService = makeGlueService;
            _glueIngredientService = glueIngredientService;
        }

      
        [HttpGet("getGlueIngredientByGlueID/{glueid}", Name = "getGlueIngredientByGlueID")]
        public async Task<IActionResult> getGlueIngredientByGlueID(int glueid)
        {
            var obj = await _makeGlueService.GetGlueWithIngredients(glueid);
            return Ok(obj);
        }
        [HttpGet("GetAllGlues", Name = "GetAllGlues")]
        public async Task<IActionResult> GetAllGlues()
        {
            var glues = await _makeGlueService.GetAllGlues();
            return Ok(glues);
        }

        [HttpPost("{glueid}", Name = "MakeGlue")]
        public async Task<IActionResult> MakeGlue(int glueid)
        {
            var lists = await _makeGlueService.MakeGlue(glueid);
            return Ok(lists);
        }
        [HttpPost("{code}", Name = "MakeGlueByCode")]
        public async Task<IActionResult> MakeGlue(string code)
        {
            var item = await _makeGlueService.MakeGlue(code);
            return Ok(item);
        }
        [HttpGet("GetGlueWithIngredientByGlueCode/{code}")]
        public async Task<IActionResult> GetGlueWithIngredientByGlueCode(string code)
        {
            var item = await _makeGlueService.GetGlueWithIngredientByGlueCode(code);
            return Ok(item);
        }
        [HttpPut("Guidance")]
        public async Task<IActionResult> Guidance(List<GlueIngredientForGuidanceDto> update)
        {
            if (await _glueIngredientService.Guidance(update))
                return NoContent();
            return BadRequest($"Updating glue Ingredient{update.First().GlueID} failed on save");
        }
    }
}