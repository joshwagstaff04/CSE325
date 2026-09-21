namespace BlazorApp.Models;

public class OrderWithStatus
{
    public Order Order { get; set; }

    public string StatusText { get; set; }

    public static OrderWithStatus FromOrder(Order order)
    {
        return new OrderWithStatus
        {
            Order = order,
            StatusText = ComputeStatusText(order)
        };
    }

    private static string ComputeStatusText(Order order)
    {
        return order.DeliveryPostalCode switch
        {
            null => "Preparing",
            not null => "Out for delivery"
        };
    }
}
