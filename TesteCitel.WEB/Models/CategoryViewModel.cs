using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TesteCitel.WEB.Models
{
    public class CategoryViewModel
    {
        public string Id { get; set; }

        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        public IEnumerable<Domain.Arguments.Category.CategoryResponse> Categories { get; set; }
    }
}
