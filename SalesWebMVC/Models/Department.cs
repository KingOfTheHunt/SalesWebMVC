using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();

        public Department()
        {
        }

        public Department(int id, string name, ICollection<Seller> sellers)
        {
            Id = id;
            Name = name;
            Sellers = sellers;
        }

        public void AddSeller(Seller seller)
        {
            Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            // Retorna o total das vendas de um determinado departamento
            return Sellers.Sum(s => s.TotalSales(initial, final));
        } 
    }
}
