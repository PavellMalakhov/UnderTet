using System;

public class Util
{
    private static Random s_random = new Random();

    public static int GetRandom(int maxValue)
    {
        return s_random.Next(maxValue);
    }

    public static int GetRandom(int minValue, int maxValue)
    {
        return s_random.Next(minValue, maxValue);
    }

    public static bool GetRandomBoolean()
    {
        int maxValueRandom = 2;
        return s_random.Next(maxValueRandom) == 1;
    }
}
