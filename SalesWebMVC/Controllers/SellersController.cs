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

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Precisa usar a propriedade Value porque o id é nullable.
            var seller = _sellerService.FindById(id.Value);
            
            if (seller == null)
            {
                return NotFound();
            }

            return View(seller);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);

            // Redirecionando para o Index
            return RedirectToAction(nameof(Index));
        }
    }
}
