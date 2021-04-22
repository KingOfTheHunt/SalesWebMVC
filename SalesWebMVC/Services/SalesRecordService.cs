using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SalesWebMVC.Data;
using SalesWebMVC.Models;

namespace SalesWebMVC.Services
{
    public class SalesRecordService
    {
        private readonly SalesWebMVCContext _context;

        public SalesRecordService(SalesWebMVCContext context)
        {
            _context = context;
        }

        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            // Cria um objeto IQueryable que contém todos os dados da tabela.
            var result = from obj in _context.SalesRecord select obj;
           
            if (minDate.HasValue)
            {
                result = result.Where(s => s.Date >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(s => s.Date <= maxDate.Value);
            }

            // Realizando uma junção entre as tabelas.
            result = result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(s => s.Date);

            return await result.ToListAsync();
        }

        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.SalesRecord select obj;

            if (minDate.HasValue)
            {
                result = result.Where(s => s.Date >= minDate);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(s => s.Date <= maxDate);
            }

            return await result
                .Include(s => s.Seller)
                .Include(d => d.Seller.Department)
                .OrderByDescending(sr => sr.Date)
                .GroupBy(d => d.Seller.Department)
                .ToListAsync();
        }
    }
}
