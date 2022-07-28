namespace database.Columns;

public class Column<T> : IColumn
{
    public Type _type { get; set; }

    public void add<T>(T input)
    {
        
    }

    public T get<T, G>(G key)
    {
        if (typeof(G) != _type)
            throw new NotSupportedException();

        throw new NotImplementedException();
    }

    public T get<T>(int index)
    {
        throw new NotImplementedException();
    }

    public T1 get<T1>(List<int> indexList)
    {
        throw new NotImplementedException();
    }

    public List<int> contains<G>(G value)
    {
        throw new NotImplementedException();
    }

    public List<int> equals<G>(G value)
    {
        throw new NotImplementedException();
    }
}