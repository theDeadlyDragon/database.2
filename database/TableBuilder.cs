using database.Columns;

namespace database;

public class TableBuilder
{
    public string name { get; private set; }
    public Dictionary<string, ColumnTypes> columns { get; private set; }

    public TableBuilder(String name)
    {
        this.name = name;
        columns = new Dictionary<string, ColumnTypes>();
    }
    
    public void add(string nameColumn, ColumnTypes column)
    {
        columns.Add(nameColumn,column);
    }


}