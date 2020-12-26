using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AutoMapper;
using NexoShop.Models;
using NexoShop.ViewModel;

namespace NexoShop.Controllers
{
    public class ClientesController : Controller
    {
        private readonly NexoShopDbContext _db;
        private List<ClienteViewModel> _clienteViewModels;
        private Cliente _cliente;
        private readonly IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<Cliente, ClienteViewModel>().ReverseMap()).CreateMapper();
        private readonly List<string> _statusList = new List<string>()
        {
            "Todos",
            "Prata",
            "Ouro",
            "Platina",
            "Diamante"
        };

        public ClientesController(NexoShopDbContext context)
        {
            _db = context;           

            ViewBag.Status = new SelectList(_statusList);
        }

        // GET: Clientes
        public ActionResult Index(string term, string status)
        {
            var list = string.IsNullOrEmpty(term) 
                ? _db.Clientes.ToList()
                : _db.Clientes.Where(x => x.Nome.ToLower().Contains(term.ToLower())).ToList();
            
            _clienteViewModels = mapper.Map<List<Cliente>, List<ClienteViewModel>>(list);

            foreach (var cliente in _clienteViewModels)
            {
                if(cliente.Ativo && (DateTime.Now.Year - cliente.DataCadastro.Year) >= 5)
                {
                    var count = _db.Produtos.Count(p => p.ClienteId == cliente.ClienteId);
                    switch (count)
                    {
                        case 2000:
                            cliente.Status = _statusList[2];
                            break;
                        case 5000:
                            cliente.Status = _statusList[3];
                            break;
                        case 10000:
                            cliente.Status = _statusList[4];
                            break;
                        default:
                            cliente.Status = _statusList[1];
                            break;
                    }
                }
            }
            if(!string.IsNullOrEmpty(status) && status != "Todos")
            {
                _clienteViewModels = _clienteViewModels.Where(x => x.Status == status).ToList();
            }

            return View(_clienteViewModels);
        }

        // GET: Clientes/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _cliente = _db.Clientes.Find(id);
            if (_cliente == null)
            {
                return HttpNotFound();
            }
            var clientView = mapper.Map<Cliente, ClienteViewModel>(_cliente);

            return View(clientView);
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClienteId,Nome,Sobrenome,Email,DataCadastro,Ativo")] ClienteViewModel clientView)
        {
            _cliente = null;

            if (ModelState.IsValid)
            {
                _cliente = mapper.Map<ClienteViewModel, Cliente>(clientView);

                _db.Clientes.Add(_cliente);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(_cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _cliente = _db.Clientes.Find(id);
            if (_cliente == null)
            {
                return HttpNotFound();
            }

            var clientView = mapper.Map<Cliente, ClienteViewModel>(_cliente);

            return View(clientView);
        }

        // POST: Clientes/Edit/5
        // Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        // Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClienteId,Nome,Sobrenome,Email,Ativo")] ClienteViewModel clientView)
        {
            if (ModelState.IsValid)
            {
                _cliente = mapper.Map<ClienteViewModel, Cliente>(clientView);

                _db.Entry(_cliente).State = EntityState.Modified;
                _db.Entry(_cliente).Property(x => x.DataCadastro).IsModified = false;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(clientView);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _cliente = _db.Clientes.Find(id);
            if (_cliente == null)
            {
                return HttpNotFound();
            }
            var clientView = mapper.Map<Cliente, ClienteViewModel>(_cliente);

            return View(clientView);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            _cliente = _db.Clientes.Find(id);
            _db.Clientes.Remove(_cliente);
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
