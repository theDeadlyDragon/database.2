namespace database;

public class Settings
{
    private static Settings _instance;
    private static readonly object padlock = new object();

    //string column settings
    public int maxWordLength = 5;
    public int minWordLength = 1;
    public int numberOfRunners = 2;
    
    
    private Settings()
    {
        
    }

    public static Settings Instance()
    {
        lock (padlock)
        {
            if (_instance == null)
                _instance = new Settings();
            return _instance;
        }
    }


}