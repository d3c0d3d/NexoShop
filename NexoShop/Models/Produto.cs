using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NexoShop.Models
{
    [Table("Produto")]
    public class Produto
    {
        [Key]        
        public long ProdutoId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Range(1, 99999.99, ErrorMessage = "O Valor deve estar entre 1,00 e 99999,99.")]
        public decimal Valor { get; set; }

        [Required]
        public bool Disponivel { get; set; }

        [Required,ForeignKey("ClienteKey")]
        [Display(Name = "Cliente")]
        public long ClienteId { get; set; }

        public virtual Cliente ClienteKey { get; set; }

        public Produto()
        {

        }
        public Produto(string nome, decimal valor, bool disponivel, long clienteId)
        {
            Nome = nome ?? throw new ArgumentNullException(nameof(nome));
            Valor = valor;
            Disponivel = disponivel;
            ClienteId = clienteId;
        }

    }
}