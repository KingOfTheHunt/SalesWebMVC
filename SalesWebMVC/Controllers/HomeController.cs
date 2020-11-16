using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;

namespace SalesWebMVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            // A mensagem será acessada na View
            ViewData["Message"] = "Aplicação desenvolvida em ASP.NET Core no Curso de C#.";
            ViewData["Author"] = "Davi Francisco";
            ViewData["Professor"] = "Nelio Alves";

            // O método View é method builder que retorna um objeto do tipo ViewResult
            // Ele vai buscar dentro da pasta Home um arquivo .cshtml que contém o mesmo nome
            // da ação que chamou ele.
            // Aqui ele vai acessar a pasta Views, depois Home e vai retornar a página
            // About.cshtml.
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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
