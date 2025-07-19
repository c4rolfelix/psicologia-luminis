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

        // GET: Psicologo/Index (Listagem de Psicólogos com Filtro)
        public async Task<IActionResult> Index(string searchSpecialty)
        {
            // 1. Obter todas as especialidades para o filtro (dropdown/checkboxes)
            ViewBag.Especialidades = await _context.Especialidades
                                                   .OrderBy(e => e.Nome)
                                                   .ToListAsync();

            // 2. Construir a consulta de psicólogos ativos
            IQueryable<Psicologo> psicologosQuery = _context.Psicologos
                                                             .Where(p => p.Ativo == true) // Apenas psicólogos ATIVOS
                                                             .Include(p => p.Especialidades); // Inclui as especialidades relacionadas

            // 3. Aplicar filtro por especialidade, se houver
            if (!string.IsNullOrEmpty(searchSpecialty))
            {
                psicologosQuery = psicologosQuery.Where(p =>
                    p.Especialidades.Any(e => e.Nome.Contains(searchSpecialty)) // Busca por nome da especialidade
                );
            }

            // 4. Executar a consulta e obter a lista de psicólogos
            var psicologos = await psicologosQuery.ToListAsync();

            // 5. Retorna a View com a lista de psicólogos
            return View(psicologos);
        }

        // GET: Psicologo/Details/{id} (Página de Detalhes do Psicólogo)
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