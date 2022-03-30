using System;
using System.ComponentModel.DataAnnotations;

namespace TesteCitel.API.Models
{
    public class UpdateProductViewModel
    {
        [Required(ErrorMessage = "O id é obrigatório.")]
        public string Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        [Range(0.1, 99999.99,ErrorMessage = "O Preço deve estar entre 0.1 e 99999,99.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O id é obrigatório.")]
        public string CategoryId { get; set; }
    }
}
