﻿namespace PAMSI_2.Sorts;

public static class Verifier
{
    public static void AssertSorted<T>(this SimpleArrayList<T> list, Comparator<T> comparator)
    {
        for (var i = 0; i < list.Count - 1; i++)
        {
            if (comparator.Invoke(list[i], list[i + 1]) > 0) throw new Exception("Not sorted");
        }
    }
}