public static class Program  
{
	public static string printAction(string[] fileContentLines)
	{
		string output = "";
		//get the header line    
		string header = fileContentLines[0];    
		//get other content lines    
		string[] dataLines = fileContentLines[1..(fileContentLines.Length)];  
		var columnInfos = new List<(int index, int size, string name)>();  
		//build the header of the table with column names from our data file    
		int i = 0;  
		foreach (var columName in header.Split(','))  
		{ 
			columnInfos.Add((i++, columName.Length, columName));  
		}  
		var headerString  = String.Join(  " | ",   
			columnInfos.Select(x=>x.name).Select(  
				(val,ind) => val.PadLeft(16)));  
		output += "+" + new String('-', headerString.Length + 2) + "+\n";  
		output += "| " + headerString + " |\n";  
		output += "+" + new String('-', headerString.Length +2 ) + "+\n";  

		//then add each line to the table    
		foreach (string data in dataLines)    
		{   
			//extract columns from our csv line and add all these cells to the line    
			var cells = data.Split(',');  
			var tableLine  = String.Join(" | ",   
				data.Split(',').Select(  
					(val,ind) => val.PadLeft(16)));  
			output += $"| {tableLine} |\n";  
		}
			
		output += "+" + new String('-', headerString.Length+2) + "+";  
		return output;
	}

	public static string reportAction(string[] fileContentLines)
	{
		string output = "";
		//get all the lines without the header in the first line    
		string[] dataLines = fileContentLines[1..(fileContentLines.Length)];    
		//declare variables for our conters    
		int numberOfSales = 0, totalItemsSold = 0;    
		double averageAmount = 0.0, averageItemsPrice = 0.0, totalSalesAmount = 0;    
		HashSet<string> clients = new HashSet<string>();    
		DateTime last = DateTime.MinValue;    
		// Get Total of sales
		numberOfSales = getNumberOfSales(dataLines);
		// Get clients list
		clients = getClientList(dataLines);
		// Get total items sold
		totalItemsSold = getTotalItemsSold(dataLines);
		// Get sales total amount
		totalSalesAmount = getTotalSalesAmount(dataLines);
		//we compute the average basket amount per sale    
		averageAmount = getAverageAmount(totalSalesAmount, numberOfSales);    
		//we compute the average item price sold    
		averageItemsPrice = getAverageItemsPrice(totalSalesAmount, totalItemsSold);  
		output += $"+{new String('-',45)}+\n";  
		output += $"| {" Number of sales".PadLeft(30)} | {numberOfSales.ToString().PadLeft(10)} |\n";  
		output += $"| {" Number of clients".PadLeft(30)} | {clients.Count.ToString().PadLeft(10)} |\n";  
		output += $"| {" Total items sold".PadLeft(30)} | {totalItemsSold.ToString().PadLeft(10)} |\n";  
		output += $"| {" Total sales amount".PadLeft(30)} | {Math.Round(totalSalesAmount,2).ToString().PadLeft(10)} |\n";  
		output += $"| {" Average amount/sale".PadLeft(30)} | {averageAmount.ToString().PadLeft(10)} |\n";  
		output += $"| {" Average item price".PadLeft(30)} | {averageItemsPrice.ToString().PadLeft(10)} |\n";  
		output += $"+{new String('-',45)}+";

		return output;
	}

	public static int getNumberOfSales(string[] salesData)
	{
		return salesData.Length;
	}

	public static double getAverageAmount(double totalSalesAmount, int numberOfSales)
	{
		return Math.Round(totalSalesAmount / numberOfSales, 2);
	}
	
	public static double getAverageItemsPrice(double totalSalesAmount, int totalItemsSold)
	{
		return Math.Round(totalSalesAmount / totalItemsSold, 2);
	}

	public static int getTotalItemsSold(string[] orderData)
	{
		int totalItemsSold = 0;
		foreach (var order in orderData)
		{
			//get the cell values for the line    
			var cells = order.Split(',');
			totalItemsSold += int.Parse(cells[2]);//we sum the total of items sold here
		}

		return totalItemsSold;
	}
	
	public static double getTotalSalesAmount(string[] orderData)
	{
		double totalSalesAmount = 0;
		foreach (var order in orderData)
		{
			//get the cell values for the line    
			var cells = order.Split(',');
			totalSalesAmount += double.Parse(cells[3]);//we sum the amount of each sell 
		}

		return totalSalesAmount;
	}
	
	public static HashSet<string> getClientList(string[] orderData)
	{
		HashSet<string> clients = new HashSet<string>();
		foreach (var order in orderData)
		{
			//get the cell values for the line    
			var cells = order.Split(',');
			//to count the number of clients, we put only distinct names in a hashset 
			//then we'll count the number of entries 
			if (!clients.Contains(cells[1])) 
				clients.Add(cells[1]);
		}

		return clients;
	}

	public static void invalidAction()
	{
		Console.WriteLine("[ERR] your action is not valid ");    
		Console.WriteLine("Help: ");    
		Console.WriteLine("    - [print]  : show the content of our commerce records in data.csv");    
		Console.WriteLine("    - [report] : show a summary from data.csv records ");
	}
 	public static void Main(string[] args)  
 	{ 
	    //add a title to our app    
		Console.WriteLine("=== Sales Viewer ===");  
		//extract the action name from the args    
		string action = args.Length > 0 ? args[0] : "unknown";    
		string file = args.Length >= 2 ? args[1] : "./data.csv";  
		 //read content of our data file    
		 //[2012-10-30] rui : actually it only works with this file, maybe it's a good idea to pass file //name as parameter to this app later?    
		string[] fileContentLines = File.ReadAllLines(file);    
		//if action is print    
		if (action == "print")
		{
			Console.WriteLine(printAction(fileContentLines));
		}   
		// if action is report 
		else if (action == "report")
		{
			Console.WriteLine(reportAction(fileContentLines));
		}
		else
		{
			invalidAction();
		}  
 }}
