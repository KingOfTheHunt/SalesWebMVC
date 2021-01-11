using SalesWebMVC.Data;
using SalesWebMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMVC.Services
{
    public class DepartmentService
    {
        private SalesWebMVCContext _context;

        public DepartmentService(SalesWebMVCContext context)
        {
            _context = context;
        }

        // Transformando a função em assíncrona.
        // Agora a aplicação não ficará travada esperando que este método realize a consulta do departamentos.
        public async Task<List<Department>> FindAllAsync()
        {
            try
            {
                // O await avisa ao compilador que a chamada é assíncrona.
                return await _context.Department.OrderBy(d => d.Name).ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
