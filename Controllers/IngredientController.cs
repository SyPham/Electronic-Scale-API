using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EC_API.Helpers;
using EC_API._Services.Interface;
using EC_API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EC_API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IngredientController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;
        public IngredientController(IIngredientService brandService)
        {
            _ingredientService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetIngredients([FromQuery]PaginationParams param)
        {
            var ingredients = await _ingredientService.GetWithPaginations(param);
            Response.AddPagination(ingredients.CurrentPage, ingredients.PageSize, ingredients.TotalCount, ingredients.TotalPages);
            return Ok(ingredients);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _ingredientService.GetAllAsync();
            return Ok(ingredients);
        }

        [HttpGet("{text}")]
        public async Task<IActionResult> Search([FromQuery]PaginationParams param, string text)
        {
            var lists = await _ingredientService.Search(param, text);
            Response.AddPagination(lists.CurrentPage, lists.PageSize, lists.TotalCount, lists.TotalPages);
            return Ok(lists);
        }
        //[HttpGet("{ingredientid}", Name = "GetIngredientOfIngredient")]
        //public async Task<IActionResult> Ingredient(int ingredientid)
        //{
        //    var lists = await _ingredientService.GetIngredientOfIngredient(ingredientid);
        //    return Ok(lists);
        //}
        [HttpPost]
        public async Task<IActionResult> Create(IngredientDto ingredientIngredientDto)
        {
            if (await _ingredientService.CheckExists(ingredientIngredientDto.ID))
                return BadRequest("Ingredient ID already exists!");
            if (await _ingredientService.CheckBarCodeExists(ingredientIngredientDto.Code))
                return BadRequest("Ingredient Barcode already exists!");
            ////var username = User.FindFirst(ClaimTypes.Name).Value;
            // //ingredientIngredientDto.Updated_By = username;
            ingredientIngredientDto.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss tt");
            if (await _ingredientService.Add(ingredientIngredientDto))
            {
                return NoContent();
            }

            throw new Exception("Creating the brand failed on save");
        }

        [HttpPut]
        public async Task<IActionResult> Update(IngredientDto ingredientIngredientDto)
        {

            ingredientIngredientDto.CreatedDate = DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss tt");
            if (await _ingredientService.Update(ingredientIngredientDto))
                return NoContent();

            return BadRequest($"Updating brand {ingredientIngredientDto.ID} failed on save");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _ingredientService.Delete(id))
                return NoContent();
            throw new Exception("Error deleting the brand");
        }
    }
}