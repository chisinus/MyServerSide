using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyServerSide.Models;

namespace MyServerSide.Controllers
{
    [Produces("application/json")]
    [Route("api/Product")]
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
                list.Add(new ProductData
                {
                    ID = i,
                    ProductName = "Product name " + i,
                    ProductCode = "Product code " + i,
                    ReleaseDate = "Date " + i,
                    Price = i + i / 10,
                    Description = "description " + i,
                    StarRating = 5,
                    ImageUrl = "rul" + i,
                    Tags = { "tag " + i + " 1", "tag " + i + " 2", "tag" + i + "1" }
                });
            }

            return list;
        }

        [HttpGet]
        public IEnumerable<ProductData> GetAll()
        {
            return context.Products;
        }

        [HttpGet("{id}", Name ="GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = context.Products.FirstOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }
    }
}