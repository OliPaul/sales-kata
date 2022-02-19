using System;
using System.IO;
using NFluent;
using Xunit;

namespace SalesReporter.Cli.Tests;

public class ReportActionTest
{
    [Fact]
    public void With_Sample_Data_On_Report_Should_Return_Orders_Report()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string output = Program.reportAction(fileContentLines);
        Check.That(output).IsEqualTo(
            @$"
+---------------------------------------------+
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

    [Fact]
    public void With_Sample_Data_Number_Of_Sales_Should_Return_Sales_Data_Length()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        int numberOfSales = Program.numberOfSales(dataLines);
        Check.That(numberOfSales).IsEqualTo(dataLines.Length);
    }
}