using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        public Seller FindById(int id)
        {
            try
            {
                // Só carrega os dados da tabela Seller
                // return _context.Seller.FirstOrDefault(s => s.Id == id);
                // Agora vai realizar um join na tabela Seller e Department e retornar os dados das duas tabelas.
                // Eager loading carrega outro objetos que estão associados a um objeto principal.
                return _context.Seller.Include(dept => dept.Department).FirstOrDefault(s => s.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Remove(int id)
        {
            try
            {
                // Encontra na coleção um objeto que tem o mesmo id que foi passado como parâmetro.
                var seller = _context.Seller.Find(id);
                _context.Seller.Remove(seller);
                // Salvando as alterações
                _context.SaveChanges();
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
