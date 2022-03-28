﻿using System.ComponentModel.DataAnnotations;

namespace TesteCitel.WEB.Models
{
    public class CreateProductViewModel
    {
        [MaxLength(50, ErrorMessage = "O nome deve ter no máximo 50 caracteres.")]
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string Name { get; set; }

        public string Price { get; set; }
    }
}
