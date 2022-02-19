using System;
using System.IO;
using NFluent;
using Xunit;
namespace SalesReporter.Cli.Tests;

public class PrintActionTest
{
    
    [Fact]
    public void With_Sample_Data_On_Print_Should_Return_Users_Orders()
    {
        string[] fileContentLines = File.ReadAllLines("./data.csv");
        string output = Program.printAction(fileContentLines);
        Check.That(output).IsEqualTo(
            @$"
+----------------------------------------------------------------------------------------------+
|          orderid |         userName |    numberOfItems |    totalOfBasket |        dateOfBuy |
+----------------------------------------------------------------------------------------------+
|                1 |            peter |                3 |           123.00 |       2021-11-30 |
|                2 |             paul |                1 |           433.50 |       2021-12-11 |
|                3 |            peter |                1 |           329.99 |       2021-12-18 |
|                4 |             john |                5 |           467.35 |       2021-12-30 |
|                5 |             john |                1 |            88.00 |       2022-01-04 |
+----------------------------------------------------------------------------------------------+
"
        );
    }
}