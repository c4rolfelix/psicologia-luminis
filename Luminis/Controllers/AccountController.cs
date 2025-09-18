using Microsoft.AspNetCore.Mvc;
using Luminis.Models.ViewModels; // Seu ViewModel para registro e login
using Luminis.Data;             // Seu DbContext
using Luminis.Models;           // Sua classe Psicologo
using System;                   // Para DateTime
using System.Threading.Tasks;   // Para métodos assíncronos
using System.Security.Cryptography; // Para SHA256
using System.Text;              // Para Encoding.UTF8
using Microsoft.AspNetCore.Authentication;          // Para HttpContext.SignInAsync
using Microsoft.AspNetCore.Authentication.Cookies;  // Para CookieAuthenticationDefaults
using System.Security.Claims;   // Para ClaimsPrincipal, ClaimsIdentity, Claim
using Microsoft.EntityFrameworkCore; // Para SingleOrDefaultAsync e AnyAsync
using Microsoft.AspNetCore.Identity; // Para UserManager, SignInManager, IdentityUser
using System.Text.RegularExpressions; // NOVO: Para Regex

namespace Luminis.Controllers
{
    public class AccountController : Controller
    {
        private readonly LuminisDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(
          LuminisDbContext context,
          UserManager<IdentityUser> userManager,
          SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // --- AÇÕES DE REGISTRO ---

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

                var identityUser = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
                var createIdentityResult = await _userManager.CreateAsync(identityUser, model.Senha);

                if (!createIdentityResult.Succeeded)
                {
                    foreach (var error in createIdentityResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }

                var psicologo = new Psicologo
                {
                    Nome = model.Nome,
                    Sobrenome = model.Sobrenome,
                    CRP = model.CRP,
                    Email = model.Email,
                    Biografia = null,
                    FotoUrl = null,
                    WhatsApp = CleanWhatsAppNumber(model.WhatsApp),
                    CPF = CleanCpf(model.CPF),
                    DataNascimento = model.DataNascimento,
                    Ativo = false,
                    DataCadastro = DateTime.Now
                };

                _context.Psicologos.Add(psicologo);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Conta criada com sucesso! Faça o login.";
                return RedirectToAction("Login");
            }

            return View(model);
        }

        // --- AÇÕES DE LOGIN ---

        public IActionResult Login()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user, model.Senha, model.LembrarMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    ModelState.Clear();

                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("EditProfile", "Psicologo");
                    }
                }
                else if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Sua conta está bloqueada.");
                    return View(model);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "E-mail ou senha inválidos.");
                    return View(model);
                }
            }

            return View(model);
        }

        // --- AÇÕES DE RECUPERAÇÃO DE SENHA ---

        [HttpGet]
        public IActionResult RecuperarSenha()
        {
            return View(new RecuperarSenhaViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RecuperarSenha(RecuperarSenhaViewModel model)
        {
            // Etapa 2: Redefinir a senha, se a validação inicial já foi feita
            if (model.Validado)
            {
                if (ModelState.IsValid)
                {
                    // Lógica para redefinir a senha do IdentityUser
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    var result = await _userManager.RemovePasswordAsync(user);
                    if (result.Succeeded)
                    {
                        result = await _userManager.AddPasswordAsync(user, model.NovaSenha);
                    }

                    if (result.Succeeded)
                    {
                        ViewBag.SuccessMessage = "Sua nova senha foi salva com sucesso!";
                        return View("Login");
                    }

                    ModelState.AddModelError(string.Empty, "Ocorreu um erro ao salvar a nova senha.");
                    return View(model);
                }
                // Se a nova senha não for válida, retorna a View para a 2ª etapa
                return View(model);
            }

            // Etapa 1: Validação do CPF e e-mail
            if (ModelState.IsValid)
            {
                var psicologo = await _context.Psicologos
                                              .FirstOrDefaultAsync(p => p.Email == model.Email && p.CPF == CleanCpf(model.CPF));
                if (psicologo != null)
                {
                    model.Validado = true;
                    ModelState.Clear();
                    return View(model);
                }

                ModelState.AddModelError(string.Empty, "E-mail ou CPF incorretos.");
            }

            // Retorna a View com erros ou para a 1ª etapa
            return View(model);
        }

        // --- AÇÃO DE LOGOUT ---

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // --- MÉTODOS AUXILIARES ---

        private string CleanCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return string.Empty;
            }
            return Regex.Replace(cpf, "[^0-9]", "");
        }

        private string CleanWhatsAppNumber(string? whatsApp)
        {
            if (string.IsNullOrEmpty(whatsApp))
            {
                return string.Empty;
            }
            return Regex.Replace(whatsApp, "[^0-9]", "");
        }
    }
}