using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.AccessControl;

namespace database.Columns;

public class ColumnString : IColumn
{
    private List<string> stringObjects = new List<string>();
    private Dictionary<int, List<int>> indexData = new Dictionary<int, List<int>>();
    private Dictionary<string, List<int>> stringIndex = new Dictionary<string, List<int>>();
    private int lastEntry = 0;
    public Type _type { get;set; } 

    public ColumnString()
    {
        _type = typeof(string);
    }


    public void add<T>(T input)
    {
        checkType(typeof(T),_type);
        
        if(typeof(T) != _type)
            Console.WriteLine("done");
        
        string inputString = Convert.ToString(input);
        string[] parsed = (inputString).Split(" ");
        List<int> keys = new List<int>();
        
        
        foreach (var temp in parsed)
        {
            keys.Add(addStringData(temp));
        }
        
        indexData.Add(lastEntry++,keys);
        
        Console.WriteLine("sdf");

    }

    public T get<T, G>(G key)
    {
        checkType(typeof(G),_type);
        checkType(typeof(T),_type);
        string output = "";

        
        
        return (T) Convert.ChangeType(output,typeof(T));
    }


    public T get<T>(List<int> indexList)
    {
        checkType(typeof(T),typeof(List<String>));
        List<String> output = new List<string>();

        foreach (var place in indexList)
        {
            output.Add(get<string>(place)); 
        }

        return (T) Convert.ChangeType(output,typeof(T));
    }

    public List<int> contains<G>(G value)
    {
        checkType(typeof(G),_type);
        
        string[] input = Convert.ToString(value).Split(" ");
        List<int> output = new List<int>();

        List<int> start = new List<int>(indexData.Keys.ToArray());

        for (int i = 0; i < input.Length; i++)
        {
            List<int> loopOutput = new List<int>(); 
            if (!stringIndex.ContainsKey(input[i])) 
                return output;
            List<int> temp = stringIndex[input[i]];

            foreach (int indexPlace in temp)
            {
                if(start.Contains(indexPlace))
                    loopOutput.Add(indexPlace);
                
            }
            start = loopOutput;
        }
        
        List<int> charSequence = new List<int>();
        foreach (var key in input)
        {
            charSequence.Add(stringObjects.IndexOf(key));
        }

        foreach (var temp in start)
        {
            
        }

        return output;
    }

    public List<int> equals<G>(G value)
    {
        checkType(typeof(G),_type);
        string valueString = Convert.ToString(value);
        string[] input = Convert.ToString(value).Split(" ");
        List<int> output = new List<int>();
        List<int> start = new List<int>(indexData.Keys.ToArray());

        for (int i = 0; i < input.Length; i++)
        {
            List<int> loopOutput = new List<int>(); 
            if (!stringIndex.ContainsKey(input[i])) 
                return output;
            List<int> temp = stringIndex[input[i]];

            foreach (int indexPlace in temp)
            {
                if(start.Contains(indexPlace))
                    loopOutput.Add(indexPlace);
                
            }
            start = loopOutput;
        }

        foreach (var index in start)
        {
            if (indexData[index].Count == input.Length)
                output.Add(index);
        }

        start = output;
        output = new List<int>();

        foreach (var index in start)
        {
            if (valueString == get<string>(index)) 
                start.Add(index);
        }
        

        
        return output;
    }

    public T get<T>(int index)
    {
        checkType(typeof(T),_type);
        string output = "";
        updateStringIndex();
        
        if(!indexData.ContainsKey(index))
            return (T) Convert.ChangeType(output,typeof(T));
        List<int> keys = indexData[index];

        foreach (var key in keys)
        {
            output += $"{stringObjects[key]} ";
        }
        output = output.Remove(output.Length - 1);
        
        
        return (T) Convert.ChangeType(output,typeof(T));
    }

    private void updateStringIndex()
    {
        foreach (var keys in indexData)
        {
            foreach (var value in keys.Value)
            {
                string key = stringObjects[value];
                if (!stringIndex.ContainsKey(key)) 
                    stringIndex.Add(stringObjects[value],new List<int>());
                stringIndex[key].Add(keys.Key);
            }
            
        }
    }

    private int addStringData(string value)
    {
        if (stringObjects.Contains(value))
            return stringObjects.IndexOf(value);
        
        stringObjects.Add(value);
        return stringObjects.Count - 1;
    }
    
    private void checkType(Type a, Type b)
    {
        if(a != b) 
            throw new NotSupportedException($"got: {a} but expected: {b}");
    }
}