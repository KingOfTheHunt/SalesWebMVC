using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;
using SalesWebMVC.Services.Exceptions;

namespace SalesWebMVC.Services
{
    public class SellerService
    {
        private readonly SalesWebMVCContext _context;

        public SellerService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<Seller>> FindAllAsync()
        {
            try
            {
                // Acessa a fonte de dados associado a tabela de sellers e converte em uma lista
                return await _context.Seller.ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<Seller> FindByIdAsync(int id)
        {
            try
            {
                // Só carrega os dados da tabela Seller
                // return _context.Seller.FirstOrDefault(s => s.Id == id);
                // Agora vai realizar um join na tabela Seller e Department e retornar os dados das duas tabelas.
                // Eager loading carrega outro objetos que estão associados a um objeto principal.
                return await _context.Seller.Include(dept => dept.Department).FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task RemoveAsync(int id)
        {
            try
            {
                // Encontra na coleção um objeto que tem o mesmo id que foi passado como parâmetro.
                var seller = _context.Seller.Find(id);
                _context.Seller.Remove(seller);
                // Salvando as alterações
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task InsertAsync(Seller seller)
        {
            try
            {
                // Adicionando ao banco
                _context.Add(seller);
                // Salvando as alterações
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task UpdateAsync(Seller seller)
        {
            try
            {
                bool hasAny = await _context.Seller.AnyAsync(x => x.Id == seller.Id);

                if (!hasAny)
                {
                    throw new NotFoundException("Id não encontrado");
                }

                // Atualizando um registro no banco.
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new DbConcurrentException(ex.Message);
            }
        }
    }
}
