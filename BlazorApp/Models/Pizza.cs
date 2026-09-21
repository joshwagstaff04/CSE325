namespace BlazorApp.Models;

public class Pizza
{
    public const int DefaultSize = 12;
    public const int MinimumSize = 9;
    public const int MaximumSize = 17;

    public int Id { get; set; }

    public int OrderId { get; set; }

    public int SpecialId { get; set; }

    public PizzaSpecial Special { get; set; }

    public int Size { get; set; } = DefaultSize;

    public List<PizzaTopping> Toppings { get; set; } = new();

    public decimal GetBasePrice()
    {
        return Special?.BasePrice ?? 0m;
    }

    public decimal GetTotalPrice()
    {
        return GetBasePrice() + Toppings.Sum(t => t.Topping?.ExtraPrice ?? 0);
    }

    public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
}
