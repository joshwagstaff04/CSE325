# W01 Assignment Notes

## Pizza API Evidence

The `PizzaApi` project implements the ASP.NET Core controller CRUD example from the module. The starting list is in `PizzaApi/Services/PizzaService.cs` and includes the module-style records plus one additional record:

```csharp
private static readonly List<Pizza> Pizzas =
[
    new() { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
    new() { Id = 2, Name = "Veggie", IsGlutenFree = true },
    new() { Id = 3, Name = "Buffalo Chicken", IsGlutenFree = false }
];
```

The additional record I added is:

```csharp
new() { Id = 3, Name = "Buffalo Chicken", IsGlutenFree = false }
```

Example API requests and expected successful responses:

| Operation | Request | Example body | Status code |
| --- | --- | --- | --- |
| GET | `GET http://localhost:5095/pizza` | Returns the pizza list including `Buffalo Chicken` | `200 OK` |
| POST | `POST http://localhost:5095/pizza` | `{ "name": "Hawaiian", "isGlutenFree": false }` | `201 Created` |
| PUT | `PUT http://localhost:5095/pizza/4` | `{ "id": 4, "name": "Hawaiian Deluxe", "isGlutenFree": false }` | `204 No Content` |
| DELETE | `DELETE http://localhost:5095/pizza/4` | No response body | `204 No Content` |

These requests are also saved in `PizzaApi/PizzaApi.http`.

## Sales Summary Function

The `SalesReporter` project includes the required extra function that generates a sales summary report file.

```csharp
static void GenerateSalesSummaryReport(IEnumerable<string> salesFiles, decimal totalSales, string reportPath)
{
    var report = new StringBuilder();

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    foreach (var salesFile in salesFiles.OrderBy(file => file))
    {
        var fileTotal = CalculateSalesTotal(salesFile);
        var displayName = Path.GetRelativePath(Directory.GetCurrentDirectory(), salesFile);
        report.AppendLine($"{displayName}: {fileTotal:C}");
    }

    File.WriteAllText(reportPath, report.ToString());
}
```

The generated report is written to `sales-summary.txt` when the console app runs.
