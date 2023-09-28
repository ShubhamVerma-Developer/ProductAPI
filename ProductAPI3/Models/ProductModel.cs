using System.ComponentModel.DataAnnotations;

namespace ProductAPI3.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please Enter a Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Select a Category.")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Please enter the description.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Please Enter the quantity.")]
        [Range(1, 100, ErrorMessage = "The Quantity must be in between the range of 1 to 100")]
        public int Quantity { get; set; }
        public string? Type { get; set; }
        public int TypeId { get; set; }
        public string? Image { get; set; }
        public string? CategoryName { get; set; }
    }


    // class for the Product Category 
    public class Item
    {
        public int CatId { get; set; }
        public string PName { get; set; }
    }

    // class for the Product Type
    public class TypeItem
    {
        public int Pid { get; set; }
        public string Ptype { get; set; }
    }

}

