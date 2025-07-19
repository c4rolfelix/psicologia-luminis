using Microsoft.AspNetCore.Mvc;
using Luminis.Data; 
using Luminis.Models; 
using System.Linq; 
using System.Threading.Tasks; 
using Microsoft.EntityFrameworkCore; 
using Microsoft.AspNetCore.Authorization; 

namespace Luminis.Controllers 
{
    public class AdminController : Controller
    {
        private readonly LuminisDbContext _context;

        public AdminController(LuminisDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var psicologos = await _context.Psicologos
                                          .Include(p => p.Especialidades) 
                                          .OrderByDescending(p => p.DataCadastro) 
                                          .ToListAsync();
            return View(psicologos);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleActive(int id)
        {
            var psicologo = await _context.Psicologos.FindAsync(id);

            if (psicologo == null)
            {
                return NotFound();
            }

            psicologo.Ativo = !psicologo.Ativo;

            _context.Update(psicologo);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Perfil de {psicologo.Nome} {(psicologo.Ativo ? "ativado" : "desativado")} com sucesso!";
            return RedirectToAction(nameof(Index)); 
        }

    }
}