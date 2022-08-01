using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;


namespace WebAppAPI.Dtos {
    public class CategoryDto {

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

    }
}
