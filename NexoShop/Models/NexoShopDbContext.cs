using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace NexoShop.Models
{
    public class NexoShopDbContext : DbContext
    {
        public NexoShopDbContext() : base("name=NexoShopDB")
        {
            // Descomentar para semear o banco de testes
            //Database.SetInitializer(new NexoShopDbInitializer());
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<Produto> Produtos { get; set; }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            return base.ValidateEntity(entityEntry, items);
        }

    }

    public class NexoShopDbInitializer : DropCreateDatabaseAlways<NexoShopDbContext>
    {
        protected override void Seed(NexoShopDbContext context)
        {
            try
            {

                IList<Cliente> ClienteList = new List<Cliente>();

                for (int i = 0; i < 4; i++)
                {
                    var id = i + 1;
                    ClienteList.Add(new Cliente(
                        $"Cliente Nome {id}",
                        $"Cliente Sobrenome {id}",
                        $"Cliente{id}@gmail.com",
                        DateTime.Now.AddYears(-5),
                        true));
                }

                context.Clientes.AddRange(ClienteList);

                IList<Produto> ProdutoList = new List<Produto>();

                for (int i = 0; i < 2000; i++)
                {
                    var id = i + 1;
                    ProdutoList.Add(new Produto($"Produto {id}", id, true, 2));
                }

                for (int i = 0; i < 5000; i++)
                {
                    var id = i + 1;
                    ProdutoList.Add(new Produto($"Produto {id}", id, true, 3));
                }

                for (int i = 0; i < 10000; i++)
                {
                    var id = i + 1;
                    ProdutoList.Add(new Produto($"Produto {id}", id, true, 4));
                }

                context.Produtos.AddRange(ProdutoList);
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" erros:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Propriedade: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
            }

            base.Seed(context);
        }
    }
}