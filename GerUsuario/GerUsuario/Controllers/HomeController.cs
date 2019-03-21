using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GerUsuario.Models;
using Microsoft.AspNetCore.Identity;

namespace GerUsuario.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<Usuario> _gerenciadorUsuarios;
        private readonly SignInManager<Usuario> _gerenciadorLogin;
        private readonly RoleManager<NivelAcesso> _roleManager;

        public HomeController(UserManager<Usuario> gerenciadorUsuarios, SignInManager<Usuario> gerenciadorLogin, RoleManager<NivelAcesso> roleManager)
        {
            _gerenciadorUsuarios = gerenciadorUsuarios;
            _gerenciadorLogin = gerenciadorLogin;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(RegistroViewModel registro)
        {

            if(ModelState.IsValid)
            {
                var usuario = new Usuario
                {
                    UserName = registro.NomeUsuario,
                    Nome = registro.Nome,
                    SobreNome = registro.SobreNome,
                    Idade = registro.Idade,
                    Email = registro.Email
                };

                var usuarioCriado = await _gerenciadorUsuarios.CreateAsync(usuario, registro.Senha);

                if(usuarioCriado.Succeeded)
                {
                    await _gerenciadorLogin.SignInAsync(usuario, false);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(registro);
        }

        public async Task<IActionResult> LogOut()
        {
            await _gerenciadorLogin.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult NovoNivelAcesso()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> NovoNivelAcesso(NivelAcesso nivelAcesso)
        {
            if(ModelState.IsValid)
            {
                bool roleExiste = await _roleManager.RoleExistsAsync(nivelAcesso.Name);

                if(!roleExiste)
                {
                    await _roleManager.CreateAsync(nivelAcesso);

                    return RedirectToAction("Index", "Home");
                }
            }
            return View(nivelAcesso);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
