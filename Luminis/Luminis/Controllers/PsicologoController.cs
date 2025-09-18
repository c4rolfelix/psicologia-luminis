using Microsoft.AspNetCore.Mvc;
using Luminis.Data; // Seu DbContext
using Luminis.Models; // Seu modelo Psicologo e Especialidade
using System.Linq; // Para LINQ (Where, ToListAsync, Select)
using System.Threading.Tasks; // Para async/await
using Microsoft.EntityFrameworkCore; // Para ToListAsync e include

namespace Luminis.Controllers // Verifique seu namespace! Deve ser Luminis.Controllers
{
    public class PsicologoController : Controller
    {
        private readonly LuminisDbContext _context;

        public PsicologoController(LuminisDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string searchSpecialty)
        {
            ViewBag.Especialidades = await _context.Especialidades
                                                   .OrderBy(e => e.Nome)
                                                   .ToListAsync();

            IQueryable<Psicologo> psicologosQuery = _context.Psicologos
                                                             .Where(p => p.Ativo == true) // Apenas psicólogos ATIVOS
                                                             .Include(p => p.Especialidades); // Inclui as especialidades relacionadas

            if (!string.IsNullOrEmpty(searchSpecialty))
            {
                psicologosQuery = psicologosQuery.Where(p =>
                    p.Especialidades.Any(e => e.Nome.Contains(searchSpecialty)) // Busca por nome da especialidade
                );
            }

            var psicologos = await psicologosQuery.ToListAsync();

            return View(psicologos);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var psicologo = await _context.Psicologos
                                          .Include(p => p.Especialidades) // Inclui as especialidades
                                          .FirstOrDefaultAsync(m => m.Id == id && m.Ativo == true); // Busca apenas se ativo

            if (psicologo == null)
            {
                return NotFound();
            }

            return View(psicologo);
        }
    }
}