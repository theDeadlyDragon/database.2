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

        var maxWordLength = Settings.Instance().maxWordLength;
        var minWordLength = Settings.Instance().minWordLength;
        
        string inputString = Convert.ToString(input);
        string[] parsed = (inputString).Split(" ");
        List<int> keys = new List<int>();
        
        
        foreach (var temp in parsed)
        {
            List<int> instruction = new List<int>();
           int numberMaxWord = temp.Length / maxWordLength;
           int restWord = temp.Length % maxWordLength;

           foreach (var i in Enumerable.Range(0, numberMaxWord))
               instruction.Add(numberMaxWord);
           
           if(restWord != 0)
               instruction.Add(restWord);

           int start = 0;
           foreach (var wordLenght in instruction)
           {
               var a = temp.Substring(start, start + wordLenght);
               start += wordLenght;

               Console.Out.WriteLine(a);
           }
          
           
           //Console.Out.WriteLine($"number of rest : {restWord} ms");
           //Console.Out.WriteLine($"number of large : {numberMaxWord} ms");

           Console.WriteLine(String.Join(", ", instruction));
           
           if(parsed[parsed.Length-1] != temp)
               
           
            keys.Add(addStringData(temp));
        }
        
        indexData.Add(lastEntry++,keys);
        


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
        var watch = new System.Diagnostics.Stopwatch();
        watch.Start();
        checkType(typeof(G),_type);
        
        updateStringIndex();
        
        string[] input = Convert.ToString(value).Split(" ");
        List<int> output = new List<int>();                                         
        List<int> wordSequence = new List<int>();                                   //store char sequence of the input value 
        List<int> data = new List<int>(indexData.Keys.ToArray());           //stores the index data for contains query

        

        //check if key exist in hashmap gives the line locations in start
        for (int i = 0; i < input.Length; i++)
        {
            List<int> loopOutput = new List<int>(); 
            if (!stringIndex.ContainsKey(input[i])) 
                return null;
            
            List<int> temp = stringIndex[input[i]];

            foreach (int indexPlace in temp)
            {
                if(data.Contains(indexPlace))
                    loopOutput.Add(indexPlace);
                
            }
            data = loopOutput;
        }

        
        wordSequence = WordSequence(input);
        output = new List<int>(data);
        
        foreach (var index in data)
        {
            List<int> temp = indexData[index];

            int firstWord = temp.IndexOf(wordSequence.First());

            if (temp.Count - firstWord < wordSequence.Count)
                output.Remove(index);

            else
            {
                for (int i = 0; i < wordSequence.Count; i++)
                {
                    if (temp[i + firstWord] != wordSequence[i])
                    {
                        output.Remove(index);
                    }
                }
            }

 
        }
        watch.Stop();
        Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                
            

        

        return output;
    }

    public List<int> equals<G>(G value)
    {
        checkType(typeof(G),_type);
        List<int> output = new List<int>();
        List<int> wordSequence = WordSequence(Convert.ToString(value).Split(" "));
        
        List<int> data = contains(value);

        if(data == null)
            return output;
        
        foreach (int index in data)
        {
            if(indexData[index].Count == wordSequence.Count)
                output.Add(index);
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

 
    
    /**returns indexlist of the word sequence currently stored in memory**/
    private List<int> WordSequence(string[] input)
    {
        List<int> output = new List<int>();
        foreach (var key in input)
        {
            if (stringObjects.Contains(key))
                output.Add(stringObjects.IndexOf(key));
            else
                return null;
        }
        

        return output;
    }
}