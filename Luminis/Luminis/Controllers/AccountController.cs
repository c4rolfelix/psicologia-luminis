using Microsoft.AspNetCore.Mvc;
using Luminis.Models.ViewModels; 
using Luminis.Data;             
using Luminis.Models;           
using System;                   
using System.Threading.Tasks;   
using System.Security.Cryptography; 
using System.Text;             
using Microsoft.AspNetCore.Authentication;          
using Microsoft.AspNetCore.Authentication.Cookies;  
using System.Security.Claims;   
using Microsoft.EntityFrameworkCore; 

namespace Luminis.Controllers 
{
    public class AccountController : Controller
    {
        private readonly LuminisDbContext _context;

        public AccountController(LuminisDbContext context)
        {
            _context = context;
        }

        // exibe o formulário de registro
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> Register(PsicologoRegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Psicologos.AnyAsync(p => p.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Este e-mail já está em uso.");
                    return View(model);
                }
                if (await _context.Psicologos.AnyAsync(p => p.CRP == model.CRP))
                {
                    ModelState.AddModelError("CRP", "Este CRP já está cadastrado.");
                    return View(model);
                }

                string senhaHash = HashPassword(model.Senha);

                var psicologo = new Psicologo
                {
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    CRP = model.CRP,
                    Email = model.Email,
                    Biografia = null, 
                    FotoUrl = null,   
                    WhatsApp = model.WhatsApp,
                    Ativo = false, // O perfil não está ativo por padrão, precisa de liberação dos gestores
                    DataCadastro = DateTime.Now 
                };

                _context.Psicologos.Add(psicologo);
                await _context.SaveChangesAsync(); 

                TempData["SuccessMessage"] = "Conta criada com sucesso! Faça o login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Encontrar o psicólogo pelo e-mail no banco de dados
                var psicologo = await _context.Psicologos
                                             .SingleOrDefaultAsync(p => p.Email == model.Email);

                // Verificar se o psicólogo existe e se a senha fornecida está correta
                if (psicologo == null || !VerifyPasswordHash(model.Senha, psicologo.SenhaHash))
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos."); 
                    return View(model);
                }

                // Criar as claims (informações do usuário que serão armazenadas no cokie de autenticação)
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, psicologo.Id.ToString()), 
                    new Claim(ClaimTypes.Name, psicologo.Nome + " " + psicologo.Sobrenome), 
                    new Claim(ClaimTypes.Email, psicologo.Email),
                    new Claim(ClaimTypes.Role, "Psicologo"), 
                    new Claim("IsActive", psicologo.Ativo.ToString()) // Adiciona o status 'Ativo' como uma claim
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = model.LembrarMe, // Se Lembrar-me foi marcado o cookie será persistente
                    ExpiresUtc = model.LembrarMe ?
                                DateTimeOffset.UtcNow.AddDays(7) : DateTimeOffset.UtcNow.AddMinutes(30), 
                    AllowRefresh = true
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

      
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home"); 
        }
    }
}