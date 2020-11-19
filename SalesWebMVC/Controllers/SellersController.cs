using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Services;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        // Injetando a dependência
        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();

            // Passando a lista para a View
            return View(sellers);
        }

        public IActionResult Create()
        {
            return View();
        }

        // Informando que o controller vai utilizar o método POST para realizar o envio de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            _sellerService.Insert(seller);

            // redirecionando para o index
            return RedirectToAction(nameof(Index));
        }
    }
}
