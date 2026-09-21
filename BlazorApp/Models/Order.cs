namespace BlazorApp.Models;

public class Order
{
    public int OrderId { get; set; }

    public List<Pizza> Pizzas { get; set; } = new();

    public string DeliveryAddress { get; set; }

    public string DeliveryPostalCode { get; set; }

    public DateTime CreatedTime { get; set; }

    public decimal GetTotalPrice()
    {
        return Pizzas.Sum(p => p.GetTotalPrice());
    }

    public string GetFormattedTotalPrice() => GetTotalPrice().ToString("0.00");
}
