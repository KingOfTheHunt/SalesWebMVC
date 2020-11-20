using System.Collections.Generic;

namespace SalesWebMVC.Models.ViewModels
{
    // Vai representar um modelo de cadastro de Seller
    // É composta por um Seller e uma lista de Departamentos
    public class SellerFormViewModel
    {
        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
