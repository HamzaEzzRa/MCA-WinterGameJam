using System;
using System.IO;
using UnityEngine;

public static class Utility
{
    private static int oldSeed = -1;
    private static int currentSeed = -1;
    private static System.Random prng;

    private static void SetRandomSeed(int seed)
    {
        oldSeed = currentSeed;
        currentSeed = seed;

        if (currentSeed != oldSeed)
            prng = new System.Random(currentSeed);
    }

    public static T[] SuffleArray<T>(T[] array, int seed)
    {
        SetRandomSeed(seed);

        for (int i = 0; i < array.Length - 1; i++)
        {
            int rndIndex = prng.Next(i, array.Length);
            T tmp = array[i];
            array[i] = array[rndIndex];
            array[rndIndex] = tmp;
        }

        return array;
    }

    public static void Log(String value, String filename)
    {
        if (!Directory.Exists(Application.persistentDataPath + "/Logs"))
            Directory.CreateDirectory(Application.persistentDataPath + "/Logs");

        string path = Application.persistentDataPath + "/Logs" + filename + ".txt";
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(new DateTime().ToString(@"R") + ": " + value);
        writer.Close();
    }

    public static float GetRandomPercent(int seed)
    {
        SetRandomSeed(seed);
        return (float)prng.NextDouble();
    }

    public static RuntimePlatform Platform
    {
        get
        {
#if UNITY_ANDROID
            return RuntimePlatform.Android;
#elif UNITY_IOS
            return RuntimePlatform.IPhonePlayer;
#elif UNITY_STANDALONE_OSX
            return RuntimePlatform.OSXPlayer;
#elif UNITY_STANDALONE_WIN
            return RuntimePlatform.WindowsPlayer;
#endif
        }
    }
}
