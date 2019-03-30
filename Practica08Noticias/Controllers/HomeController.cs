using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practica08Noticias.Data;
using Practica08Noticias.Models;

namespace Practica08Noticias.Controllers
{
    public class HomeController : Controller
    {
        // Lineas agregadas para poder consultar las noticias desde el Home
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Recuperar las ultimas 10 noticias 
            var noticias = (from n in _context.Noticias.Include("Categorias")
                            orderby n.FechaPublicacion descending
                            select n).Take(10);
            var noticias2 = _context.Noticias
                .OrderByDescending(n => n.FechaPublicacion)
                .Take(10);

            // pasamos la lista de noticias a la vista para mostrarlas
            return View(noticias.ToList());
        }

        public IActionResult Noticia(int? id)
        {
            if (id == null)
                return NotFound();

            var noticia = _context.Noticias
                .FirstOrDefault(m => m.NoticiaID == id);
            if (noticia == null)
                return NotFound();
            return View(noticia);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
