using System.ComponentModel.DataAnnotations;

namespace TesteCitel.API.Models
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }
    }
}
