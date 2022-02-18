public static class Program  
{  
 	public static void Main(string[] args)  
 	{ 
	    //add a title to our app    
		Console.WriteLine("=== Sales Viewer ===");  
		//extract the command name from the args    
		string command = args.Length > 0 ? args[0] : "unknown";    
		string file = args.Length >= 2 ? args[1] : "./data.csv";  
		 //read content of our data file    
		 //[2012-10-30] rui : actually it only works with this file, maybe it's a good idea to pass file //name as parameter to this app later?    
		string[] dataContentString = File.ReadAllLines(file);    
		//if command is print    
		if (command == "print")    
      	{    
			//get the header line    
			string line1 = dataContentString[0];    
			//get other content lines    
			string[] otherLines = dataContentString[1..(dataContentString.Length)];  
			var columnInfos = new List<(int index, int size, string name)>();  
			 //build the header of the table with column names from our data file    
			int i = 0;  
			foreach (var columName in line1.Split(','))  
			{ 
				columnInfos.Add((i++, columName.Length, columName));  
			}  
			var headerString  = String.Join(  " | ",   
					columnInfos.Select(x=>x.name).Select(  
			 		(val,ind) => val.PadLeft(16)));  
			Console.WriteLine("+" + new String('-', headerString.Length + 2) + "+");  
			Console.WriteLine("| " + headerString + " |");  
			Console.WriteLine("+" + new String('-', headerString.Length +2 ) + "+");  

			 //then add each line to the table    
			foreach (string line in otherLines)    
			{   
				//extract columns from our csv line and add all these cells to the line    
				var cells = line.Split(',');  
			 	var tableLine  = String.Join(" | ",   
						line.Split(',').Select(  
			 				(val,ind) => val.PadLeft(16)));  
			 	Console.WriteLine($"| {tableLine} |");  
			}
			
			Console.WriteLine("+" + new String('-', headerString.Length+2) + "+");  
			// if command is report  
		}   
		else if (command == "report")    
		{    
			//get all the lines without the header in the first line    
			string[] otherLines = dataContentString[1..(dataContentString.Length)];    
			//declare variables for our conters    
			int number1 = 0, number2 = 0;    
			double number4 = 0.0, number5 = 0.0, number3 = 0;    
			HashSet<string> clients = new HashSet<string>();    
			DateTime last = DateTime.MinValue;    
			//do the counts for each line    
			foreach (var line in otherLines)    
			{ 
				//get the cell values for the line    
				var cells = line.Split(',');    
				number1++;//increment the total of sales    
				//to count the number of clients, we put only distinct names in a hashset 
				//then we'll count the number of entries 
				if (!clients.Contains(cells[1])) clients.Add(cells[1]);    
				number2 += int.Parse(cells[2]);//we sum the total of items sold here    
				number3 += double.Parse(cells[3]);//we sum the amount of each sell    
				//we compare the current cell date with the stored one and pick the higher
				last = DateTime.Parse(cells[4]) > last ? DateTime.Parse(cells[4]) : last;    
			}   
			//we compute the average basket amount per sale    
			number4 = Math.Round(number3 / number1,2);    
			//we compute the average item price sold    
			number5 = Math.Round(number3 / number2,2);    
			Console.WriteLine($"+{new String('-',45)}+");  
			Console.WriteLine($"| {" Number of sales".PadLeft(30)} | {number1.ToString().PadLeft(10)} |");  
			Console.WriteLine($"| {" Number of clients".PadLeft(30)} | {clients.Count.ToString().PadLeft(10)} |");  
			Console.WriteLine($"| {" Total items sold".PadLeft(30)} | {number2.ToString().PadLeft(10)} |");  
			Console.WriteLine($"| {" Total sales amount".PadLeft(30)} | {Math.Round(number3,2).ToString().PadLeft(10)} |");  
			Console.WriteLine($"| {" Average amount/sale".PadLeft(30)} | {number4.ToString().PadLeft(10)} |");  
			Console.WriteLine($"| {" Average item price".PadLeft(30)} | {number5.ToString().PadLeft(10)} |");  
			Console.WriteLine($"+{new String('-',45)}+");  
		}
		else    
		{    
			Console.WriteLine("[ERR] your command is not valid ");    
			Console.WriteLine("Help: ");    
			Console.WriteLine("    - [print]  : show the content of our commerce records in data.csv");    
			Console.WriteLine("    - [report] : show a summary from data.csv records ");    
		}  
 }}
