using System;
using System.Collections.Generic;
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
            @$"+---------------------------------------------+
|                Number of sales |          5 |
|              Number of clients |          3 |
|               Total items sold |         11 |
|             Total sales amount |    1441.84 |
|            Average amount/sale |     288.37 |
|             Average item price |     131.08 |
+---------------------------------------------+"
        );
    }

    [Fact]
    public void With_Sample_Data_Number_Of_Sales_Should_Return_Sales_Data_Length()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        int numberOfSales = Program.getNumberOfSales(dataLines);
        Check.That(numberOfSales).IsEqualTo(dataLines.Length);
    }
    
    [Fact]
    public void With_Sample_Data_Average_Amount_Should_Return_Total_Sales_Amount_Divided_By_Number_Of_Sales()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        int numberOfSales = Program.getNumberOfSales(dataLines);
        double averageAmount = Program.getAverageAmount(0, numberOfSales);
        Check.That(numberOfSales).IsEqualTo(dataLines.Length);
    }

    [Fact]
    public void With_Sample_Data_Get_Client_List_Should_Return_Client_List_Without_Duplicate()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        HashSet<String> clients = Program.getClientList(dataLines);
        Check.That(clients).IsEqualTo(new HashSet<string>(){" peter", " paul", " john"});
    }

    [Fact]
    public void With_Sample_Data_Get_Total_Items_Sold_Should_Return_Total_Of_Items_Sold()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        int totalItemsSold = Program.getTotalItemsSold(dataLines);
        Check.That(totalItemsSold).IsEqualTo(11);
    }
    
    [Fact]
    public void With_Sample_Data_Get_Total_Sales_Amount_Should_Return_Total_Sales_Amount()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string[] dataLines = fileContentLines[1..(fileContentLines.Length)];
        double totalSalesAmount = Program.getTotalSalesAmount(dataLines);
        Check.That(totalSalesAmount).IsEqualTo(1441.8400000000001);
    }
}