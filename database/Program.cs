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


var watch = new System.Diagnostics.Stopwatch();

FileReader f = new FileReader();
var inputData= f.ReadFile("t.txt");

var a = new ColumnString();

watch.Start();
for(int i = 0; i < 2; i++)
{
    a.add(inputData[i]);
}
watch.Stop();
Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");


watch.Reset();

watch.Start();
var output = a.contains("is");
watch.Stop();

//Console.Out.WriteLine(output.Count);



/*foreach (var VARIABLE in output)
{
    
    Console.Out.WriteLine(a.get<string>(VARIABLE));
}*/

Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");

Console.WriteLine("done");


