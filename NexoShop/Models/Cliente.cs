using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexoShop.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public long ClienteId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Sobrenome { get; set; }
      
        public string Email { get; set; }
        
        public DateTime DataCadastro { get; set; }

        [Required]
        public bool Ativo { get; set; }

        public Cliente(string nome, string sobrenome, string email, DateTime dataCadastro, bool ativo)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Sobrenome = sobrenome ?? throw new ArgumentNullException(nameof(sobrenome));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            DataCadastro = dataCadastro;
            Ativo = ativo;
        }

        public Cliente()
        {

        }
    }
}