using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyServerSide.Models
{
    public class ProductData
    {
        [Key]
        public int ID;
        public string ProductName;
        public string ProductCode;
        public string ReleaseDate;
        public float Price;
        public string Description;
        public float StarRating;
        public string ImageUrl;
        public List<string> Tags;
    }
}
