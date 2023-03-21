using Microsoft.AspNetCore.Mvc;
using PizzaFrontEnd.Models;
using PizzaFrontEnd.Services;
using System.Diagnostics;

namespace PizzaFrontEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPizzaService _service;
        public IEnumerable<PizzaInfo> Pizzas;

        public HomeController(ILogger<HomeController> logger, IPizzaService service)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetPizzas()
        {
            Pizzas = await _service.GetPizzasAsync();

            if (Pizzas is null)
            {
                _logger.LogWarning("Erro ao obter pizzas!");
                return View("Error");
            }

            _logger.LogInformation("Obtendo pizzas");

            return View(Pizzas);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}