using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model.Models;
using Negocio.Business;

namespace AvonaleHub.Controllers
{
    public class RepositorioController : Controller
    {
        public GerenciadoraRepositorio gerenciadora = new GerenciadoraRepositorio();

        public IActionResult Index()
        {
            List<Repositorio> repositorios = gerenciadora.ObterMeusRepositorios();
            return View(repositorios);
        }

        public IActionResult Detalhes(int id)
        {
            Repositorio repositorio = gerenciadora.ObterRepositorio(id);
            return View(repositorio);
        }

        public IActionResult AdicionarFavoritos(int id)
        {
            Repositorio repositorio = gerenciadora.ObterRepositorio(id);
            gerenciadora.AdicionarFavorito(repositorio);
            return RedirectToAction("Favoritos");
        }

        public IActionResult Favoritos()
        {
            List<Repositorio> repositorios = gerenciadora.ObterFavoritos();
            return View(repositorios);
        }

        public IActionResult Pesquisar()
        {
            return View();
        }

        public IActionResult BuscarAjax(string chave)
        {
            List<Repositorio> repositorios = gerenciadora.PesquisarRepositorios(chave);
            return PartialView(repositorios);
        }
    }
}