using System.Text;
using System.Text.Json;

var storesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "stores");
var summaryPath = Path.Combine(Directory.GetCurrentDirectory(), "sales-summary.txt");

CreateSampleSalesFiles(storesDirectory);

var salesFiles = FindFiles(storesDirectory);
var totalSales = CalculateTotalSales(salesFiles);

GenerateSalesSummaryReport(salesFiles, totalSales, summaryPath);

Console.WriteLine($"Total sales: {totalSales:C}");
Console.WriteLine($"Sales summary report created at: {summaryPath}");

static IEnumerable<string> FindFiles(string folderName)
{
    return Directory.EnumerateFiles(folderName, "sales.json", SearchOption.AllDirectories);
}

static decimal CalculateTotalSales(IEnumerable<string> salesFiles)
{
    return salesFiles.Sum(CalculateSalesTotal);
}

static decimal CalculateSalesTotal(string salesFile)
{
    var salesJson = File.ReadAllText(salesFile);
    var salesData = JsonSerializer.Deserialize<SalesData>(salesJson);

    return salesData?.Total ?? 0;
}

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

static void CreateSampleSalesFiles(string storesDirectory)
{
    var sampleFiles = new Dictionary<string, decimal>
    {
        [Path.Combine(storesDirectory, "store-101", "sales.json")] = 22345.67m,
        [Path.Combine(storesDirectory, "store-102", "sales.json")] = 18901.25m,
        [Path.Combine(storesDirectory, "store-103", "sales.json")] = 30750.10m
    };

    foreach (var (filePath, total) in sampleFiles)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

        if (!File.Exists(filePath))
        {
            var salesData = new SalesData(total);
            var json = JsonSerializer.Serialize(salesData, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}

public record SalesData(decimal Total);
