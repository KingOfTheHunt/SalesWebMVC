using System;
using System.Collections.Generic;
using System.Linq;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public List<Seller> FindAll()
        {
            try
            {
                // Acessa a fonte de dados associado a tabela de sellers e converte em uma lista
                return _context.Seller.ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Insert(Seller seller)
        {
            try
            {
                // Solução provisória
                seller.Department = _context.Department.First();
                // Adicionando ao banco
                _context.Add(seller);
                // Salvando as alterações
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
