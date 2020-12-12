using System;

public static class Utility
{
    private static int oldSeed = -1;
    private static int currentSeed = -1;
    private static Random prng;

    private static void SetRandomSeed(int seed)
    {
        oldSeed = currentSeed;
        currentSeed = seed;
        if (currentSeed != oldSeed)
        {
            prng = new Random(currentSeed);
        }
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

    public static float GetRandomPercent(int seed)
    {
        SetRandomSeed(seed);
        return (float)prng.NextDouble();
    }
}
