namespace database;

public class FileReader
{


    public List<string> ReadFile(string filename)
    {
        List<string> output = new List<string>();
        string path = Path.Combine(Environment.CurrentDirectory, @"testData\", filename);
        Console.Out.WriteLine(path);
        string text = System.IO.File.ReadAllText(path);

        output = new List<string>( text.Split("\r\n"));
        
        return output;
    }
    
}