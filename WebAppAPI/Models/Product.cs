using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace WebAppAPI.Models {
    public class Product {

        public int Id {
            get; set;
        }

        [Required(ErrorMessage = "u have 2 provide a valid Name")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 charachters")]
        [MaxLength(20, ErrorMessage = "Name mustn't exceed 20 charachters")]
        public string? Name {
            get; set;
        }

        [Required(ErrorMessage = "u have 2 provide a valid Descriotion")]
        [MinLength(2, ErrorMessage = "Description mustn't be less than 2 charachters")]
        [MaxLength(50, ErrorMessage = "Description mustn't exceed 50 charachters")]
        public string? Description {
            get; set;
        }
        [Required(ErrorMessage = "u have 2 provide a valid GuaranteeYears.")]
        [Range(0, int.MaxValue, ErrorMessage = "GuaranteeYears mustn't be less than 0")]
        public int GuaranteeYears { get; set; }

        [Required(ErrorMessage = "u have 2 provide a valid Price.")]
        [Range(0, double.MaxValue, ErrorMessage = "Price mustn't be less than 0")]
        public double? Price {
            get; set;
        }

        [ValidateNever]
        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
