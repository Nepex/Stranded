using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util : MonoBehaviour
{
    /// <summary>
    /// Rolls a number between 1-100 (or passed min / max numbers)
    /// </summary>
    public static int Roll(int min = 1, int max = 100)
    {
        return Random.Range(min, max + 1);
    }

    /// <summary>
    /// Checks for successful procs depending on passed percentChanceOfProc
    /// </summary>
    public static bool DidProc(int percentChanceOfProc)
    {
        return Roll() <= percentChanceOfProc;
    }

}
