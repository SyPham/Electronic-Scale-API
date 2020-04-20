using System.Threading.Tasks;
using EC_API._Repositories.Interface;
using EC_API.Data;
using EC_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EC_API.DTO;
using System.Collections.Generic;

namespace EC_API._Repositories.Repositories
{
    public class GlueIngredientRepository : ECRepository<GlueIngredient>, IGlueIngredientRepository
    {
        private readonly DataContext _context;
        public GlueIngredientRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> EditPercentage(int glueid, int ingredientid, int percentage)
        {
            if(await _context.GlueIngredient.AnyAsync(x => x.GlueID == glueid && x.IngredientID == ingredientid))
            {
                var item =await _context.GlueIngredient.FirstOrDefaultAsync(x => x.GlueID == glueid && x.IngredientID == ingredientid);
                item.Percentage = percentage;
                try
                {
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (System.Exception)
                {
                    return false;
                }

            }
            return false;
        }

        public async Task<object> GetIngredientOfGlue(int glueid)
        {
            var model2 =await (from a in _context.GlueIngredient
                        join b in _context.Glues on a.GlueID equals b.ID
                        join c in _context.Ingredients on a.IngredientID equals c.ID
                        select new GlueIngredientDto
                        {
                            ID = a.GlueID,
                            Name = b.Name,
                            Code = b.Code,
                            Ingredient = c,
                            Percentage = a.Percentage
                        }).GroupBy(x=>x.ID).ToListAsync();
            //var glues = new GlueDto();
            //var list = new List<IngredientDto>();
            //foreach (var item in model2)
            //{
            //    if(item.ID == glueid)
            //    {
            //        glues.ID = item.ID;
            //        glues.Name = item.Name;
            //        glues.Code = item.Code;

            //        list.Add(new IngredientDto {
            //            ID = item.Ingredient.ID,
            //            Name =item.Ingredient.Name,
            //            Percentage = item.Percentage,
            //            Code = item.Ingredient.Code,
            //        });
            //    }
                
            //}
            //glues.Ingredients = list;
            return model2;

        }

        public async Task<bool> Guidance(List<GlueIngredientForGuidanceDto> glueIngredientForGuidanceDto)
        {
          var update = await _context.GlueIngredient.Where(x => glueIngredientForGuidanceDto.Select(x => x.GlueID).Contains(x.GlueID)).ToListAsync();
            var listUpdate = new List<GlueIngredient>();
            foreach (var item in glueIngredientForGuidanceDto)
            {
                foreach (var item2 in update)
                {
                    if(item.GlueID == item2.GlueID && item.IngredientID == item2.IngredientID)
                    {
                        item2.Input = item.Input;
                        item2.ModelName = item.ModelName;
                        item2.ModelNo = item.ModelNo;
                    }
                }
            }
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (System.Exception ex)
            {
                await _context.SaveChangesAsync();
                return false;

            }
            throw new System.NotImplementedException();
        }


        //Login khi them repo
    }
}