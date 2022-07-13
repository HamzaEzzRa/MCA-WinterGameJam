using System;

[Serializable]
public class GameData
{
    private static GameData instance;
    public static GameData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
        set
        {
            if (value != null)
                instance = value;
        }
    }

    public string CotcId;
    public string CotcSecret;
}
