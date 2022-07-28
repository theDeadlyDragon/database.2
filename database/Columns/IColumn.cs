using System.Linq.Expressions;

namespace database.Columns;

public interface IColumn
{
   
    public Type _type { get; set; }
    public void add<T>(T input);
    public T get<T, G>(G key);
    public T get<T>(int index);

    public T get<T>(List<int> indexList);
    public List<int> contains<G>(G value);
    public List<int> equals<G>(G value);
}