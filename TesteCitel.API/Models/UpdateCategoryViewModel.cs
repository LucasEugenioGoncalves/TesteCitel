using System.ComponentModel.DataAnnotations;

namespace TesteCitel.API.Models
{
    public class UpdateCategoryViewModel
    {
        [Required(ErrorMessage = "O id é obrigatório.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }
    }
}
