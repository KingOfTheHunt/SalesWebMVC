using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using System.Collections.Generic;

namespace SalesWebMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        // Injetando a dependência
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            var sellers = _sellerService.FindAll();

            // Passando a lista para a View
            return View(sellers);
        }

        public IActionResult Create()
        {
            ICollection<Department> departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel() { Departments = departments };

            return View(viewModel);
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
