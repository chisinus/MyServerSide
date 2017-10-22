using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyServerSide.Models;

namespace MyServerSide.Controllers
{
    [Produces("application/json")]
    //[Route("api/Product")]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly ProductContext context;

        public ProductController(ProductContext context)
        {
            this.context = context;

            if (context.Products.Count() == 0)
            {
                context.Products.AddRange(GetInitProducts());
                context.SaveChanges();
            }
        }

        private List<ProductData> GetInitProducts()
        {
            List<ProductData> list = new List<ProductData>();
            for (int i=1; i<=10; i++)
            {
                //Tag[] tags = {new Tag() { TagId = 1, TagName = "tag1" },
                //              new Tag() { TagId = 2, TagName = "tag2" } };

                list.Add(new ProductData
                {
                    Id = i,
                    ProductName = "Product name " + i,
                    ProductCode = "Product code " + i,
                    ReleaseDate = "Date " + i,
                    Price = i + i / 10,
                    Description = "description " + i,
                    StarRating = 5,
                    ImageUrl = "rul" + i
                    //,
                    //Tags = { "tag " + i + " 1", "tag " + i + " 2", "tag" + i + "1" }
                    //Tags = tags.ToList()
                });
            }

            return list;
        }

        [HttpGet]
        public List<ProductData> GetAll()
        {
            int i = context.Products.Count();
            return context.Products.ToList();
        }

        [HttpGet("{id}", Name ="GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpPost]
        public IActionResult Create([FromBody] ProductData product)
        {
            if (product == null) return BadRequest();

            product.Id = context.Products.Max(p => p.Id) + 1; 
            context.Products.Add(product);
            context.SaveChanges();

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]ProductData product)
        {
            if ((product == null) || product.Id != id) return BadRequest();

            var prod = context.Products.FirstOrDefault(p => p.Id == id);
            if (prod == null) return BadRequest();

            prod.ProductName = product.ProductName;
            prod.ProductCode = product.ProductCode;

            context.Products.Update(prod);
            context.SaveChanges();

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var prod = context.Products.FirstOrDefault(p => p.Id == id);
            if (prod == null) return BadRequest();

            context.Products.Remove(prod);
            context.SaveChanges();

            return new NoContentResult();
        }
    }
}