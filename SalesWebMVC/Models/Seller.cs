using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebMVC.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public double BaseSalary { get; set; }
        // Representa qualquer tipo de coleção
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();
        public Department Department { get; set; }

        public Seller()
        {
        }

        public Seller(int id, string name, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
        }

        public void AddSales(SalesRecord sale)
        {
            Sales.Add(sale);
        }

        public void RemoveSales(SalesRecord sale)
        {
            Sales.Remove(sale);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final)
                .Select(s => s.Amount)
                .Sum();
        }
    }
}
