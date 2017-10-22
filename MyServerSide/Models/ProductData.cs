using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyServerSide.Models
{
    public class ProductData
    {
        [Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ReleaseDate { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public float StarRating { get; set; }
        public string ImageUrl { get; set; }
        //public List<Tag> Tags { get; set; }
    }
}
