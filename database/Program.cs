// See https://aka.ms/new-console-template for more information

using System.Net.Sockets;
using database;
using database.Columns;




// TableBuilder temp = new TableBuilder("hell");
// temp.add("name",ColumnTypes.String);
// temp.add("age",ColumnTypes.Int);
// temp.add("gender",ColumnTypes.String);

// Database database  = new Database("data");
// database.addTable(temp);



var a = new ColumnString();
a.add("this is amazing");
a.add("this is garbage");
a.add("garbage this is");
a.add("get is this");


a.get<string>(0);

var output = a.contains("this is garbage");

//Console.Out.WriteLine(a.equals("this is garbage"));



foreach (var VARIABLE in output)
{
    
    Console.Out.WriteLine(a.get<string>(VARIABLE));
}

Console.WriteLine();