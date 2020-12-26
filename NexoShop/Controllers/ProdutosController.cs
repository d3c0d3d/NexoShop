using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using NexoShop.Models;

namespace NexoShop.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly NexoShopDbContext _db;        

        public ProdutosController(NexoShopDbContext context)
        {
            _db = context;
        }

        // GET: Produtos
        public ActionResult Index()
        {
            var produtos = _db.Produtos.Include(p => p.ClienteKey);
            return View(produtos.ToList());
        }

        // GET: Produtos
        [HttpPost]
        public ActionResult Index(string term)
        {
            var produtos = string.IsNullOrEmpty(term)
                ? _db.Produtos.Include(p => p.ClienteKey)
                : _db.Produtos.Where(x => x.Nome.ToLower().Contains(term.ToLower())).Include(p => p.ClienteKey); ;
            return View(produtos.ToList());
        }

        // GET: Produtos/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // GET: Produtos/Create
        public ActionResult Create()
        {
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nome");
            return View();
        }

        // POST: Produtos/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProdutoId,Nome,Valor,Disponivel,ClienteId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _db.Produtos.Add(produto);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nome", produto.ClienteId);
            return View(produto);
        }

        // GET: Produtos/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nome", produto.ClienteId);
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProdutoId,Nome,Valor,Disponivel,ClienteId")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(produto).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClienteId = new SelectList(_db.Clientes, "ClienteId", "Nome", produto.ClienteId);
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produto produto = _db.Produtos.Find(id);
            if (produto == null)
            {
                return HttpNotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Produto produto = _db.Produtos.Find(id);
            _db.Produtos.Remove(produto);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
