using System.Net.Sockets;
using database.Columns;

namespace database;

public class Database
{
    public string databaseName {  get;private set; }
    private Dictionary<string, Table> _tables = new Dictionary<string, Table>();
    public Database(string name)
    {
        databaseName = name;
        
    }

    public void addTable(TableBuilder tableBuilder)
    {
           _tables.Add("bob",new Table(tableBuilder));
    }
    

}