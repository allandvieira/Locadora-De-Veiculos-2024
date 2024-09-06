using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApp.Controllers;

[Authorize(Roles = "Empresa,Funcionario")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}