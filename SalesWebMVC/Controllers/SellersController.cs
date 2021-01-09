using Microsoft.AspNetCore.Mvc;
using SalesWebMVC.Models;
using SalesWebMVC.Models.ViewModels;
using SalesWebMVC.Services;
using SalesWebMVC.Services.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;

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
            // Verifica se o modelo foi válidado.
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel() { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellerService.Insert(seller);

            // redirecionando para o index
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            // Precisa usar a propriedade Value porque o id é nullable.
            var seller = _sellerService.FindById(id.Value);
            
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado." });
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

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado." });
            }

            return View(seller);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            var seller = _sellerService.FindById(id.Value);

            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Vendedor não encontrado." });
            }

            List<Department> departments = _departmentService.FindAll();

            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = _departmentService.FindAll();
                return View(new SellerFormViewModel() { Seller = seller, Departments = departments });
            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id não encontrado." });
            }

            try
            {
                _sellerService.Update(seller);
            }
            catch (NotFoundException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }
            catch (DbConcurrentException ex)
            {
                return RedirectToAction(nameof(Error), new { message = ex.Message });
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel() { Message = message, 
                // Acessa o Id interno da requisição.
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(viewModel);
        }
    }
}
