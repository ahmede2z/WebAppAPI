using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;
using WebAppAPI.Dtos;
using WebAppAPI.Models;

namespace WebAppAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase {
        ApplicationDbContext context;

        public CategoriesController(ApplicationDbContext cont) {
            context = cont;
        }

        /// <summary>
        /// Add new Category
        /// </summary>
        /// <param name="CategoryDto"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public IActionResult AddCategory(CategoryDto? CategoryDto) {
            if (CategoryDto == null) {
                return BadRequest();
            }
            Category categ = new Category() {
                Name = CategoryDto.Name,
                Description = CategoryDto.Description
            };
            context.Categories.Add(categ);
            context.SaveChanges();
            // CreatedAtAction  (action name,object route value , object value)
            return CreatedAtAction("GetById", new {
                id = categ.Id
            }, categ);
        }

        /// <summary>
        /// modify Category
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CategoryDto"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPut]
        public IActionResult UpdateCategory(int? id, CategoryDto? CategoryDto) {
            if (id == null) {
                return BadRequest();
            }
            if (CategoryDto == null) {
                return BadRequest();
            }
            Category categ = context.Categories.Find(id);
            if (categ == null) {
                return NotFound();
            }

            categ.Name = CategoryDto.Name;
            categ.Description = CategoryDto.Description;

            context.Categories.Update(categ);
            context.SaveChanges();
            return NoContent();
        }

        ///<summary>
        ///  Delete Category
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        [Route("Remove")]
        [HttpDelete]
        public IActionResult DeleteCategory(int? id) {
            if (id == null) {
                return BadRequest();
            }
            Category categ = context.Categories.Find(id);
            if (categ == null) {
                return NotFound();
            }

            context.Categories.Remove(categ);
            context.SaveChanges();

            return NoContent();

        }


        ///<summary>
        ///  Display All Category
        ///</summary>
        ///<returns></returns>
        [Route("List")]
        [HttpGet]
        public IActionResult GetAll() {
            return Ok(context.Categories);
        }

        ///<summary>
        ///  Get product by Id
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        [Route("Details")]
        [HttpGet]
        public IActionResult GetById(int? id) {
            Category categ = context.Categories.FirstOrDefault(x => x.Id == id);
            if (categ == null) {
                return BadRequest();
            } else {
                return Ok(categ);
            }

            return Ok(categ);
        }

    }
}
