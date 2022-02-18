using System;
using System.IO;
using NFluent;
using Xunit;
namespace SalesReporter.Cli.Tests;

public class ReportActionTest
{
    private string[] fileContentLines = File.ReadAllLines("./data.csv"); 
    
    [Fact]
    public void With_Sample_Data_On_Report_Should_Return_Sales_Report()
    {
        using var writer = new StringWriter();
        Console.SetOut(writer);
        Console.SetError(writer);
        Program.ReportAction(fileContentLines);
        var sut = writer.ToString();
        Check.That(sut).IsEqualTo(
            @$"+---------------------------------------------+
|                Number of sales |          5 |
|              Number of clients |          3 |
|               Total items sold |         11 |
|             Total sales amount |    1441.84 |
|            Average amount/sale |     288.37 |
|             Average item price |     131.08 |
+---------------------------------------------+
"
        );
    }
    
}