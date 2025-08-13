using System;
using UnityEngine;

public class Bag : MonoBehaviour
{
    private static int _fruits;

    public static void AddFruit()
    {
        _fruits++;
    }
}