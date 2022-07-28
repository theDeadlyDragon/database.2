using System.Diagnostics;
using database.Columns;

namespace database;

public class Table
{
    public Dictionary<string, IColumn> columns = new Dictionary<string, IColumn>();
    public string name { get; private set; }
    
    public Table(TableBuilder tableBuilder)
    {
        foreach (var column in tableBuilder.columns)
        {
            switch(column.Value)
            {
               case ColumnTypes.String:
                   columns.Add(column.Key,new ColumnString());
                   break;
                   
            }
            Console.WriteLine(column.ToString());
            
        }
        
    }
    
}