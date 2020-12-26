using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NexoShop.ViewModel
{
    public class ClienteViewModel
    {
        public long ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }

        [Display(Name = "Data Cadastro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// Prata, Ouro, Platina, Diamante
        /// </summary>
        public string Status { get; set; }

        public bool Ativo { get; set; }
    }
}