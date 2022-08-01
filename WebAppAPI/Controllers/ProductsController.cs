using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppAPI.Data;
using WebAppAPI.Dtos;
using WebAppAPI.Models;

//HTTP Verbs :
//POST : ADD
//GET  : GET
//PUt  : UPdate
//Delete:Delete

namespace WebAppAPI.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase {
        ApplicationDbContext context;
        IWebHostEnvironment webHostEnvironment;
        public ProductsController(ApplicationDbContext cont, IWebHostEnvironment webHostEnv) {
            context = cont;
            webHostEnvironment = webHostEnv;
        }

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public IActionResult AddProduct([FromForm] ProductDto? productDto) {
            if (productDto == null) {
                return BadRequest();
            }
            string imgUrl;
            if (productDto.ImageFile != null) {
                string imgExtension = Path.GetExtension(productDto.ImageFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                imgUrl = "\\images\\" + imgGuid + imgExtension;
                string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                FileStream fs = new FileStream(imgPath, FileMode.Create);
                productDto.ImageFile.CopyTo(fs);
                fs.Dispose();
            } else {
                imgUrl = "\\images\\No_Img.jpeg";
            }

            Product prod = new Product() {
                Name = productDto.Name,
                Description = productDto.Description,
                GuaranteeYears = productDto.GuaranteeYears,
                Price = productDto.Price,
                ImageUrl = imgUrl,
                CategoryId = productDto.CategoryId,
            };
            context.Products.Add(prod);
            context.SaveChanges();

            // CreatedAtAction  (action name,object route value , object value)
            return CreatedAtAction("GetById", new {
                id = prod.Id
            }, prod);
        }

        /// <summary>
        /// modify product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="productDto"></param>
        /// <returns></returns>
        [Route("Edit")]
        [HttpPut]
        public IActionResult UpdateProduct([FromForm] ProductDto? productDto, int? id) {
            if (id == null) {
                return BadRequest();
            }
            if (productDto == null) {
                return BadRequest();
            }
            Product prod = context.Products.Find(id);
            if (prod == null) {
                return NotFound();
            }

            if (prod.ImageUrl != null) {
                if (prod.ImageUrl != "\\images\\No_img.jpeg") {
                    string oldImgPath = webHostEnvironment.WebRootPath + prod.ImageUrl;
                    if (System.IO.File.Exists(oldImgPath)) {
                        System.IO.File.Delete(oldImgPath);
                    }
                }

                string imgExtension = Path.GetExtension(productDto.ImageFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtension;
                string imgUrl = "\\images\\" + imgName;
                string imgPath = webHostEnvironment.WebRootPath + imgUrl;
                FileStream imgStream = new FileStream(imgPath, FileMode.Create);
                prod.ImageUrl = imgUrl;
                productDto.ImageFile.CopyTo(imgStream);
                imgStream.Dispose();

            }
            prod.Name = productDto.Name;
            prod.Description = productDto.Description;
            prod.GuaranteeYears = productDto.GuaranteeYears;
            prod.Price = productDto.Price;
            prod.CategoryId = productDto.CategoryId;

            context.Products.Update(prod);
            context.SaveChanges();
            return NoContent();
        }

        ///<summary>
        ///  Delete product
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        [Route("Remove")]
        [HttpDelete]
        public IActionResult DeleteProduct(int? id) {
            if (id == null) {
                return BadRequest();
            }
            Product product = context.Products.Find(id);
            if (product == null) {
                return NotFound();
            }
            string imgPath = webHostEnvironment.WebRootPath + product.ImageUrl;
            if (imgPath != "\\assests\\images\\No_img.jpeg") {
                if (System.IO.File.Exists(imgPath)) {
                    System.IO.File.Delete(imgPath);
                }
            }
            context.Products.Remove(product);
            context.SaveChanges();

            return NoContent();

        }


        ///<summary>
        ///  Display All product
        ///</summary>
        ///<returns></returns>
        [Route("List")]
        [HttpGet]
        public IActionResult GetAll() {
            return Ok(context.Products);
        }

        ///<summary>
        ///  Get product by Id
        ///</summary>
        ///<param name="id"></param>
        ///<returns></returns>
        [Route("Details")]
        [HttpGet]
        public IActionResult GetById(int? id) {
            Product product = context.Products.FirstOrDefault(x => x.Id == id);
            if (product == null) {
                return BadRequest();
            } else {
                return Ok(product);
            }

            return Ok(product);
        }

    }
}
