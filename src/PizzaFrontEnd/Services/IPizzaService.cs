using PizzaFrontEnd.Models;

namespace PizzaFrontEnd.Services;

public interface IPizzaService
{
    Task<IEnumerable<PizzaInfo>> GetPizzasAsync();
}